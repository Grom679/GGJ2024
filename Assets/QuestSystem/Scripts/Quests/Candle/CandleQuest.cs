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

        protected override void FinishQuestInnerActions()
        {
            QuestPoint.DeactivatePoint();
        }

        protected override void PartlyFinishQuestInnerActions()
        {
            //StartCoroutine(AppearBug());
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

            ChainManager.Instance.FinishActions();
        }

        private IEnumerator AppearBug()
        {
            float x = 0, y = 0, z = 0;

            while(_bug.localScale.x < 0.9f)
            {
                x += Time.deltaTime;
                y += Time.deltaTime;
                z += Time.deltaTime;

                _bug.localScale = new Vector3(x, y, z);

                yield return null;
            }

            //Play sound
        }
    }
}
