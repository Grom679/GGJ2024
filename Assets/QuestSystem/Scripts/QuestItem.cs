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
        Flask,
        FakeFlask,
        Bug,
        Picture
    }

    public class QuestItem : MonoBehaviour
    {
        public bool ItemActivated => _itemActivated;
        public QuestItemType ItemType => _itemType;

        [SerializeField]
        private QuestItemType _itemType;

        private bool _itemActivated;
        private Vector3 _defaultPosition;
        private Quaternion _defaultRotation;

        private void Start()
        {
            UpdateDefaultPosition();
        }

        public void UpdateDefaultPosition()
        {
            _defaultPosition = transform.position;
            _defaultRotation = transform.rotation;
        }

        public void ActivateQuestItem()
        {
            if(!_itemActivated)
            {
                _itemActivated = true;
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
            yield return new WaitForSeconds(2);

            Debug.LogError("Hided item");

            action?.Invoke();

            //Hide effect
        }

        private void ReturnItem()
        {
            if (_itemActivated)
            {
                _itemActivated = false;

                transform.position = _defaultPosition;
                transform.rotation = _defaultRotation;
            }
        }

        private void DeleItem()
        {
           
        }
    }
}
