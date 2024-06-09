using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using UnityEngine.UI;
using System;

public class MainMenuController : MonoBehaviour
{
    public float titleScaleSize = 2f;
    public float titleScaleSpeed = .1f;

    public float delayBetweenActivations = 0.15f;
    public List<GameObject> title = new List<GameObject>();
    public List<GameObject> menuButtons = new List<GameObject>();
    public List<GameObject> anyButtons = new List<GameObject>();
    private bool doOnce = true; 
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown&& doOnce)
        {
            SetPromptTextInactive();
            TitleScale();
            Debug.Log("keydown");
            doOnce=false;
        }
    }

    void SetPromptTextInactive()
    {
        foreach (GameObject go in anyButtons)
        {
            go.SetActive(false);
        }
    }
    void TitleScale()
    {
        title.PickRandom().SetActive(true);
        foreach (GameObject go in title)
        {

            go.GetComponent<TextMeshProUGUI>().LeanTMPAlpha(1f, .05f);
            LeanTween.scale(go, new Vector3(1f, 1f, 1f), titleScaleSpeed).setEaseInExpo();
            StartCoroutine(ActivateButtonsCoroutine());
        }
    }

    private IEnumerator ActivateButtonsCoroutine()
    {
        GameObject activeButtonGroup = menuButtons.PickRandom();
        activeButtonGroup.SetActive(true);
        Debug.Log("Name of Active is" + activeButtonGroup.name);
        List<GameObject> children = GetTopLevelChildrenOf(activeButtonGroup);
        

        foreach (GameObject child in children)
        {

            yield return new WaitForSeconds(delayBetweenActivations);
            child.SetActive(true);
            
        }

    }
    public List<GameObject> GetTopLevelChildrenOf(GameObject parent)
    {
        List<GameObject> topLevelChildren = new List<GameObject>();

        // Iterate through each child of the parent GameObject
        foreach (Transform child in parent.transform)
        {
            topLevelChildren.Add(child.gameObject);
        }

        return topLevelChildren;
    }

    void ActivateGameObject(GameObject obj)
    {
        obj.SetActive(true);
    }
}
