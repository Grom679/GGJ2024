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

        private void Awake()
        {
            DontDestroyOnLoad(this);

            Instance = this;
        }
    }
}

