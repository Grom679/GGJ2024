using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PuzzleGame.Core
{
    public class Boot : MonoBehaviour
    {
        [SerializeField]
        private int _sceneIndex;

        [SerializeField]
        private float _timeToBoot;

        private void Start()
        {
            StartCoroutine(LoadScene());
        }

        private IEnumerator LoadScene()
        {
            yield return new WaitForSeconds(_timeToBoot);

            SceneManager.LoadSceneAsync(_sceneIndex);
        }
    }
}

