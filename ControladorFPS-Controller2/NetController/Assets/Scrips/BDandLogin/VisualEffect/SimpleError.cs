using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//https://answers.unity.com/questions/1199251/onmouseover-ui-button-c.html
public class SimpleError : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public void OnPointerEnter(PointerEventData eventData)
    {
        //do stuf
        this.gameObject.transform.Find("Panel").gameObject.SetActive(true);
    }
    public void OnPointerExit(PointerEventData pointerEventData)
    {
        this.gameObject.transform.Find("Panel").gameObject.SetActive(false);
    }
}

