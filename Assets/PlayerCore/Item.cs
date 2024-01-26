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

    public PortalEnum CurrentPosition { get; set; }
    public Rigidbody Rigidbody => _rigidbody;

    [SerializeField] private bool _fullDisactivated;
    [SerializeField] private bool _useGravity = true;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private QuestItem _questItem;

    private TooltipObj _tooltipObj;
    private Transform _defaultTransform;

   private void Awake()
   {
      _rigidbody = GetComponent<Rigidbody>();
      _questItem = GetComponent<QuestItem>();

      CurrentPosition = _questItem.BelongsTo;
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

      if(_useGravity)
      {
          _rigidbody.useGravity = true;
      }
      
      _rigidbody.isKinematic = false;
      Scenario.Instance.CurrentQuest.DisactivateQuestItem(_questItem, _fullDisactivated);
   }

   private void InteractItem()
   {
      Debug.LogError("use item");
      Scenario.Instance.CurrentQuest.QuestPoint.PutQuestItem(_questItem);
      //Destroy(gameObject);
   }
   
   public QuestItemType CheckItemType()
   {
      return _questItem.ItemType;
   }
}
