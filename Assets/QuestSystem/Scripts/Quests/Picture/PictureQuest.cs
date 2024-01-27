using PuzzleGame.Audio;
using PuzzleGame.Core;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

namespace PuzzleGame.Quest
{
    public class PictureQuest : SimpleQuest
    {
        [SerializeField]
        private List<Picture> _pictures;
        [SerializeField] 
        private Texture2D _texture2D;

        protected override void FinishQuestInnerActions()
        {
            QuestPoint.DeactivatePoint();

            Scenario.Instance.PortalManager.RemoveAdditionalActionOnPortal(PortalEnum.Floor, PortalEnum.Fireplace, EnterPicture);
            Scenario.Instance.PortalManager.RemoveAdditionalActionOnPortal(PortalEnum.Fireplace, PortalEnum.Floor, ExitPicture);

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
            Scenario.Instance.InstructionMat.SetTexture("_BaseMap",_texture2D);
            Scenario.Instance.PortalManager.ChangeMainPortal(PortalEnum.Fireplace);

            Scenario.Instance.PortalManager.SetAdditionalActionOnPortal(PortalEnum.Floor, PortalEnum.Fireplace, EnterPicture);
            Scenario.Instance.PortalManager.SetAdditionalActionOnPortal(PortalEnum.Fireplace, PortalEnum.Floor, ExitPicture);

            ChainManager.Instance.RegisterNewChain();

            ChainManager.Instance.PlayAudio(AudioManager.Instance.AudioData.OSmellsDilicious);
            ChainManager.Instance.WaitUntil(1f);
            ChainManager.Instance.PlayAudio(AudioManager.Instance.AudioData.AlmoustImpossible);
            ChainManager.Instance.WaitUntil(1f);
            ChainManager.Instance.PlayAudio(AudioManager.Instance.AudioData.Clean);
            ChainManager.Instance.WaitUntil(1f);
            ChainManager.Instance.PlayAudio(AudioManager.Instance.AudioData.DifferentAngle);
            ChainManager.Instance.Do(() => { Scenario.Instance.PortalManager.EnablePortal(PortalEnum.Floor, PortalEnum.Fireplace); });

            ChainManager.Instance.FinishActions();
        }

        private void EnterPicture()
        {
            Scenario.Instance.PortalManager.EnablePortal(PortalEnum.Fireplace, PortalEnum.Floor);
            Scenario.Instance.PortalManager.DisablePortal(PortalEnum.Floor, PortalEnum.Fireplace);
        }

        private void ExitPicture()
        {
            Scenario.Instance.PortalManager.DisablePortal(PortalEnum.Fireplace, PortalEnum.Floor);
            Scenario.Instance.PortalManager.EnablePortal(PortalEnum.Floor, PortalEnum.Fireplace);

        }

        private void DisabledNeededPortals()
        {
            Scenario.Instance.PortalManager.DisablePortal(PortalEnum.Floor, PortalEnum.Fireplace);
            Scenario.Instance.PortalManager.DisablePortal(PortalEnum.Fireplace, PortalEnum.Floor);
        }
    }
}
