/// Sprite에 SpriteRenderer 에 버튼으로 사용
/// UI랑 같이 눌릴 경우 동작하지 않게 함.

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class SpriteButton : MonoBehaviour
{
    public UnityEvent onClickEvent;
    private new Collider2D collider2D;

    public bool m_interactable = true;
    public bool interactable
    {
        get { return m_interactable; }
        set
        {
            if (m_interactable != value)
            {
                if (value)
                    SetEnable();
                else
                    SetDisable();

                if (collider2D != null)
                    collider2D.enabled = value;

                m_interactable = value;
            }
        }
    }

    public enum Transition
    {
        None,
        ColorTint,
        SpriteSwap
    }
    public Transition transition = Transition.None;
    public SpriteRenderer targetSpriteRenderer;
    public Color pressColor = new Color(0.78f, 0.78f, 0.78f, 1);
    public Color disableColor = new Color(0.78f, 0.78f, 0.78f, 0.5f);
    public float fadeDuration = 0.1f;

    public Sprite enableSprite;
    public Sprite disableSprite;
    public Sprite pressSprite;

    private void OnEnable()
    {
        collider2D = GetComponent<Collider2D>();
        collider2D.isTrigger = true;

        targetSpriteRenderer = GetComponent<SpriteRenderer>();

        if (targetSpriteRenderer != null)
            enableSprite = targetSpriteRenderer.sprite;
    }

    private void SetEnable()
    {
        if (transition == Transition.SpriteSwap)
        {
            if (targetSpriteRenderer != null && enableSprite != null)
                targetSpriteRenderer.sprite = enableSprite;
        }
        else if (transition == Transition.ColorTint)
        {
            if (targetSpriteRenderer != null)
                targetSpriteRenderer.color = Color.white;
        }
    }

    private void SetDisable()
    {
        if (transition == Transition.SpriteSwap)
        {
            if (targetSpriteRenderer != null && disableSprite != null)
                targetSpriteRenderer.sprite = disableSprite;
        }
        else if (transition == Transition.ColorTint)
        {
            if (targetSpriteRenderer != null)
                targetSpriteRenderer.color = disableColor;
        }
    }

    private void SetPress()
    {
        if (transition == Transition.SpriteSwap)
        {
            if (targetSpriteRenderer != null && pressSprite != null)
                targetSpriteRenderer.sprite = pressSprite;
        }
        else if (transition == Transition.ColorTint)
        {
            if (targetSpriteRenderer != null)
                targetSpriteRenderer.color = pressColor;
        }
    }


    private bool IsPointerOverUIObject()
    {
        // Referencing this code for GraphicRaycaster https://gist.github.com/stramit/ead7ca1f432f3c0f181f
        // the ray cast appears to require only eventData.position.
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }

    private void OnMouseDown()
    {
        //Debug.Log(name + " OnMouseDown");
        if (EventSystem.current.IsPointerOverGameObject() || IsPointerOverUIObject())
        {
            //Debug.Log(name + " OnMouseDown return IsPointerOverGameObject(): " + EventSystem.current.IsPointerOverGameObject() + " /IsPointerOverUIObject(): " + IsPointerOverUIObject());
            return;
        }

        if (interactable)
            SetPress();
    }


    private void OnMouseUp()
    {
        //Debug.Log(name + " OnMouseUp isActive: " + isActive);
        if (EventSystem.current.IsPointerOverGameObject() || IsPointerOverUIObject())
        {
            return;
        }

        onClickEvent.Invoke();

        if (interactable)
            SetEnable();
    }
}




#if UNITY_EDITOR
[CustomEditor(typeof(SpriteButton))]
public class SpriteButtonInSpriteInspector : Editor
{
    SpriteButton spriteButton;

    public override void OnInspectorGUI()
    {
        EditorGUI.BeginChangeCheck();

        spriteButton = (SpriteButton)target;

        // 컬라이더가 없으면 BoxCollider를 붙인다.
        if (spriteButton.GetComponent<Collider2D>() == null)
            spriteButton.gameObject.AddComponent<BoxCollider2D>();

        spriteButton.interactable = EditorGUILayout.Toggle("Interactable", spriteButton.interactable);
        string[] enums = Enum.GetNames(typeof(SpriteButton.Transition));
        spriteButton.transition = (SpriteButton.Transition)EditorGUILayout.Popup("Transition", (int)spriteButton.transition, enums);

        EditorGUI.indentLevel += 1;
        // 컬러 타입
        if (spriteButton.transition == SpriteButton.Transition.ColorTint)
        {
            // 타겟 스프라이트가 없으면 현제 스프라이트로 한다.
            if (spriteButton.targetSpriteRenderer == null)
                spriteButton.targetSpriteRenderer = spriteButton.gameObject.GetComponent<SpriteRenderer>();

            spriteButton.targetSpriteRenderer = (SpriteRenderer)EditorGUILayout.ObjectField("Target Sprite", spriteButton.targetSpriteRenderer, typeof(SpriteRenderer), true);
            spriteButton.pressColor = EditorGUILayout.ColorField("Press Color", spriteButton.pressColor);
            spriteButton.disableColor = EditorGUILayout.ColorField("Disable Color", spriteButton.disableColor);
            spriteButton.fadeDuration = EditorGUILayout.FloatField("Fade Duration", spriteButton.fadeDuration);
        }
        // 이미지 타입
        else if (spriteButton.transition == SpriteButton.Transition.SpriteSwap)
        {
            // 타겟 스프라이트가 없으면 현제 스프라이트로 한다.
            if (spriteButton.targetSpriteRenderer == null)
                spriteButton.targetSpriteRenderer = spriteButton.gameObject.GetComponent<SpriteRenderer>();
            spriteButton.pressSprite = (Sprite)EditorGUILayout.ObjectField("Press Sprite", spriteButton.pressSprite, typeof(Sprite), true, GUILayout.Height(EditorGUIUtility.singleLineHeight));
            spriteButton.disableSprite = (Sprite)EditorGUILayout.ObjectField("Disable Sprite", spriteButton.disableSprite, typeof(Sprite), true, GUILayout.Height(EditorGUIUtility.singleLineHeight));
        }
        EditorGUI.indentLevel -= 1;

        // OnClick 이벤트
        EditorGUILayout.Space();
        SerializedProperty sprop = serializedObject.FindProperty("onClickEvent");
        EditorGUILayout.PropertyField(sprop);

        // 저장
        if (EditorGUI.EndChangeCheck())
        {
            EditorUtility.SetDirty(target);
            serializedObject.ApplyModifiedProperties();
            AssetDatabase.SaveAssets();
        }
    }
}
#endif
