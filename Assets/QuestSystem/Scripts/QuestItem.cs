using PuzzleGame.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PuzzleGame.Quest
{
    public enum QuestItemType
    {
        Bucket,
        Dynamite,
        Book,
        FakeBook,
        Plant,
        FakePlant,
        Flask,
        FakeFlask,
        Bug,
        Undercut,
        Picture
    }

    [RequireComponent(typeof(Item))]
    public class QuestItem : MonoBehaviour
    {
        public bool ItemActivated => _itemActivated;
        public QuestItemType ItemType => _itemType;

        public PortalEnum BelongsTo => _belongsTo;

        [SerializeField]
        private QuestItemType _itemType;
        [SerializeField]
        private PortalEnum _belongsTo;

        private bool _itemActivated;
        private Vector3 _defaultPosition;
        private Quaternion _defaultRotation;
        private Transform _defaultParent;

        private void Start()
        {
            _defaultParent = transform.parent;

            UpdateDefaultPosition();
        }

        public void UpdateDefaultPosition()
        {
            _defaultPosition = transform.localPosition;
            _defaultRotation = transform.localRotation;
        }

        public void ActivateQuestItem()
        {
            if(!_itemActivated)
            {
                _itemActivated = true;
            }
        }

        public void DisactivateQuestItem()
        {
            if (_itemActivated)
            {
                _itemActivated = false;
            }
        }

        public void ResetItem()
        {
            StartCoroutine(StartHideEffect(ReturnItem));
        }

        public void UseItem()
        {
            StartCoroutine(StartHideEffect(DeleItem));
        }

        private IEnumerator StartHideEffect(Action action)
        {
            yield return new WaitForSeconds(1);

            Debug.LogError("Hided item");

            action?.Invoke();

            //Hide effect
        }

        private void ReturnItem()
        {
            if (_itemActivated)
            {
                Scenario.Instance.Player.OnDropItem?.Invoke();

                GlobalEvents.Instance.OnResetItem(this);

                _itemActivated = false;

                transform.parent = _defaultParent;

                transform.localPosition = _defaultPosition;
                transform.localRotation = _defaultRotation;
            }
        }

        private void DeleItem()
        {
           
        }
    }
}
