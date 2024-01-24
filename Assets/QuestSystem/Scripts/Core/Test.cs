using PuzzleGame.Core;
using PuzzleGame.Quest;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public QuestItem _first;

    public QuestItem _second;

    public QuestItem _third;

    public bool putFirst;

    public bool activateSecond;

    public bool putSecond;

    public bool activateThird;

    public bool putThird;

    public bool teleportToBooks;

    public bool teleportback;

    public BookQuest bookQuest;

    void Start()
    {
        //GlobalEvents.Instance.OnChainFinished += ChainFinished;

        //Scenario.Instance.PlayFirstQuest();
    }

    private void Update()
    {
        if(putFirst)
        {
            putFirst = false;

            Scenario.Instance.CurrentQuest.QuestPoint.PutQuestItem(_first);
        }

        if(activateSecond)
        {
            activateSecond = false;

            Scenario.Instance.CurrentQuest.ActivateQuestItem(_second);
        }

        if (putSecond)
        {
            putSecond = false;

            Scenario.Instance.CurrentQuest.QuestPoint.PutQuestItem(_second);
        }

        if(teleportToBooks)
        {
            teleportToBooks = false;

            bookQuest.EnterBookWall();
        }

        if(teleportback)
        {
            teleportback = false;

            bookQuest.ExitBookWall();
        }

        if (putThird)
        {
            putThird = false;

            Scenario.Instance.CurrentQuest.QuestPoint.PutQuestItem(_third);
        }

        if (activateThird)
        {
            activateThird = false;

            Scenario.Instance.CurrentQuest.ActivateQuestItem(_third);
        }
    }

    private void ChainFinished()
    {
        Scenario.Instance.CurrentQuest.ActivateQuestItem(_first);
    }
}
