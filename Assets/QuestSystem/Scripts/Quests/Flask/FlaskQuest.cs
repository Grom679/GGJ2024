using PuzzleGame.Audio;
using PuzzleGame.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PuzzleGame.Quest
{
    public class FlaskQuest : SimpleQuest
    {
        public bool QuestIsActive { get; private set; }

        public bool GrabbedCorrect { get; private set; }
        private QuestItem _flask;
        
        public void ExitLaboratory() 
        {
            Debug.LogError("ExitLaboratory");
            if (QuestIsActive) 
            {
                if (!GrabbedCorrect && _flask)
                {
                    GlobalEvents.Instance.OnQuestError?.Invoke();
                }
            } 
        }

        protected override void FinishQuestInnerActions()
        {
            QuestIsActive = false;
            QuestPoint.DeactivatePoint();
            
            DisabledNeededPortals();
            Scenario.Instance.PortalManager.SetAdditionalActionOnPortal(PortalEnum.Laboratory,PortalEnum.Floor, ExitLaboratory);
        }

        protected override void PartlyFinishQuestInnerActions()
        {
            
        }

        protected override void RemoveItem(QuestItem item)
        {
            if(item == _flask)
            {
                item.ResetItem();
                _flask = null;
            }
        }

        protected override void SartQuestInnerActions(QuestItem item)
        {
            Debug.LogError(_flask);
            if(_flask != null)
            {
                _flask.ResetItem();

                _flask = null;
            }
            
            if(item.ItemType == QuestItemType.FakeFlask)
            {
                GrabbedCorrect = false;
                AudioManager.Instance.PlayClip(AudioManager.Instance.AudioData.SoThisFlask);
            }
            else if (item.ItemType == QuestItemType.Flask)
            {
                GrabbedCorrect = true;
                AudioManager.Instance.PlayClip(AudioManager.Instance.AudioData.DestoyingEnough);
            }
            
            _flask = item;
        }

        protected override void StartErrorEffect()
        {
            _flask.ResetItem();

            _flask = null;
        }

        protected override void StartQuestIntroduction()
        {
            QuestIsActive = true;
            Scenario.Instance.PortalManager.ChangeMainPortal(PortalEnum.Laboratory);
            Scenario.Instance.PortalManager.SetAdditionalActionOnPortal(PortalEnum.Laboratory,PortalEnum.Floor, ExitLaboratory);
            
            ChainManager.Instance.RegisterNewChain();

            ChainManager.Instance.PlayAudio(AudioManager.Instance.AudioData.NextStone);
            ChainManager.Instance.WaitUntil(1f);
            ChainManager.Instance.PlayAudio(AudioManager.Instance.AudioData.GoToLab);
            ChainManager.Instance.WaitUntil(1f);
            ChainManager.Instance.PlayAudio(AudioManager.Instance.AudioData.FindFlask);
            ChainManager.Instance.WaitUntil(1f);
            ChainManager.Instance.PlayAudio(AudioManager.Instance.AudioData.GetItSorted);
            ChainManager.Instance.Do(EnableNeededPortals);

            ChainManager.Instance.FinishActions();
        }
        
        private void EnableNeededPortals()
        {
            Scenario.Instance.PortalManager.EnablePortal(PortalEnum.Floor, PortalEnum.Laboratory);
            Scenario.Instance.PortalManager.EnablePortal(PortalEnum.Laboratory, PortalEnum.Floor);
        }
        
        private void DisabledNeededPortals()
        {
            Scenario.Instance.PortalManager.DisablePortal(PortalEnum.Floor, PortalEnum.Laboratory);
            Scenario.Instance.PortalManager.DisablePortal(PortalEnum.Laboratory, PortalEnum.Floor);
        }
    }
}
