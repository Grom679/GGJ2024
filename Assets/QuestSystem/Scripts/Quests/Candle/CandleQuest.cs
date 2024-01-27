using PuzzleGame.Audio;
using PuzzleGame.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace PuzzleGame.Quest
{
    public class CandleQuest : SimpleQuest
    {
        [SerializeField]
        private Transform _bug;
        [SerializeField]
        private PlayableDirector _director;
        [SerializeField] 
        private Texture2D _texture2D;
        [SerializeField] 
        private GameObject _puttenChandelier;
        
        protected override void FinishQuestInnerActions()
        {
            QuestPoint.DeactivatePoint();

            DisabledNeededPortals();

            Scenario.Instance.PortalManager.RemoveAdditionalActionOnPortal(PortalEnum.Floor, PortalEnum.Ceiling, EnterCeiling);
            Scenario.Instance.PortalManager.RemoveAdditionalActionOnPortal(PortalEnum.Ceiling, PortalEnum.Floor, ExitCeiling);
        }

        protected override void PartlyFinishQuestInnerActions()
        {
            _puttenChandelier.SetActive(true);
            StartCoroutine(AppearBug());
        }

        protected override void RemoveItem(QuestItem item)
        {
            item.ResetItem();
        }

        protected override void SartQuestInnerActions(QuestItem item)
        {
            if(item.ItemType == QuestItemType.Bug)
            {
                AudioManager.Instance.PlayClip(AudioManager.Instance.AudioData.HowCuteHeIs);
            }
            else if (item.ItemType == QuestItemType.Undercut)
            {
                AudioManager.Instance.PlayClip(AudioManager.Instance.AudioData.BurnHouse);
            }
        }

        protected override void StartErrorEffect()
        {
            
        }

        protected override void StartQuestIntroduction()
        {
            Scenario.Instance.InstructionMat.SetTexture("_BaseMap",_texture2D);
            _bug.localScale = new Vector3(0f, 0f, 0f);
            Scenario.Instance.PortalManager.ChangeMainPortal(PortalEnum.Ceiling);

            Scenario.Instance.PortalManager.SetAdditionalActionOnPortal(PortalEnum.Floor, PortalEnum.Ceiling, EnterCeiling);
            Scenario.Instance.PortalManager.SetAdditionalActionOnPortal(PortalEnum.Ceiling, PortalEnum.Floor, ExitCeiling);

            ChainManager.Instance.RegisterNewChain();

            ChainManager.Instance.PlayAudio(AudioManager.Instance.AudioData.IDidntExpect);
            ChainManager.Instance.WaitUntil(1f);
            ChainManager.Instance.PlayAudio(AudioManager.Instance.AudioData.StillDisgusting);
            ChainManager.Instance.WaitUntil(1f);
            ChainManager.Instance.PlayAudio(AudioManager.Instance.AudioData.LikeThis);
            ChainManager.Instance.WaitUntil(1f);
            ChainManager.Instance.PlayAudio(AudioManager.Instance.AudioData.AdditionalFromTheBook);
            ChainManager.Instance.PlayTimeLine(_director);
            ChainManager.Instance.WaitUntil(1f);
            ChainManager.Instance.PlayAudio(AudioManager.Instance.AudioData.OneCandle);
            ChainManager.Instance.Do(() => { Scenario.Instance.PortalManager.EnablePortal(PortalEnum.Floor, PortalEnum.Ceiling); });

            ChainManager.Instance.FinishActions();
        }

        public void EnterCeiling()
        {
            Scenario.Instance.PortalManager.EnablePortal(PortalEnum.Ceiling, PortalEnum.Floor);
            Scenario.Instance.PortalManager.DisablePortal(PortalEnum.Floor, PortalEnum.Ceiling);
        }

        public void ExitCeiling()
        {
            Scenario.Instance.PortalManager.DisablePortal(PortalEnum.Ceiling, PortalEnum.Floor);
            Scenario.Instance.PortalManager.EnablePortal(PortalEnum.Floor, PortalEnum.Ceiling);
        }

        private void DisabledNeededPortals()
        {
            Scenario.Instance.PortalManager.DisablePortal(PortalEnum.Floor, PortalEnum.Ceiling);
            Scenario.Instance.PortalManager.DisablePortal(PortalEnum.Ceiling, PortalEnum.Floor);
        }

        private IEnumerator AppearBug()
        {
            float x = 0, y = 0, z = 0;

            Rigidbody rigidbody = _bug.GetComponent<Rigidbody>();

            rigidbody.useGravity = false;

            while (_bug.localScale.x < 1.5f)
            {
                x += Time.deltaTime * 0.5f;
                y += Time.deltaTime * 0.5f;
                z += Time.deltaTime * 0.5f;

                _bug.localScale = new Vector3(x, y, z);

                yield return null;
            }

            rigidbody.useGravity = true;

            //Play sound
        }
    }
}
