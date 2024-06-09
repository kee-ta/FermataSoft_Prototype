using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextAlphaTween : MonoBehaviour
{
    public TextMeshProUGUI text;
    public float cycleDuration = 5.0f;
    private Coroutine lerpCoroutine;
    // Start is called before the first frame update
    void Start()
    {
        if (text == null)
        {
            text = GetComponent<TextMeshProUGUI>();
        }

        StartLerping();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void StartLerping()
    {
        StartCoroutine(LerpAlpha());
    }
 private IEnumerator LerpAlpha()
    {
        while (true)
        {
            // Fade in
            yield return LerpAlphaValue(0f, 1f, cycleDuration / 2);
            //wait abit
            yield return new WaitForSeconds(1f);
            // Fade out
            yield return LerpAlphaValue(1f, 0f, cycleDuration / 2);
            yield return new WaitForSeconds(.3f);
        }
    }

    private IEnumerator LerpAlphaValue(float startAlpha, float endAlpha, float duration)
    {
        float time = 0;
        Color color = text.color;
        
        while (time < duration)
        {
            time += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, endAlpha, time / duration);
            text.color = new Color(color.r, color.g, color.b, alpha);
            yield return null;
        }
        
        // Ensure the final alpha value is set
        text.color = new Color(color.r, color.g, color.b, endAlpha);
    }
}
