using PuzzleGame.Audio;
using PuzzleGame.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PuzzleGame.Quest
{
    public class PreparationQuest : SimpleQuest
    {
        [SerializeField]
        private float _middleFireDistance = 2f;

        private bool _useDistance;

        public Transform player; 

        private void Update()
        {
            if (_useDistance) 
            {
                if(Vector3.Distance(player.position, QuestPoint.transform.position) <= _middleFireDistance)
                {
                    _useDistance = false;

                    AudioManager.Instance.PlayClip(AudioManager.Instance.AudioData.OneStepWarn);
                }
            }
        }

        protected override void StartQuestIntroduction()
        {
            ChainManager.Instance.RegisterNewChain();

            ChainManager.Instance.PlayAudio(AudioManager.Instance.AudioData.WellWellWell);
            ChainManager.Instance.WaitUntil(1f);
            ChainManager.Instance.PlayAudio(AudioManager.Instance.AudioData.NeverLeave);
            ChainManager.Instance.PlayAudio(AudioManager.Instance.AudioData.PinkPonies);
            ChainManager.Instance.PlayAudio(AudioManager.Instance.AudioData.NeedPotion);
            ChainManager.Instance.WaitUntil(2f);
            ChainManager.Instance.PlayAudio(AudioManager.Instance.AudioData.PrepareCauldron);

            ChainManager.Instance.FinishActions();

            Debug.Log("Started preporation quest");
            // start chain
        }

        protected override void FinishQuestInnerActions()
        {
            QuestPoint.DeactivatePoint();
        }

        protected override void SartQuestInnerActions(QuestItem item)
        {
            if(item.ItemType == QuestItemType.Bucket)
            {
                AudioManager.Instance.PlayClip(AudioManager.Instance.AudioData.ReallyYouWantToPut);
            }
            else if (item.ItemType == QuestItemType.Dynamite)
            {
                AudioManager.Instance.PlayClip(AudioManager.Instance.AudioData.YouCantDestroy);

                _useDistance = true;
            }
        }

        protected override void StartErrorEffect()
        {
            
        }

        protected override void RemoveItem(QuestItem item)
        {
            
        }

        protected override void PartlyFinishQuestInnerActions()
        {
            
        }
    }
}
