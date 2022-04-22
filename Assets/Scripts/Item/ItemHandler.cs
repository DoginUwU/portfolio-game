using System;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider2D))]
public class ItemHandler : MonoBehaviour
{
    public UnityEvent onAction;

    private void Action() {
        if(onAction != null) 
            onAction.Invoke();

        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerBehaviour>()) Action();
    }
}
