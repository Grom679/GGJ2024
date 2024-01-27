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

    public bool CanBeGrabbed { get; set; } = true;

    public PortalEnum CurrentPosition { get; set; }
    public Rigidbody Rigidbody => _rigidbody;

    [SerializeField] private bool _fullDisactivated;
    [SerializeField] private bool _useGravity = true;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private Collider _collider;
    [SerializeField] private QuestItem _questItem;
    [SerializeField] private Vector3 _grabbedRotAngle;
    [SerializeField] private Vector3 _grabbedPos;
    [SerializeField] private bool _useItem = false;
       
    private Transform _defaultParent;
    private Transform _defaultTransform;

   private void Awake()
   {
      _rigidbody = GetComponent<Rigidbody>();
      _questItem = GetComponent<QuestItem>();
      _collider = GetComponent<Collider>();
      _defaultParent = transform.parent;
      CurrentPosition = _questItem.BelongsTo;
      CanBeGrabbed = false;
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
      _collider.isTrigger = true;
      transform.localPosition = _grabbedPos;
      transform.localEulerAngles = _grabbedRotAngle;
      Scenario.Instance.CurrentQuest.ActivateQuestItem(_questItem);
   }
   
   private void Drop()
   {
      Debug.LogError("drop item");

      transform.parent = _defaultParent;
      if(_useGravity)
      {
          _rigidbody.useGravity = true;
      }
      _collider.isTrigger = false;
      _rigidbody.isKinematic = false;
      Scenario.Instance.CurrentQuest.DisactivateQuestItem(_questItem, _fullDisactivated);
   }

   private void InteractItem()
   {
      Debug.LogError("use item");
      if (!_useItem)
      {
         _useItem = true;
         Scenario.Instance.CurrentQuest.QuestPoint.PutQuestItem(_questItem);
      }
      
   }
   
   public QuestItemType CheckItemType()
   {
      return _questItem.ItemType;
   }
   
   public void MakeUngrabble()
   {
      CanBeGrabbed = false;
   }

   public void MakeGrabble()
   {
      CanBeGrabbed = true;
   }
}
