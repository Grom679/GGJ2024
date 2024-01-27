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
        Picture,
        Glasses,
        FakePicture
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
        private Vector3 _defaultScale;
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
            _defaultScale = transform.localScale;
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
            StartCoroutine(StartHideEffect(ReturnItem, true));
        }

        public void UseItem()
        {
            StartCoroutine(StartHideEffect(DeleItem, false));
        }

        private IEnumerator StartHideEffect(Action action, bool isReset)
        {
            if(!isReset)
            {
                Scenario.Instance.Explosion.SetActive(false);

                Scenario.Instance.Explosion.transform.position = transform.position;

                Scenario.Instance.Explosion.SetActive(true);

                yield return new WaitForSeconds(0f);

                action?.Invoke();
            }
            else
            {
                yield return new WaitForSeconds(1f);

                Scenario.Instance.Explosion.SetActive(false);

                Scenario.Instance.Explosion.transform.position = transform.position;

                Scenario.Instance.Explosion.SetActive(true);

                action?.Invoke();
            }
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
                transform.localScale = _defaultScale;

                transform.GetComponent<Rigidbody>().isKinematic = true;
            }
        }

        private void DeleItem()
        {
            if (_itemActivated)
            {
                Scenario.Instance.Player.OnDropItem?.Invoke();

                GlobalEvents.Instance.OnResetItem(this);

                ReturnItem();

                gameObject.SetActive(false);
            }
        }
    }
}
