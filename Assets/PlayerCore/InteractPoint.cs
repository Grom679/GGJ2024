using System;
using System.Collections;
using System.Collections.Generic;
using PuzzleGame.Quest;
using UnityEngine;

public class InteractPoint : MonoBehaviour
{
   public Action<Item> OnInteractItem;
   
   [SerializeField] private List<QuestItemType> _correctItems;

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
      if (_correctItems.Contains(item.CheckItemType()))
      {
         item.OnInteractItem?.Invoke();
      }
      else
      {
         Debug.LogError("wrong " + item.name + " to " + name);
      }
   }
}
