using PuzzleGame.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PuzzleGame.Quest
{
    public class QuestPoint : MonoBehaviour
    {
        [SerializeField]
        private List<QuestItemType> _questItems;

        private int _usedItemsNumber;

        public void ActivatePoint()
        {
           
        }

        public void DeactivatePoint() 
        {
            this.enabled = false;
        }

        public void PutQuestItem(QuestItem item)
        {
            if(CheckItemType(item))
            {
                _usedItemsNumber++;

                item.UseItem();

                if(_usedItemsNumber >= _questItems.Count)
                {
                    GlobalEvents.Instance.OnQuestFinished?.Invoke();
                }
                else
                {
                    GlobalEvents.Instance.OnPartlyFinished?.Invoke();
                }
            }
            else
            {
                item.ResetItem();

                GlobalEvents.Instance.OnQuestError?.Invoke();
            }
        }

        private bool CheckItemType(QuestItem item)
        {
            bool includes = true;

            if (!_questItems.Contains(item.ItemType))
            {
                includes = false;
            }

            return includes;
        }
    }
}
