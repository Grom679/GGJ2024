using System;
using System.Collections;
using System.Collections.Generic;
using PuzzleGame.Core;
using UnityEngine;
using UnityEngine.Rendering;

public class Player : MonoBehaviour
{
    public Action<Item> OnPickItem;
    public Action OnDropItem;
    public Action<InteractPoint> OnInteractItem;
    public Action OnBookQuestChange;

    public Item PickedItem => _pickedItem;

    [SerializeField] private Item _pickedItem;
    [SerializeField] private Transform _transformItem;
    [SerializeField] private Transform _root;
    [SerializeField] private Volume _monochromeVolume;
    
    private InteractPoint _interactPoint;
    private FirstPersonController _firstPersonController;

    private void Awake()
    {
        _firstPersonController = GetComponent<FirstPersonController>();
    }

    private void OnEnable()
    {
        OnPickItem += PickItem;
        OnDropItem += DropItem;
        OnInteractItem += IntegrateItem;
        OnBookQuestChange += MonochomeVolume;
        GlobalEvents.Instance.OnChainStarted += DisableMovement;
        GlobalEvents.Instance.OnChainFinished += EnableMovement;
    }

    private void OnDisable()
    {
        OnPickItem -= PickItem;
        OnDropItem -= DropItem;
        OnInteractItem -= IntegrateItem;
        OnBookQuestChange -= MonochomeVolume;
        GlobalEvents.Instance.OnChainStarted -= DisableMovement;
        GlobalEvents.Instance.OnChainFinished -= EnableMovement;
    }

    private void PickItem(Item item)
    {
        if(!item.CanBeGrabbed)
        {
            return;
        }

        if (_pickedItem == null)
        {
            _pickedItem = item;
            _pickedItem.transform.position = _transformItem.position;
            _pickedItem.transform.SetParent(_transformItem);
            _pickedItem.OnActivateItem?.Invoke();
        }
    }
    
    private void DropItem()
    {
        if (_pickedItem != null)
        {
            _pickedItem.transform.SetParent(_root);
            _pickedItem.OnDropItem?.Invoke();
            _pickedItem = null;
        }
    }

    private void IntegrateItem(InteractPoint point)
    {
        if (_pickedItem != null)
        {
            _interactPoint = point;
            _interactPoint.OnInteractItem?.Invoke(_pickedItem);
            
            
            // _pickedItem = null;
            // _interactPoint = null;
        }
    }

    private void DisableMovement()
    {
        _firstPersonController.playerCanMove = false;
    }

    private void EnableMovement()
    {
        _firstPersonController.playerCanMove = true;
    }

    private void MonochomeVolume()
    {
        _monochromeVolume.enabled = !_monochromeVolume.enabled;
    }
    
    public void IncreasePointer()
    {
        _firstPersonController.crosshairObject.gameObject.transform.localScale = Vector2.one *0.2f;
        _firstPersonController.crosshairObject.color = Color.red;
        
    }
    
    public void DecreasePointer()
    {
        _firstPersonController.crosshairObject.color = Color.white;
        _firstPersonController.crosshairObject.gameObject.transform.localScale = Vector2.one * 0.05f;
    }
}
