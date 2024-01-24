using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public Action<PortalEnum> OnChangePortal;
    public Action OnActivatePortal;
    
    [SerializeField] private PortalEnum _portalEnum;
    [SerializeField] private bool _isActivated;
    [SerializeField] private PortalManager _portalManager;

    public PortalEnum PortalEnum => _portalEnum; 
    private void Awake()
    {
        _portalManager = GetComponentInParent<PortalManager>();
    }

    private void OnEnable()
    {
        OnChangePortal += ChangePortal;
        OnActivatePortal += ActivatePortal;
    }

    private void OnDisable()
    {
        OnChangePortal -= ChangePortal;
        OnActivatePortal -= ActivatePortal;
    }

    private void ActivatePortal()
    {
        if (_isActivated)
        {
            _portalManager.OnTeleport?.Invoke(this);
        }
    }

    public void EnablePortal(bool enable)
    {
        _isActivated = enable;
    }
    
    private void ChangePortal(PortalEnum portalEnum)
    {
        _portalEnum = portalEnum;
    }
}
