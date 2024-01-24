using PuzzleGame.Audio;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PuzzleGame.Core
{
    public class ChainManager : MonoBehaviour
    {
        public static ChainManager Instance { get; private set; }

        private Coroutine _workRoutine;
        private Queue<IEnumerator> _actions = new Queue<IEnumerator>();

        private bool _finished;

        private void Awake()
        {
            DontDestroyOnLoad(this);
            Instance = this;
        }

        public void RegisterNewChain()
        {
            if(_workRoutine == null)
            {
                _finished = false;
                _workRoutine = StartCoroutine(StartWorkingProcess());

                GlobalEvents.Instance.OnChainStarted?.Invoke();
            }
            else
            {
                Debug.LogWarning("Chain already run");
            }
        }

        public void FinishActions()
        {
            _finished = true;
        }

        public void WaitUntil(float time)
        {
            _actions.Enqueue(PlayWaitChain(time));
        }

        public void PlayAudio(AudioClip clip, Action action)
        {
            _actions.Enqueue(PlayAudioChain(clip));
        }

        public void PlayAudio(AudioClip clip)
        {
            _actions.Enqueue(PlayAudioChain(clip));
        }

        private void KillChain()
        {
            _workRoutine = null;

            _actions.Clear();
        }

        private IEnumerator StartWorkingProcess()
        {
            while (true)
            {
                if(_finished && _actions.Count == 0)
                {
                    KillChain();

                    Debug.Log("Finishing chain");

                    GlobalEvents.Instance.OnChainFinished?.Invoke();

                    //Call event finished actions

                    yield break;
                }

                if(_actions.Count > 0)
                {
                    IEnumerator action = _actions.Dequeue();

                    yield return action;
                }

                yield return null;
            }
        }

        private IEnumerator PlayAudioChain(AudioClip clip)
        {
            AudioManager.Instance.PlayClip(clip);

            yield return new WaitForSeconds(clip.length);

            Debug.Log("Finished One");
        }

        private IEnumerator PlayAudioChain(AudioClip clip, Action action)
        {
            AudioManager.Instance.PlayClip(clip);

            yield return new WaitForSeconds(clip.length);

            action?.Invoke();
            Debug.Log("Finished One");
        }

        private IEnumerator PlayWaitChain(float time)
        {
            yield return new WaitForSeconds(time);

            Debug.Log("Finished One");
        }
    }
}
