
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ExtendedButton : Button, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public Action<PointerEventData> OnDownEvent = (i_PointerEventData) => { };
    public Action<PointerEventData> OnUpEvent = (i_PointerEventData) => { };
    public Action<PointerEventData> OnBeginDragEvent = (i_PointerEventData) => { };
    public Action<PointerEventData> OnDragEvent = (i_PointerEventDataevData) => { };
    public Action<PointerEventData> OnEndDragEvent = (i_PointerEventData) => { };



    public override void OnPointerDown(PointerEventData eventData)
    {
        base.OnPointerDown(eventData);

        OnDownEvent?.Invoke(eventData);
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        base.OnPointerUp(eventData);

        OnUpEvent?.Invoke(eventData);
    }

    public virtual void OnBeginDrag(PointerEventData i_PointerEventData)
    {
        OnBeginDragEvent?.Invoke(i_PointerEventData);
    }

    public virtual void OnDrag(PointerEventData i_PointerEventData)
    {
        OnDragEvent?.Invoke(i_PointerEventData);
    }

    public virtual void OnEndDrag(PointerEventData i_PointerEventData)
    {
        OnEndDragEvent?.Invoke(i_PointerEventData);
    }
}
