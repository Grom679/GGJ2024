using System;
using System.Collections;
using System.Collections.Generic;
using PuzzleGame.Core;
using PuzzleGame.Quest;
using UnityEngine;

public class PortalManager : MonoBehaviour
{
    public Action<Portal> OnTeleport;
    [SerializeField] private List<Portal> _portals;
    [SerializeField] private Transform _house;
    [SerializeField] private CameraFade _cameraFade;
    //[SerializeField] private Portal _mainPortal;
    [SerializeField] private List<PortalEnt> _portalEnts;

    private void Awake()
    {
        _portals = new List<Portal>(GetComponentsInChildren<Portal>(true));
    }

    private void OnEnable()
    {
        OnTeleport += Teleportate;
    }

    private void OnDisable()
    {
        OnTeleport -= Teleportate;
    }

    //Debug
    private void Start()
    {
        DisablePortals();
    }

    public void TeleportTo(PortalEnum location, PortalEnum portalEnum)
    {
        Portal portal = FindPortal(location, portalEnum);

        Debug.Log(portal.gameObject.name);

        if(portal != null)
        {
            Teleportate(portal);
        }
    }

    private void Teleportate(Portal portal)
    {
        StartCoroutine(TeleportateCoroutine(portal));
    }

    private IEnumerator TeleportateCoroutine(Portal portal)
    {
        _cameraFade.Fade();
        yield return new WaitForSeconds(1f);
        CheckPortal(portal.PortalTo);
        portal.OnAdditionalAction?.Invoke();
        _cameraFade.Fade();
    }

    public void ChangeMainPortal(PortalEnum portalEnum)
    {
        _portals[0].OnChangePortal?.Invoke(portalEnum);
    }

    public void EnablePortal(PortalEnum location, PortalEnum portalEnum)
    {
        Portal portal = FindPortal(location, portalEnum);

        if(portal != null)
        {
            portal.EnablePortal(true);
        }
    }

    public void DisablePortal(PortalEnum location, PortalEnum portalEnum)
    {
        Portal portal = FindPortal(location, portalEnum);

        if (portal != null)
        {
            portal.EnablePortal(false);
        }
    }

    private void CheckPortal(PortalEnum portalEnum)
    {
        foreach (PortalEnt ent in _portalEnts)
        {
            if (portalEnum == ent._portalEnum)
            {
                Debug.LogError(ent._portalEnum);
                _house.position = ent._transform;
                _house.eulerAngles = ent._rotate;
                Scenario.Instance.Player.transform.position = ent._portalPos.position;
            }
        }
    }

    private Portal FindPortal(PortalEnum location, PortalEnum portalEnum)
    {
        List<Portal> portals = _portals.FindAll(x => x.PortalLocation == location);

        Portal portal = portals.Find(x => x.PortalTo == portalEnum);

        return portal;
    }

    private void DisablePortals()
    {
        foreach (Portal portal in _portals)
        {
            portal.EnablePortal(false);
        }
    }

    private void EnablePortals()
    {
        foreach (Portal portal in _portals)
        {
            portal.EnablePortal(true);
        }
    }

    public void SetAdditionalActionOnPortal(PortalEnum location, PortalEnum portalEnum, Action action)
    {
        Portal portal = FindPortal(location, portalEnum);
        portal.OnAdditionalAction += action;
    }
    
    public void RemoveAdditionalActionOnPortal(PortalEnum location, PortalEnum portalEnum, Action action)
    {
        Portal portal = FindPortal(location, portalEnum);
        portal.OnAdditionalAction -= action;
    }

}

[Serializable]
public class PortalEnt
{
    public PortalEnum _portalEnum;
    public Transform _portalPos;
    public Vector3 _rotate;
    public Vector3 _transform;
}