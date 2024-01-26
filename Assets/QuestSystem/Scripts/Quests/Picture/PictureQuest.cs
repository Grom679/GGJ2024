using PuzzleGame.Audio;
using PuzzleGame.Core;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

namespace PuzzleGame.Quest
{
    public class PictureQuest : SimpleQuest
    {
        [SerializeField]
        private List<Picture> _pictures;

        protected override void FinishQuestInnerActions()
        {
            QuestPoint.DeactivatePoint();

            DisabledNeededPortals();
        }

        protected override void PartlyFinishQuestInnerActions()
        {
            foreach (var picture in _pictures) 
            {
                picture.SwapImage();
            }
        }

        protected override void RemoveItem(QuestItem item)
        {
            item.ResetItem();
        }

        protected override void SartQuestInnerActions(QuestItem item)
        {
            if(item.ItemType == QuestItemType.Glasses)
            {
                QuestPoint.PutQuestItem(item);
            }
            else if(item.ItemType == QuestItemType.Picture)
            {
                AudioManager.Instance.PlayClip(AudioManager.Instance.AudioData.MyGrandFather);
            }
        }

        protected override void StartErrorEffect()
        {

        }

        protected override void StartQuestIntroduction()
        {
            foreach (var picture in _pictures)
            {
                picture.MakeUngrabble();
            }

            Scenario.Instance.PortalManager.ChangeMainPortal(PortalEnum.Fireplace);

            ChainManager.Instance.RegisterNewChain();

            ChainManager.Instance.PlayAudio(AudioManager.Instance.AudioData.OSmellsDilicious);
            ChainManager.Instance.WaitUntil(1f);
            ChainManager.Instance.PlayAudio(AudioManager.Instance.AudioData.AlmoustImpossible);
            ChainManager.Instance.WaitUntil(1f);
            ChainManager.Instance.PlayAudio(AudioManager.Instance.AudioData.Clean);
            ChainManager.Instance.WaitUntil(1f);
            ChainManager.Instance.PlayAudio(AudioManager.Instance.AudioData.DifferentAngle);
            ChainManager.Instance.Do(EnableNeededPortals);

            ChainManager.Instance.FinishActions();
        }

        private void EnableNeededPortals()
        {
            Scenario.Instance.PortalManager.EnablePortal(PortalEnum.Floor, PortalEnum.Fireplace);
            Scenario.Instance.PortalManager.EnablePortal(PortalEnum.Fireplace, PortalEnum.Floor);
        }

        private void DisabledNeededPortals()
        {
            Scenario.Instance.PortalManager.DisablePortal(PortalEnum.Floor, PortalEnum.Fireplace);
            Scenario.Instance.PortalManager.DisablePortal(PortalEnum.Fireplace, PortalEnum.Floor);
        }
    }
}
