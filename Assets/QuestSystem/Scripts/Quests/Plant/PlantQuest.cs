using PuzzleGame.Audio;
using PuzzleGame.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PuzzleGame.Quest
{
    public class PlantQuest : SimpleQuest
    {
        [SerializeField]
        private float _timeForError = 30f;
        [SerializeField]
        private float _minDistance = 5f;

        private QuestItem _plant;

        private bool _activatedPlant;
        private float _currentTime;

        private int _attemptNumber = 0;

        private void Update()
        {
            if(_activatedPlant)
            {
                if(_currentTime >= _timeForError)
                {
                    _activatedPlant = false;
                    _currentTime = 0f;
                    GlobalEvents.Instance.OnQuestError?.Invoke();

                    return;
                }
                
                _currentTime += Time.deltaTime;
                Debug.Log(_currentTime);

                if (Vector3.Distance(_plant.transform.position, QuestPoint.transform.position) <= _minDistance)
                {
                    _activatedPlant = false;

                    QuestPoint.PutQuestItem(_plant);
                }
            }
        }

        protected override void FinishQuestInnerActions()
        {
            QuestPoint.DeactivatePoint();

            _attemptNumber = 0;
        }

        protected override void SartQuestInnerActions(QuestItem item)
        {
            if(item.ItemType == QuestItemType.Plant)
            {
                // Play strange sound

                if (_attemptNumber == 0)
                {
                    AudioManager.Instance.PlayClip(AudioManager.Instance.AudioData.GetRidOfThis);
                }
                else
                {
                    AudioManager.Instance.PlayClip(AudioManager.Instance.AudioData.UsePhysics);
                }

                _attemptNumber++;

                _plant = item;

                _activatedPlant = true;
            }
            else if(item.ItemType == QuestItemType.FakePlant)
            {
                //No voice
                AudioManager.Instance.PlayClip(AudioManager.Instance.AudioData.NotThisPlant);

                item.ResetItem();
            }
        }

        protected override void StartErrorEffect()
        {
            //Teleport user turn off teleports

            _activatedPlant = false;

            _plant.ResetItem();

            _plant = null;
        }

        protected override void StartQuestIntroduction()
        {
            ChainManager.Instance.RegisterNewChain();

            ChainManager.Instance.PlayAudio(AudioManager.Instance.AudioData.PlantMonologue);
            ChainManager.Instance.WaitUntil(1f);
            ChainManager.Instance.PlayAudio(AudioManager.Instance.AudioData.BookIllustration);
            ChainManager.Instance.WaitUntil(1f);
            ChainManager.Instance.PlayAudio(AudioManager.Instance.AudioData.HaveFun);

            ChainManager.Instance.FinishActions();

            _plant = null;
        }

        protected override void RemoveItem(QuestItem item)
        {
            
        }

        protected override void PartlyFinishQuestInnerActions()
        {
            
        }
    }
}
