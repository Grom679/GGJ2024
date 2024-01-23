using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PuzzleGame.Quest
{
    public enum QuestTypes
    {
        Preparation,
        Book
    }

    public interface IQuest
    {
        public void StartQuest();

        public void ActivateQuestItem(QuestItem item);

        public void FinishQuest();
    }
}

