using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PuzzleGame.Quest
{
    public class CandleQuest : SimpleQuest
    {
        [SerializeField]
        private Transform _bug;

        protected override void FinishQuestInnerActions()
        {
            QuestPoint.DeactivatePoint();
        }

        protected override void PartlyFinishQuestInnerActions()
        {
            StartCoroutine(AppearBug());
        }

        protected override void RemoveItem(QuestItem item)
        {
            
        }

        protected override void SartQuestInnerActions(QuestItem item)
        {
            if(item.ItemType == QuestItemType.Bug)
            {

            }
            else if (item.ItemType == QuestItemType.Undercut)
            {

            }
        }

        protected override void StartErrorEffect()
        {
            
        }

        protected override void StartQuestIntroduction()
        {
            
        }

        private IEnumerator AppearBug()
        {
            float x = 0, y = 0, z = 0;

            while(_bug.localScale.x < 0.9f)
            {
                x += Time.deltaTime;
                y += Time.deltaTime;
                z += Time.deltaTime;

                _bug.localScale = new Vector3(x, y, z);

                yield return null;
            }

            //Play sound
        }
    }
}
