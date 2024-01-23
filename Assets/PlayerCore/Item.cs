using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
   public Action<bool> OnInteractItem;
   public Action OnActivateItem;

   [SerializeField] private Rigidbody _rigidbody;

   private void Awake()
   {
      _rigidbody = GetComponent<Rigidbody>();
   }

   private void OnEnable()
   {
      OnActivateItem += Activate;
      OnInteractItem += InteractItem;
   }

   private void OnDisable()
   {
      OnActivateItem -= Activate;
      OnInteractItem -= InteractItem;
   }

   private void Activate()
   {
      Debug.LogError("activate item");
   }

   private void InteractItem(bool enable)
   {
      _rigidbody.useGravity = enable;
      _rigidbody.isKinematic = !enable;
   }
}
