using PuzzleGame.Quest;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PuzzleGame.Core
{
    public class GlobalEvents : MonoBehaviour
    {
        public static GlobalEvents Instance { get; private set; }

        public Action OnPartlyFinished;

        public Action OnQuestError;

        public Action OnQuestFinished;

        public Action OnChainFinished;

        public Action OnChainStarted;

        public Action<QuestItem> OnResetItem;

        private void Awake()
        {
            DontDestroyOnLoad(this);

            if (Instance != null && Instance != this)
            {
                Destroy(this);
            }
            else
            {
                Instance = this;
            }
        }
    }
}

