using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractPoint : MonoBehaviour
{
   public Action<Item> OnInteractItem;

   private void OnEnable()
   {
      OnInteractItem += InteractAction;
   }

   private void OnDisable()
   {
      OnInteractItem -= InteractAction;
   }

   private void InteractAction(Item item)
   {
      Destroy(item.gameObject);
   }
}
