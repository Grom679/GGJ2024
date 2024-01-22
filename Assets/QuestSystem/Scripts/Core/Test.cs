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

    void Start()
    {
        ChainManager.Instance.RegisterNewChain();

        ChainManager.Instance.WaitUntil(3f);
        ChainManager.Instance.WaitUntil(3f);
        ChainManager.Instance.WaitUntil(3f);
        ChainManager.Instance.WaitUntil(3f);

        ChainManager.Instance.FinishActions();

        GlobalEvents.Instance.OnChainFinished += ChainFinished;
    }

    private void ChainFinished()
    {
        Scenario.Instance.PlayFirstQuest();

        Scenario.Instance.CurrentQuest.ActivateQuestItem(_first);
        Scenario.Instance.CurrentQuest.QuestPoint.PutQuestItem(_first);
        Scenario.Instance.CurrentQuest.ActivateQuestItem(_second);
    }
}
