using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UiDrager : MonoBehaviour, IPointerDownHandler,IDragHandler
{
    public RectTransform drager;

    public void OnDrag(PointerEventData eventData)
    {
        drager.anchoredPosition += eventData.delta;
      
       // transform.position = Input.mousePosition;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        
    }
}
