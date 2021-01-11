using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonForWaiter : MonoBehaviour, IPointerDownHandler
{
    public bool Interactable = true;
    public Waiter Waiter;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (Interactable)
        {
            Waiter.TakeOrder();
        }
    }
}
