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
        public AudioClip UseThisPortal;
        public AudioClip GoodBoy;
        public AudioClip MagicCloset;
        public AudioClip PlantMonologue;
        public AudioClip BookIllustration;
        public AudioClip Bat;
        public AudioClip HaveFun;
        public AudioClip GetRidOfThis;
        public AudioClip UsePhysics;
        public AudioClip NotThisPlant;
        public AudioClip AdditionalFromTheBook;
        public AudioClip BurnHouse;
        public AudioClip HowCuteHeIs;
        public AudioClip IDidntExpect;
        public AudioClip LikeThis;
        public AudioClip OneCandle;
        public AudioClip StillDisgusting;
        public AudioClip DestoyingEnough;
        public AudioClip FindFlask;
        public AudioClip GetItSorted;
        public AudioClip GoToLab;
        public AudioClip IllAddItFor;
        public AudioClip NextStone;
        public AudioClip SoThisFlask;
        public AudioClip YesOfCourse;
        public AudioClip AlmoustImpossible;
        public AudioClip Clean;
        public AudioClip DifferentAngle;
        public AudioClip OSmellsDilicious;
        public AudioClip MyGrandFather;


        public List<DataEntity> audioDataSpecial;
    }
}
