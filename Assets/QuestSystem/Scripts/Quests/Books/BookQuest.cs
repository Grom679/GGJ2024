using PuzzleGame.Audio;
using PuzzleGame.Core;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

namespace PuzzleGame.Quest
{
    public class BookQuest : SimpleQuest
    {
        public bool QuestIsActive { get; private set; }

        public bool GrabbedCorrect { get; private set; }

        [SerializeField]
        private List<QuestItem> _changingBooks;

        private QuestItem _book;
        private int _swapIndex;

        public void EnterBookWall()
        {
            if(QuestIsActive)
            {
                ReshuffleBooks();
            }
        }

        public void ExitBookWall() 
        {
            if (QuestIsActive) 
            {
                if (!GrabbedCorrect && _book)
                {
                    GlobalEvents.Instance.OnQuestError?.Invoke();
                }
            } 
        }

        protected override void FinishQuestInnerActions()
        {
            QuestIsActive = false;
            QuestPoint.DeactivatePoint();
        }

        protected override void SartQuestInnerActions(QuestItem item)
        {

            Debug.LogError(_book);
            if(_book != null)
            {
                _book.ResetItem();

                _book = null;
            }

            if(item.ItemType == QuestItemType.Book)
            {
                GrabbedCorrect = true;
            }
            else if(item.ItemType == QuestItemType.FakeBook)
            {
                GrabbedCorrect = false;
            }

            _book = item;
        }

        protected override void StartErrorEffect()
        {
            _book.ResetItem();

            _book = null;
        }

        protected override void StartQuestIntroduction()
        {
            QuestIsActive = true;

            ChainManager.Instance.RegisterNewChain();

            ChainManager.Instance.PlayAudio(AudioManager.Instance.AudioData.UseThisPortal);
            ChainManager.Instance.WaitUntil(1f);
            ChainManager.Instance.PlayAudio(AudioManager.Instance.AudioData.MagicCloset);

            ChainManager.Instance.FinishActions();
        }

        private void ReshuffleBooks()
        {
            if (_swapIndex == 0)
            {
                SwapPositions(_changingBooks[0].transform, _changingBooks[1].transform);
                _swapIndex++;
            }
            else if (_swapIndex == 1)
            {
                SwapPositions(_changingBooks[0].transform, _changingBooks[2].transform);
                _swapIndex++;
            }
            else if (_swapIndex == 2)
            {
                SwapPositions(_changingBooks[0].transform, _changingBooks[1].transform);
                SwapPositions(_changingBooks[1].transform, _changingBooks[2].transform);
                _swapIndex = 0; 
            }

            foreach(QuestItem book in _changingBooks)
            {
                book.UpdateDefaultPosition();
            }
        }

        void SwapPositions(Transform obj1, Transform obj2)
        {
            Vector3 tempPosition = obj1.position;
            obj1.position = obj2.position;
            obj2.position = tempPosition;
        }

        protected override void RemoveItem(QuestItem item)
        {
            if(item == _book)
            {
                item.ResetItem();
                _book = null;
            }
        }

        protected override void PartlyFinishQuestInnerActions()
        {
            
        }
    }
}
