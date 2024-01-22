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

        public void PutQuestItem(QuestItem item)
        {
            if(CheckItemType(item))
            {
                _usedItemsNumber++;

                item.UseItem();

                if(_usedItemsNumber == _questItems.Count)
                {
                    //To do quest success
                }
            }
            else
            {
                //Quest point error action
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
