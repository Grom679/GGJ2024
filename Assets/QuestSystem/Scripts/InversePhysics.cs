using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InversePhysics : MonoBehaviour
{
    public bool UseInverseGravity { 
        get
        {
            return _useInverseGravity;
        }
        set
        {
            _useInverseGravity = value;
        }
    }

    [SerializeField]
    private bool _useInverseGravity;

    private Rigidbody _rigidbody;

    private float _gravity;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _gravity = -Physics.gravity.y;
    }

    private void FixedUpdate()
    {
        if (_rigidbody != null) 
        {
            if(_useInverseGravity)
            {
                _rigidbody.AddForce(Vector3.up * _gravity, ForceMode.Force);
            }
            else
            {
                _rigidbody.AddForce(Vector3.down * _gravity, ForceMode.Force);
            }
        }
    }
}
