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
            if(_itemActivated)
            {
                _itemActivated = false;

                transform.position = _defaultPosition;
                transform.rotation = _defaultRotation;
            }
        }

        public void UseItem()
        {
            StartCoroutine(StartHideEffect());
        }

        private IEnumerator StartHideEffect()
        {
            yield return new WaitForSeconds(2);

            Debug.LogError("Hided item");
            //Hide effect
        }
    }
}
