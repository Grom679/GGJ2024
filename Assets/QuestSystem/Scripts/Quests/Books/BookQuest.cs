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
                if(_book == null)
                {
                    ReshuffleBooks();
                }

                Scenario.Instance.PortalManager.EnablePortal(PortalEnum.Library, PortalEnum.Floor);
                Scenario.Instance.PortalManager.DisablePortal(PortalEnum.Floor, PortalEnum.Library);

                Scenario.Instance.Player.OnBookQuestChange?.Invoke();
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

                Scenario.Instance.PortalManager.DisablePortal(PortalEnum.Library, PortalEnum.Floor);
                Scenario.Instance.PortalManager.EnablePortal(PortalEnum.Floor, PortalEnum.Library);

                Scenario.Instance.Player.OnBookQuestChange?.Invoke();
            } 
        }

        protected override void FinishQuestInnerActions()
        {
            QuestIsActive = false;
            QuestPoint.DeactivatePoint();

            DisabledNeededPortals();
            Scenario.Instance.PortalManager.RemoveAdditionalActionOnPortal(PortalEnum.Floor, PortalEnum.Library, EnterBookWall);
            Scenario.Instance.PortalManager.RemoveAdditionalActionOnPortal(PortalEnum.Library,PortalEnum.Floor, ExitBookWall);
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

            Scenario.Instance.PortalManager.ChangeMainPortal(PortalEnum.Library);
            Scenario.Instance.PortalManager.SetAdditionalActionOnPortal(PortalEnum.Floor, PortalEnum.Library, EnterBookWall);
            Scenario.Instance.PortalManager.SetAdditionalActionOnPortal(PortalEnum.Library,PortalEnum.Floor, ExitBookWall);
            
            ChainManager.Instance.RegisterNewChain();

            // ChainManager.Instance.PlayAudio(AudioManager.Instance.AudioData.UseThisPortal);
            // ChainManager.Instance.WaitUntil(1f);
            ChainManager.Instance.Do(() => { Scenario.Instance.PortalManager.EnablePortal(PortalEnum.Floor, PortalEnum.Library); });
            ChainManager.Instance.PlayAudio(AudioManager.Instance.AudioData.MagicCloset);

            ChainManager.Instance.FinishActions();
        }

        private void DisabledNeededPortals()
        {
            Scenario.Instance.PortalManager.DisablePortal(PortalEnum.Floor, PortalEnum.Library);
            Scenario.Instance.PortalManager.DisablePortal(PortalEnum.Library, PortalEnum.Floor);
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
