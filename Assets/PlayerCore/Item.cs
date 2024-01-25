using System;
using System.Collections;
using System.Collections.Generic;
using PuzzleGame.Quest;
using UnityEngine;

public class Item : MonoBehaviour
{
   public Action OnInteractItem;
   public Action OnActivateItem;
   public Action OnDropItem;

   [SerializeField] private bool _fullDisactivated;
   [SerializeField] private Rigidbody _rigidbody;
   [SerializeField] private QuestItem _questItem;
   [SerializeField] private InteractPoint _interactPoint;

   private void Awake()
   {
      _rigidbody = GetComponent<Rigidbody>();
      _questItem = GetComponent<QuestItem>();
   }

   private void OnEnable()
   {
      OnActivateItem += Activate;
      OnInteractItem += InteractItem;
      OnDropItem += Drop;
   }

   private void OnDisable()
   {
      OnActivateItem -= Activate;
      OnInteractItem -= InteractItem;
      OnDropItem -= Drop;
   }

   private void Activate()
   {
      Debug.LogError("pick/activate item");
      _rigidbody.useGravity = false;
      _rigidbody.isKinematic = true;
      Scenario.Instance.CurrentQuest.ActivateQuestItem(_questItem);
   }
   
   private void Drop()
   {
      Debug.LogError("drop item");
     // _rigidbody.useGravity = true;
      _rigidbody.isKinematic = false;
      Scenario.Instance.CurrentQuest.DisactivateQuestItem(_questItem, _fullDisactivated);
   }

   private void InteractItem()
   {
      Debug.LogError("use item");
      Scenario.Instance.CurrentQuest.QuestPoint.PutQuestItem(_questItem);
      Destroy(gameObject);
   }
}
