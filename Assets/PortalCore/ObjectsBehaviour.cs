using PuzzleGame.Quest;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PuzzleGame.Core
{
    [Serializable]
    public class Room
    {
        public PortalEnum room;
        public List<Item> items;
    }

    public class ObjectsBehaviour : MonoBehaviour
    {
        [SerializeField]
        private PortalManager _portalsManager;
        [SerializeField]
        private Player _player;
        [SerializeField]
        private List<Room> _rooms;

        private PortalEnum _currentRoom;

        private void Start()
        {
            _currentRoom = PortalEnum.Floor;
            InitAll();
        }

        private void OnEnable()
        {
            _portalsManager.OnChangeRoom += ChangeRoom;
            GlobalEvents.Instance.OnResetItem += ResetItem;
        }

        private void OnDisable()
        {
            _portalsManager.OnChangeRoom -= ChangeRoom;
           GlobalEvents.Instance.OnResetItem -= ResetItem;
        }

        private void ChangeRoom(PortalEnum room)
        {
            Room currentRoom = _rooms.Find(x => x.room == _currentRoom);

            foreach (Item item in currentRoom.items)
            {
                item.MakeUngrabble();
            }

            Room nextRoom = _rooms.Find(x => x.room == room);

            if (room != PortalEnum.Fireplace)
            {
                foreach (Item item in nextRoom.items)
                {
                    item.MakeGrabble();
                }
            }
            else
            {
                Debug.LogError("test pictures");
            }
           

            SwapPhysics(currentRoom, nextRoom);

            _currentRoom = room;
        }

        private void ResetItem(QuestItem quest)
        {
            Item item = quest.GetComponent<Item>();

            if (item != null) 
            {
                if(_currentRoom != quest.BelongsTo)
                {
                    item.Rigidbody.isKinematic = true;
                    item.Rigidbody.useGravity = false;
                }
            }
        }

        private void InitAll()
        {
            foreach (var room in _rooms) 
            {
                foreach(var item in room.items)
                {
                    if(room.room == PortalEnum.Floor)
                    {
                        item.Rigidbody.isKinematic = true;
                        item.Rigidbody.useGravity = true;
                    }
                    else
                    {
                        item.Rigidbody.isKinematic = true;
                        item.Rigidbody.useGravity = false;
                    }
                }
            }
        }

        private void SwapPhysics(Room currentRoom, Room nextRoom)
        {
            Debug.Log("Swap " + currentRoom.room + " " + nextRoom.room);

            foreach(Item item in currentRoom.items)
            {
                item.Rigidbody.isKinematic = true;
                item.Rigidbody.useGravity = false;
            }

            foreach (Item item in nextRoom.items)
            {
                item.Rigidbody.isKinematic = true;
                item.Rigidbody.useGravity = true;
            }
        }
    }
}
