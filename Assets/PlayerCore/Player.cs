using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Action<Item> OnPickItem;
    public Action OnDropItem;
    public Action<InteractPoint> OnInteractItem;
    
    [SerializeField] private Item _pickedItem;
    [SerializeField] private Transform _transformItem;
    [SerializeField] private Transform _root;

    private InteractPoint _interactPoint;
    
    private void OnEnable()
    {
        OnPickItem += PickItem;
        OnDropItem += DropItem;
        OnInteractItem += IntegrateItem;
    }

    private void OnDisable()
    {
        OnPickItem -= PickItem;
        OnDropItem -= DropItem;
        OnInteractItem -= IntegrateItem;
    }

    private void PickItem(Item item)
    {
        if (_pickedItem == null)
        {
            _pickedItem = item;
            _pickedItem.OnInteractItem?.Invoke(false);
            _pickedItem.transform.position = _transformItem.position;
            _pickedItem.transform.SetParent(_transformItem);
           
            Debug.LogError("pickItem");
        }
    }
    
    private void DropItem()
    {
        if (_pickedItem != null)
        {
            _pickedItem.transform.SetParent(_root);
            _pickedItem.OnInteractItem?.Invoke(true);
            _pickedItem = null;

            Debug.LogError("drop item");
        }
    }

    private void IntegrateItem(InteractPoint point)
    {
        if (_pickedItem != null)
        {
            _interactPoint = point;
            _interactPoint.OnInteractItem?.Invoke(_pickedItem);
            _pickedItem.OnActivateItem?.Invoke();
            _pickedItem = null;
            _interactPoint = null;
        
            Debug.LogError("interact item");
        }
    }
}
