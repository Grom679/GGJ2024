using System;
using PuzzleGame.Audio;
using PuzzleGame.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace PuzzleGame.Quest
{
    public class PreparationQuest : SimpleQuest
    {
        [SerializeField]
        private float _middleFireDistance = 2f;
        [SerializeField]
        private PlayableDirector _director;

        [SerializeField] 
        private Item _bucket;
        
        [SerializeField] 
        private Item _dynamite;
        [SerializeField] 
        private GameObject _fireEffect;
        [SerializeField] 
        
        private GameObject _sound;

        private bool _useDistance;

        private Transform _player;

        private void Start()
        {
            _player = Scenario.Instance.Player.transform;
        }

        private void Update()
        {
            if (_useDistance) 
            {
                if(Vector3.Distance(_player.position, QuestPoint.transform.position) <= _middleFireDistance)
                {
                    _useDistance = false;

                    AudioManager.Instance.PlayClip(AudioManager.Instance.AudioData.OneStepWarn);
                }
            }
        }

        protected override void StartQuestIntroduction()
        {
            _dynamite.gameObject.SetActive(false);
            _bucket.MakeGrabble();
            ChainManager.Instance.RegisterNewChain();

            ChainManager.Instance.WaitUntil(2f);
            ChainManager.Instance.PlayTimeLine(_director);
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
            _fireEffect.SetActive(true);
            _sound.SetActive(true);
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
            item.ResetItem();
        }

        protected override void PartlyFinishQuestInnerActions()
        {
            _dynamite.gameObject.SetActive(true);
            _dynamite.MakeGrabble();
        }
    }
}
