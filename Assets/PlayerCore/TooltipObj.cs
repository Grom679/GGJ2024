using System;
using System.Collections;
using System.Collections.Generic;
using PuzzleGame.Quest;
using TMPro;
using UnityEngine;

public class TooltipObj : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    
    public void SetText(string tooltipText)
    {
        _text.text = tooltipText;
    }
    
    private void Update()
    {
        transform.LookAt(Scenario.Instance.Player.transform);
    }
}
