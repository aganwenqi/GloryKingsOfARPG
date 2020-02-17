using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class ButtonState : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{

    public UnityEvent onPointerEnter;
    public UnityEvent onPointerExit;
    public UnityEvent onPointerDown;
    public UnityEvent onPointerUp;
    //鼠标进入按钮区域
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (onPointerEnter != null)
            onPointerEnter.Invoke();
    }
    //鼠标离开按钮区域
    public void OnPointerExit(PointerEventData eventData)
    {
        if (onPointerExit != null)
            onPointerExit.Invoke();
    }

    //鼠标按下
    public void OnPointerDown(PointerEventData eventData)
    {
        if (onPointerDown != null)
            onPointerDown.Invoke();
    }
    //鼠标松开
    public void OnPointerUp(PointerEventData eventData)
    {
        if (onPointerUp != null)
            onPointerUp.Invoke();
    }
}
