using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonHoverBehaviour : MonoBehaviour, IPointerEnterHandler , IPointerExitHandler
{
    public bool hasSelector = false;
    public float selectorOriginalY = .5f;
    public GameObject selector;
    public float scaleSize= 1.5f;
    public float tweenTime = .5f;
    public void OnPointerEnter(PointerEventData eventData)
    {
        LeanTween.scale(this.gameObject, new Vector3(scaleSize,scaleSize,scaleSize),tweenTime);
        if(hasSelector)
        LeanTween.scale(selector, new Vector3(1,scaleSize,1),tweenTime);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        LeanTween.scale(this.gameObject, new Vector3(1,1,1),tweenTime);
        if(hasSelector)
        LeanTween.scale(selector, new Vector3(1,selectorOriginalY,1),tweenTime);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
