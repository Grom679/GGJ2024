using System;
using System.Collections;
using System.Collections.Generic;
using PuzzleGame.Quest;
using UnityEngine;

public class InteractPoint : MonoBehaviour
{
   public Action<Item> OnInteractItem;

   [SerializeField] private QuestPoint _questPoint;

   private void Awake()
   {
      _questPoint = GetComponent<QuestPoint>();
   }

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
      item.OnInteractItem?.Invoke();
   }
}
