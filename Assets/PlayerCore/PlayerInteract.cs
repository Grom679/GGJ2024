using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] private float _rayLenght = 5;
    [SerializeField] private LayerMask _rayMask;
    [SerializeField] private Transform _rayOrigin;
    
    private Player _player;
    private RaycastHit hit;
    private Item item;
    private InteractPoint interactPoint;
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
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
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
