using PuzzleGame.Audio;
using PuzzleGame.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PuzzleGame.Quest
{
    public class CandleQuest : SimpleQuest
    {
        [SerializeField]
        private Transform _bug;

        private void OnEnable()
        {
            _bug.gameObject.SetActive(false);
        }

        protected override void FinishQuestInnerActions()
        {
            QuestPoint.DeactivatePoint();

            DisabledNeededPortals();
        }

        protected override void PartlyFinishQuestInnerActions()
        {
            StartCoroutine(AppearBug());
        }

        protected override void RemoveItem(QuestItem item)
        {
            
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
            Scenario.Instance.PortalManager.ChangeMainPortal(PortalEnum.Ceiling);

            ChainManager.Instance.RegisterNewChain();

            ChainManager.Instance.PlayAudio(AudioManager.Instance.AudioData.IDidntExpect);
            ChainManager.Instance.WaitUntil(1f);
            ChainManager.Instance.PlayAudio(AudioManager.Instance.AudioData.StillDisgusting);
            ChainManager.Instance.WaitUntil(1f);
            ChainManager.Instance.PlayAudio(AudioManager.Instance.AudioData.LikeThis);
            ChainManager.Instance.WaitUntil(1f);
            ChainManager.Instance.PlayAudio(AudioManager.Instance.AudioData.AdditionalFromTheBook);
            ChainManager.Instance.WaitUntil(1f);
            ChainManager.Instance.PlayAudio(AudioManager.Instance.AudioData.OneCandle);
            ChainManager.Instance.Do(EnableNeededPortals);

            ChainManager.Instance.FinishActions();
        }

        private void EnableNeededPortals()
        {
            Scenario.Instance.PortalManager.EnablePortal(PortalEnum.Floor, PortalEnum.Ceiling);
            Scenario.Instance.PortalManager.EnablePortal(PortalEnum.Ceiling, PortalEnum.Floor);
        }

        private void DisabledNeededPortals()
        {
            Scenario.Instance.PortalManager.DisablePortal(PortalEnum.Floor, PortalEnum.Ceiling);
            Scenario.Instance.PortalManager.DisablePortal(PortalEnum.Ceiling, PortalEnum.Floor);
        }

        private IEnumerator AppearBug()
        {
            float x = 0, y = 0, z = 0;

            _bug.gameObject.SetActive(true);

            Rigidbody rigidbody = _bug.GetComponent<Rigidbody>();

            rigidbody.useGravity = false;

            while (_bug.localScale.x < 0.5f)
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
