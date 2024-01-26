using PuzzleGame.Audio;
using PuzzleGame.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PuzzleGame.Quest
{
    public class FlaskQuest : SimpleQuest
    {
        private QuestItem _flask;

        protected override void FinishQuestInnerActions()
        {
            QuestPoint.DeactivatePoint();
            DisabledNeededPortals();
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
                AudioManager.Instance.PlayClip(AudioManager.Instance.AudioData.SoThisFlask);
            }
            else if (item.ItemType == QuestItemType.Flask)
            {
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
            Scenario.Instance.PortalManager.ChangeMainPortal(PortalEnum.Laboratory);
            
            ChainManager.Instance.RegisterNewChain();
            //
            // ChainManager.Instance.PlayAudio(AudioManager.Instance.AudioData.NextStone);
            // ChainManager.Instance.WaitUntil(1f);
            // ChainManager.Instance.PlayAudio(AudioManager.Instance.AudioData.GoToLab);
            // ChainManager.Instance.WaitUntil(1f);
            // ChainManager.Instance.PlayAudio(AudioManager.Instance.AudioData.FindFlask);
            // ChainManager.Instance.WaitUntil(1f);
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
