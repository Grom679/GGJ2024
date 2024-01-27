using PuzzleGame.Audio;
using PuzzleGame.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PuzzleGame.Quest
{
    public class FlaskQuest : SimpleQuest
    {
        [SerializeField]
        private Transform _player;
        [SerializeField]
        private Vector3 _scaleValue;

        private QuestItem _flask;

        protected override void FinishQuestInnerActions()
        {
            Scenario.Instance.PortalManager.RemoveAdditionalActionOnPortal(PortalEnum.Floor, PortalEnum.Laboratory, EnterLaboratory);
            Scenario.Instance.PortalManager.RemoveAdditionalActionOnPortal(PortalEnum.Laboratory, PortalEnum.Floor, ExitLaboratory);

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
            if (item.ItemType == QuestItemType.FakeFlask)
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

            Scenario.Instance.PortalManager.SetAdditionalActionOnPortal(PortalEnum.Floor, PortalEnum.Laboratory, EnterLaboratory);
            Scenario.Instance.PortalManager.SetAdditionalActionOnPortal(PortalEnum.Laboratory, PortalEnum.Floor, ExitLaboratory);

            ChainManager.Instance.RegisterNewChain();
            //
            // ChainManager.Instance.PlayAudio(AudioManager.Instance.AudioData.NextStone);
            // ChainManager.Instance.WaitUntil(1f);
            // ChainManager.Instance.PlayAudio(AudioManager.Instance.AudioData.GoToLab);
            // ChainManager.Instance.WaitUntil(1f);
            // ChainManager.Instance.PlayAudio(AudioManager.Instance.AudioData.FindFlask);
            // ChainManager.Instance.WaitUntil(1f);
            ChainManager.Instance.PlayAudio(AudioManager.Instance.AudioData.GetItSorted);
            ChainManager.Instance.Do(() => { Scenario.Instance.PortalManager.EnablePortal(PortalEnum.Floor, PortalEnum.Laboratory); } );
            
            ChainManager.Instance.FinishActions();
        }

        private void EnterLaboratory()
        {
            Scenario.Instance.PortalManager.EnablePortal(PortalEnum.Laboratory, PortalEnum.Floor);
            Scenario.Instance.PortalManager.DisablePortal(PortalEnum.Floor, PortalEnum.Laboratory);

            _player.localScale = _scaleValue;
        }

        private void ExitLaboratory() 
        {
            Scenario.Instance.PortalManager.DisablePortal(PortalEnum.Laboratory, PortalEnum.Floor);
            Scenario.Instance.PortalManager.EnablePortal(PortalEnum.Floor, PortalEnum.Laboratory);

            _player.localScale = Vector3.one;
        }
        
        private void DisabledNeededPortals()
        {
            Scenario.Instance.PortalManager.DisablePortal(PortalEnum.Floor, PortalEnum.Laboratory);
            Scenario.Instance.PortalManager.DisablePortal(PortalEnum.Laboratory, PortalEnum.Floor);
        }
    }
}
