using PuzzleGame.Quest;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PuzzleGame.Audio
{
    public enum AudioTypes
    {
        Error,
        Finish,
        PartlyFinished
    }

    [Serializable]
    public class QuestAudio
    {
        public QuestTypes questType;
        public List<AudioClip> audios;
    }

    [Serializable]
    public class DataEntity
    {
        public AudioTypes key;
        public List<QuestAudio> questAudio;
    }

    [CreateAssetMenu(fileName = "AudioData", menuName = "ScriptableObjects/SpawnAudioData", order = 1)]
    public class AudioData : ScriptableObject
    {
        public AudioClip ReallyYouWantToPut;
        public AudioClip YouCantDestroy;
        public AudioClip OneStepWarn;
        public AudioClip NeedPotion;
        public AudioClip WellWellWell;
        public AudioClip NeverLeave;
        public AudioClip PinkPonies;
        public AudioClip PrepareCauldron;

        public List<DataEntity> audioDataSpecial;
    }
}
