using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] private float _rayLenght = 5;
    [SerializeField] private LayerMask _rayMask;
    [SerializeField] private Transform _rayOrigin;
    [SerializeField] private Item item;
    [SerializeField] private InteractPoint interactPoint;
    [SerializeField] private Portal _portal;
    
    private Player _player;
    private RaycastHit hit;
    private Ray Ray;
    private Vector3 OriginPosition;
    private Quaternion Rotation;
    private Vector3 Forward;
    
    private void Awake()
    {
        _player = GetComponent<Player>();
        _rayOrigin = Camera.main.transform;
    }

    void Update()
    {
        OriginPosition = _rayOrigin.transform.position;
        Rotation = _rayOrigin.transform.rotation;
        Forward = Rotation * Vector3.forward;
        Ray = new Ray(OriginPosition, Forward);
        Debug.DrawRay(transform.position, Forward, Color.green);
        
        if (Physics.Raycast(Ray, out hit, _rayLenght, _rayMask))
        {
            item = hit.transform.GetComponent<Item>();
            interactPoint = hit.transform.GetComponent<InteractPoint>();
            _portal = hit.transform.GetComponent<Portal>();
        }
        else
        {
            item = null;
            interactPoint = null;
            _portal = null;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (_portal != null)
            {
                _portal.OnActivatePortal?.Invoke();
            }
            if (item != null)
            {
                _player.OnPickItem?.Invoke(item);
            }

            if (interactPoint != null)
            {
                _player.OnInteractItem?.Invoke(interactPoint);
            }
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            _player.OnDropItem?.Invoke();
        }
    }
}
