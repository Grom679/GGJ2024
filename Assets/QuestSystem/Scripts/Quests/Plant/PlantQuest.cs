using PuzzleGame.Audio;
using PuzzleGame.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace PuzzleGame.Quest
{
    public class PlantQuest : SimpleQuest
    {
        [SerializeField]
        private float _timeForError = 30f;
        [SerializeField]
        private float _minDistance = 5f;
        [SerializeField]
        private GameObject _helpPortal;
        [SerializeField]
        private InversePhysics _physics;
        [SerializeField]
        private Volume _volume;

        private QuestItem _plant;

        private bool _activatedPlant;
        private float _currentTime;

        private int _attemptNumber = 0;

        private float _volumeValue = 0.03f;

        private void Start()
        {
            _physics.enabled = false;
        }

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

                _volume.weight += _volumeValue * 0.0025f;

                Debug.Log(_volume.weight);
                Debug.Log(_volumeValue);

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

            Scenario.Instance.PortalManager.TeleportTo(PortalEnum.GreenHouse, PortalEnum.Floor);

            DisabledNeededPortals();

            _volume.enabled = false;
            _volume.weight = 0;
        }

        protected override void SartQuestInnerActions(QuestItem item)
        {
            if(item.ItemType == QuestItemType.Plant)
            {
                // Play strange sound

                if (_attemptNumber == 0)
                {
                    AudioManager.Instance.PlayClip(AudioManager.Instance.AudioData.GetRidOfThis);

                    DisableGreenPortal();
                }
                else
                {
                    AudioManager.Instance.PlayClip(AudioManager.Instance.AudioData.UsePhysics);
                }

                _attemptNumber++;

                _plant = item;

                _activatedPlant = true;

                _volume.enabled = true;
                _volume.weight = 0;
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

            Scenario.Instance.PortalManager.TeleportTo(PortalEnum.Floor, PortalEnum.GreenHouse);

            _physics.enabled = false;

            _physics.UseInverseGravity = false;

            _activatedPlant = false;

            _plant.ResetItem();

            _plant = null;

            _volume.enabled = false;
            _volume.weight = 0;
        }

        protected override void StartQuestIntroduction()
        {
            Scenario.Instance.PortalManager.ChangeMainPortal(PortalEnum.GreenHouse);

            ChainManager.Instance.RegisterNewChain();

            ChainManager.Instance.PlayAudio(AudioManager.Instance.AudioData.PlantMonologue);
            ChainManager.Instance.WaitUntil(1f);
            ChainManager.Instance.PlayAudio(AudioManager.Instance.AudioData.BookIllustration);
            ChainManager.Instance.WaitUntil(1f);
            ChainManager.Instance.PlayAudio(AudioManager.Instance.AudioData.HaveFun);
            ChainManager.Instance.Do(EnableNeededPortals);

            ChainManager.Instance.FinishActions();

            _plant = null;
        }

        private void EnableNeededPortals()
        {
            Scenario.Instance.PortalManager.EnablePortal(PortalEnum.Floor, PortalEnum.GreenHouse);
            Scenario.Instance.PortalManager.EnablePortal(PortalEnum.GreenHouse, PortalEnum.Floor);
        }

        private void DisableGreenPortal()
        {
            Scenario.Instance.PortalManager.DisablePortal(PortalEnum.GreenHouse, PortalEnum.Floor);

            _helpPortal.SetActive(true);

            Scenario.Instance.PortalManager.EnablePortal(PortalEnum.GreenHouse, PortalEnum.Ceiling);

            Scenario.Instance.PortalManager.SetAdditionalActionOnPortal(PortalEnum.Ceiling, () => { _physics.UseInverseGravity = true; });
        }

        private void DisabledNeededPortals()
        {
            Scenario.Instance.PortalManager.DisablePortal(PortalEnum.Floor, PortalEnum.GreenHouse);
            Scenario.Instance.PortalManager.DisablePortal(PortalEnum.GreenHouse, PortalEnum.Floor);
            Scenario.Instance.PortalManager.DisablePortal(PortalEnum.GreenHouse, PortalEnum.Ceiling);
        }

        protected override void RemoveItem(QuestItem item)
        {
            if(item.ItemType == QuestItemType.Plant)
            {
                _physics.enabled = true;
            }
        }

        protected override void PartlyFinishQuestInnerActions()
        {
            
        }
    }
}
