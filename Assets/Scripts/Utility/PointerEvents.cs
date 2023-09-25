using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class PointerEvents : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{
    public event Action<PointerEventData> onPointerEnter;
    public event Action<PointerEventData> onPointerDown;
    public event Action<PointerEventData> onPointerUp;
    public event Action<PointerEventData> onPointerExit;

    public void OnPointerEnter(PointerEventData data)
    {
        if(onPointerEnter != null)
            onPointerEnter(data);
    }

    public void OnPointerDown(PointerEventData data)
    {
        if(onPointerDown != null)
            onPointerDown(data);
    }

    public void OnPointerUp(PointerEventData data)
    {
        if(onPointerUp != null)
            onPointerUp(data);
    }

    public void OnPointerExit(PointerEventData data)
    {
        if(onPointerExit != null)
            onPointerExit(data);
    }
}
