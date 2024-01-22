using PuzzleGame.Core;
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

        private SimpleQuest _currentQuest;

        private int _currentIndex = 0;

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            GlobalEvents.Instance.OnPartlyFinished += OnPartlyFinished;

            GlobalEvents.Instance.OnQuestError += OnQuestError;

            GlobalEvents.Instance.OnQuestFinished += OnQuestFinished;

            GlobalEvents.Instance.OnChainFinished += OnChainFinished;

            GlobalEvents.Instance.OnChainStarted += OnChainStarted;
        }

        public void PlayFirstQuest()
        {
            PlayQuest();
        }

        private void OnPartlyFinished()
        {
            Debug.Log("Partly finished");   
        }

        private void OnQuestError()
        {

        }

        private void OnQuestFinished()
        {
            _currentIndex++;

            Debug.Log("finished");
           //Play audio with chain manger
        }

        private void OnChainFinished()
        {

        }

        private void OnChainStarted()
        {

        }

        private void PlayQuest()
        {
            if(_currentIndex < _quests.Count)
            {
                _currentQuest = _quests[_currentIndex];
                _currentQuest.StartQuest();
            }
            else
            {
                Debug.Log("Sceenario Ends");
            }
        }
    }
}
