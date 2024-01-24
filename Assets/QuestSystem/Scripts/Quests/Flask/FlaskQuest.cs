using PuzzleGame.Audio;
using PuzzleGame.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PuzzleGame.Quest
{
    public class FlaskQuest : SimpleQuest
    {
        protected override void FinishQuestInnerActions()
        {
            QuestPoint.DeactivatePoint();
        }

        protected override void PartlyFinishQuestInnerActions()
        {
            
        }

        protected override void RemoveItem(QuestItem item)
        {
            
        }

        protected override void SartQuestInnerActions(QuestItem item)
        {
            if(item.ItemType == QuestItemType.FakeFlask)
            {
                AudioManager.Instance.PlayClip(AudioManager.Instance.AudioData.SoThisFlask);
            }
            else if (item.ItemType == QuestItemType.Flask)
            {
                AudioManager.Instance.PlayClip(AudioManager.Instance.AudioData.DestoyingEnough);
            }
        }

        protected override void StartErrorEffect()
        {
            
        }

        protected override void StartQuestIntroduction()
        {
            ChainManager.Instance.RegisterNewChain();

            ChainManager.Instance.PlayAudio(AudioManager.Instance.AudioData.NextStone);
            ChainManager.Instance.WaitUntil(1f);
            ChainManager.Instance.PlayAudio(AudioManager.Instance.AudioData.GoToLab);
            ChainManager.Instance.WaitUntil(1f);
            ChainManager.Instance.PlayAudio(AudioManager.Instance.AudioData.FindFlask);
            ChainManager.Instance.WaitUntil(1f);
            ChainManager.Instance.PlayAudio(AudioManager.Instance.AudioData.GetItSorted);

            ChainManager.Instance.FinishActions();
        }
    }
}
