using PuzzleGame.Audio;
using PuzzleGame.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PuzzleGame.Quest
{
    public class Scenario : MonoBehaviour
    {
        public static Scenario Instance { get; private set; }

        public SimpleQuest CurrentQuest => _currentQuest;

        [SerializeField]
        private List<SimpleQuest> _quests;
        [SerializeField]
        private float _timeToStart;

        private SimpleQuest _currentQuest;

        private int _currentIndex = 0;

        private int _attemptsCount;

        private void Awake()
        {
            Instance = this;
        }

        private void OnEnable()
        {
            GlobalEvents.Instance.OnPartlyFinished += OnPartlyFinished;

            GlobalEvents.Instance.OnQuestError += OnQuestError;

            GlobalEvents.Instance.OnQuestFinished += OnQuestFinished;

            GlobalEvents.Instance.OnChainFinished += OnChainFinished;

            GlobalEvents.Instance.OnChainStarted += OnChainStarted;
        }

        private void OnDisable()
        {
            GlobalEvents.Instance.OnPartlyFinished -= OnPartlyFinished;

            GlobalEvents.Instance.OnQuestError -= OnQuestError;

            GlobalEvents.Instance.OnQuestFinished -= OnQuestFinished;

            GlobalEvents.Instance.OnChainFinished -= OnChainFinished;

            GlobalEvents.Instance.OnChainStarted -= OnChainStarted;
        }

        private void Start()
        {
            StartCoroutine(StartScenario());
        }

        public void PlayFirstQuest()
        {
            PlayQuest();
        }

        private IEnumerator StartScenario()
        {
            yield return new WaitForSeconds(_timeToStart);

            PlayFirstQuest();
        }

        private void OnPartlyFinished()
        {
            Debug.Log("Partly finished");

            AudioManager.Instance.PlayPartlyFinished(_currentQuest.QuestType);

            _currentQuest.PartlyFinishQuest();
        }

        private void OnQuestError()
        {
            if (_currentQuest.UseAttemptMechanics)
            {
                if (_currentQuest.MaxAttemptCount <= _attemptsCount)
                {
                    FinishWithAttempts();
                }
                else
                {
                    _attemptsCount++;
                    MakeErrorActions();
                }
            }
            else
            {
                MakeErrorActions();
            }
        }

        private void MakeErrorActions()
        {
            AudioManager.Instance.PlayRandomErrorAudio(_currentQuest.QuestType);

            _currentQuest.MakeErrorEffect();
        }

        private void OnQuestFinished()
        {
            _currentIndex++;

            AudioManager.Instance.PlayFinishQuestAudio(_currentQuest.QuestType);

            StartCoroutine(WaitForNext(PlayQuest));

            Debug.Log("finished");
           //Play audio with chain manger
        }

        private void OnChainFinished()
        {

        }

        private void OnChainStarted()
        {

        }

        private void FinishWithAttempts()
        {
            _currentIndex++;

            switch (_currentQuest.QuestType)
            {
                case QuestTypes.Flask:

                    AudioManager.Instance.PlayClip(AudioManager.Instance.AudioData.IllAddItFor);

                    StartCoroutine(WaitForNext(PlayQuest));

                    break;
            }
        }

        private IEnumerator WaitForNext(Action action)
        {
            while(AudioManager.Instance.VOSource.isPlaying)
            {
                yield return null;
            }

            action?.Invoke();
        }

        private void PlayQuest()
        {
            if(_currentIndex < _quests.Count)
            {
                _currentQuest = _quests[_currentIndex];
                _currentQuest.StartQuest();
                _attemptsCount = 0;
            }
            else
            {
                _attemptsCount = 0;
                Debug.Log("Sceenario Ends");
            }
        }
    }
}
