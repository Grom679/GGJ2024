using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayScreen : MonoBehaviour
{
    [SerializeField]
    private Button _playButton;
    [SerializeField] 
    private Button _suiсideButton;

    private void Start()
    {
        _playButton.onClick.AddListener(StartGame);
        _suiсideButton.onClick.AddListener(Suiсide);
    }

    private void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    private void Suiсide()
    {
        Application.Quit();
    }
}
