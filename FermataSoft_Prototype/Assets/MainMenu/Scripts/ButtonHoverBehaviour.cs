using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonHoverBehaviour : MonoBehaviour, IPointerEnterHandler , IPointerExitHandler
{
    public float scaleSize= 1.5f;
    public float tweenTime = .5f;
    public void OnPointerEnter(PointerEventData eventData)
    {
        LeanTween.scale(this.gameObject, new Vector3(scaleSize,scaleSize,scaleSize),tweenTime);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        LeanTween.scale(this.gameObject, new Vector3(1,1,1),tweenTime);
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
