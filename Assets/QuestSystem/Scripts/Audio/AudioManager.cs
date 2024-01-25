using PuzzleGame.Quest;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PuzzleGame.Audio
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance;
        public AudioData AudioData => _data;

        public AudioSource VOSource => _voSource;
        public AudioSource SFXSource => _sfxSource;

        [SerializeField]
        private AudioSource _voSource;
        [SerializeField]
        private AudioSource _sfxSource;
        [SerializeField]
        private AudioData _data;

        private void Awake()
        {
            DontDestroyOnLoad(this);

            if (Instance != null && Instance != this)
            {
                Destroy(this);
            }
            else
            {
                Instance = this;
            }
        }

        public void PlaySFX(AudioClip clip)
        {
            _sfxSource.time = 0f;
            _sfxSource.clip = clip;
            _sfxSource.Play();
        }

        public void StopSFX() 
        {
            _sfxSource.Stop();
        }

        public void PlayClip(AudioClip clip)
        {
            _voSource.clip = clip;
            _voSource.time = 0f;
            _voSource.Play();
        }

        public void PlayPartlyFinished(QuestTypes quest)
        {
            DataEntity entity = _data.audioDataSpecial.Find(x => x.key == AudioTypes.PartlyFinished);

            PlayFirst(entity, quest);
        }

        public void PlayFinishQuestAudio(QuestTypes quest)
        {
            DataEntity entity = _data.audioDataSpecial.Find(x => x.key == AudioTypes.Finish);

            PlayFirst(entity, quest);
        }

        public void PlayRandomErrorAudio(QuestTypes quest)
        {
            DataEntity entity = _data.audioDataSpecial.Find(x => x.key == AudioTypes.Error);

            if (entity != null)
            {
                QuestAudio questAudio = entity.questAudio.Find(x => x.questType == quest);

                if (questAudio != null)
                {
                    int index = Random.Range(0, questAudio.audios.Count);

                    AudioClip clip = questAudio.audios[index];

                    PlayClip(clip);
                }
            }
        }

        public void StopVO()
        {
            _voSource.Stop();
        }

        private void PlayFirst(DataEntity entity, QuestTypes quest)
        {
            if (entity != null)
            {
                QuestAudio questAudio = entity.questAudio.Find(x => x.questType == quest);

                if (questAudio != null)
                {
                    PlayClip(questAudio.audios[0]);
                }
            }
        }
    }
}

