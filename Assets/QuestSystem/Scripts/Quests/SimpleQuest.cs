using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PuzzleGame.Quest
{
    public class SimpleQuest : MonoBehaviour, IQuest
    {
        [SerializeField]
        private QuestPoint _questPoint;

        public void ActivateQuestItem(QuestItem item)
        {
            item.ActivateQuestItem();
        }

        public void FinishQuest()
        {
            // Finish quest
        }

        public void StartQuest()
        {
            StartQuestIntroduction();

            _questPoint.ActivatePoint();
        }

        protected virtual void StartQuestIntroduction()
        {
            //Start Intro, init action chain
        }
    }
}
