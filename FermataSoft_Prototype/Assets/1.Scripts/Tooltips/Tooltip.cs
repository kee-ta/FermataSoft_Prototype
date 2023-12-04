using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode()]
public class Tooltip : MonoBehaviour
{

    public TextMeshProUGUI headerField;

    public TextMeshProUGUI contentField;

    public LayoutElement layoutElement;

    public int characterWrapLimit;

    public RectTransform rectTransform;

    public float mouseIconOffset = 0.003f;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    private void OnEnable()
    {
        GetComponent<CanvasGroup>().alpha = 0f;
        LeanTween.alphaCanvas(GetComponent<CanvasGroup>(), 1f, 0.15f);
    }

    public void SetText(string content, string header = "")
    {
        if (string.IsNullOrEmpty(header))
        {
            headerField.gameObject.SetActive(false);
        }
        else
        {
            headerField.gameObject.SetActive(true);
            headerField.text = header;
        }

        contentField.text = content;

        int headerLength = headerField.text.Length;
        int contentLength = contentField.text.Length;

        layoutElement.enabled = (headerLength > characterWrapLimit || contentLength > characterWrapLimit) ? true : false;

    }

    private void Update()
    {
        if (Application.isEditor)
        {
            int headerLength = headerField.text.Length;
            int contentLength = contentField.text.Length;

            layoutElement.enabled = (headerLength > characterWrapLimit || contentLength > characterWrapLimit) ? true : false;
        }

        Vector2 position = Input.mousePosition;

        float pivotX = position.x / Screen.width;
        float pivotY = position.y / Screen.height;

        if(pivotX < 0.5f)
        {
            if (layoutElement.enabled)
            {
                pivotX = mouseIconOffset;
            }
            else
            {
                var width = layoutElement.preferredWidth / rectTransform.rect.width;
                pivotX = (mouseIconOffset * width);
            }
        }
        if(pivotX > 0.5f)
        {
            pivotX = 1f;
        }


        rectTransform.pivot = new Vector2(pivotX, pivotY);
        transform.position = position;

    }


}
