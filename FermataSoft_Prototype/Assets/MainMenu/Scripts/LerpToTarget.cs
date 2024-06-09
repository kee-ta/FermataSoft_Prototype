using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DPUtils;

public class LerpToTarget : MonoBehaviour
{
    public RectTransform targetPos;
    // Start is called before the first frame update
    void Start()
    {
        //LeanTween.followDamp(this.transform,targetPos.transform,LeanProp.position,1f);
        LeanTween.move(this.gameObject,targetPos.transform,1f).setEaseInOutExpo();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
