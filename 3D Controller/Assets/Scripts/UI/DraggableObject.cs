using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class DraggableObject : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private Transform targetSlot;
    public Transform TargetSlot { get { return targetSlot; } set { targetSlot = value; } }

    private Image Icon;

    private void Awake()
    {
        TargetSlot = transform.parent;
        Icon = GetComponent<Image>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
        Icon.raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Mouse.current.position.ReadValue();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        MoveSlotItem(TargetSlot);
        Icon.raycastTarget = true;
    }


    public void MoveSlotItem(Transform _target)
    {
        transform.SetParent(_target);
        TargetSlot = _target;
    }
}
