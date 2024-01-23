using PuzzleGame.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PuzzleGame.Quest
{
    public abstract class SimpleQuest : MonoBehaviour, IQuest
    {
        public QuestTypes QuestType => _questType;

        public QuestPoint QuestPoint => _questPoint;

        public bool NeedEffectOnError;

        [SerializeField]
        private QuestPoint _questPoint;

        [SerializeField]
        private QuestTypes _questType;

        public void MakeErrorEffect()
        {
            StartErrorEffect();
        }

        public void ActivateQuestItem(QuestItem item)
        {
            item.ActivateQuestItem();

            SartQuestInnerActions(item);
        }

        public void FinishQuest()
        {
            // Finish quest
            FinishQuestInnerActions();
        }

        public void StartQuest()
        {
            StartQuestIntroduction();

            _questPoint.ActivatePoint();
        }

        protected abstract void StartQuestIntroduction();

        protected abstract void SartQuestInnerActions(QuestItem item);

        protected abstract void FinishQuestInnerActions();

        protected abstract void StartErrorEffect();
    }
}
