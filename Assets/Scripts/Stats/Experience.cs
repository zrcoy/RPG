﻿using RPG.Saving;
using System;
using UnityEngine;

namespace RPG.Stats
{
    public class Experience : MonoBehaviour, ISaveable
    {
        [SerializeField] float experiencePoints = 0;

        //public delegate void ExperienceGainedDelegate();
        //public event ExperienceGainedDelegate onExperienceGained;
        public event Action onExperienceGained;

        public void GainExperience(float xp)
        {
            experiencePoints += xp;
            onExperienceGained();
        }

        public object CaptureState()
        {
            return experiencePoints;
        }

        public void RestoreState(object state)
        {
            experiencePoints = (float)state;
        }

        public float GetCurrentExperiencePoints()
        {
            return experiencePoints;
        }
    }
}
