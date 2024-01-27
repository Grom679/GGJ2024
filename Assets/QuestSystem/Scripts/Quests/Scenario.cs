using PuzzleGame.Audio;
using PuzzleGame.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace PuzzleGame.Quest
{
    public class Scenario : MonoBehaviour
    {
        public static Scenario Instance { get; private set; }

        public SimpleQuest CurrentQuest => _currentQuest;

        public PortalManager PortalManager => _portalManager;
        
        public Material InstructionMat => _instructionMat;

        public GameObject Explosion => _explosion;
        public Player Player => _player;

        [SerializeField]
        private List<SimpleQuest> _quests;
        [SerializeField]
        private PortalManager _portalManager;
        [SerializeField]
        private Player _player;
        [SerializeField]
        private float _timeToStart;
        [SerializeField] 
        private Material _instructionMat;
        [SerializeField]
        private PlayableDirector _director;
        [SerializeField]
        private GameObject _explosion;

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

            _currentQuest.FinishQuest();

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

                    _currentQuest.FinishQuest();

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

                ChainManager.Instance.RegisterNewChain();

                ChainManager.Instance.PlayTimeLine(_director);
                ChainManager.Instance.Do(DoFinals);

                ChainManager.Instance.FinishActions();
            }
        }

        private void DoFinals()
        {

        }
    }
}
