using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PuzzleGame.Quest
{
    public class PreparationQuest : SimpleQuest
    {
        [SerializeField]
        private float _middleFireDistance = 2f;

        private bool _useDistance;

        public Transform player; 

        private void Update()
        {
            if (_useDistance) 
            {
                if(Vector3.Distance(player.position, QuestPoint.transform.position) <= _middleFireDistance)
                {
                    _useDistance = false;
                    Debug.Log("Just one more step.");
                }
            }
        }

        protected override void StartQuestIntroduction()
        {
            Debug.Log("Started preporation quest");
            // start chain
        }

        protected override void FinishQuestInnerActions()
        {
            QuestPoint.DeactivatePoint();
        }

        protected override void SartQuestInnerActions(QuestItem item)
        {
            if(item.ItemType == QuestItemType.Bucket)
            {
                Debug.Log("Really you want to put it into the ...?");
            }
            else if (item.ItemType == QuestItemType.Dynamite)
            {
                Debug.Log("No, you can't just destroy my house.");

                _useDistance = true;
            }
        }

        protected override void StartErrorEffect()
        {
            
        }
    }
}
