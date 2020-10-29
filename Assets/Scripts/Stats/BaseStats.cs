using System;
using UnityEngine;


namespace RPG.Stats
{
    public class BaseStats : MonoBehaviour
    {
        [Range(1, 99)]
        [SerializeField] int startingLevel = 1;
        [SerializeField] CharacterClass characterClass;
        [SerializeField] Progression progression = null;
        [SerializeField] GameObject levelUpParticleEffect = null;
        [SerializeField] bool shouldUseModifiers = false;

        int currentLevel = 0;
        public event Action onLevelUp;

        private void Start()
        {
            currentLevel = CalculateLevel();
            Experience experience = GetComponent<Experience>();
            if (experience != null)
            {
                experience.onExperienceGained += UpdateLevel;
            }
        }

        public float GetStat(Stat stat)
        {
            return (GetBaseStat(stat) + GetAdditiveModifier(stat)) * (GetPercentageModifier(stat)/100f + 1);
        }



        private float GetBaseStat(Stat stat)
        {
            return progression.GetStat(stat, characterClass, GetLevel());
        }

        private float GetAdditiveModifier(Stat stat)
        {
            if(!shouldUseModifiers)
            {
                return 0;
            }
            float total = 0;
            foreach(IModifierProvider provider in GetComponents<IModifierProvider>())
            {
                foreach(float modifier in provider.GetAdditiveModifier(stat))
                {
                    total += modifier;
                }
            }
            return total;
        }

        private float GetPercentageModifier(Stat stat)
        {
            if (!shouldUseModifiers)
            {
                return 0;
            }
            float total = 0;
            foreach (IModifierProvider provider in GetComponents<IModifierProvider>())
            {
                foreach (float modifier in provider.GetPercentageModifier(stat))
                {
                    total += modifier;
                }
            }
            return total;
        }

        private void UpdateLevel()
        {
            int newLevel = CalculateLevel();
            if (newLevel > currentLevel)
            {
                currentLevel = newLevel;
                LevelUpEffect();
                onLevelUp();
            }
        }

        private void LevelUpEffect()
        {
            Instantiate(levelUpParticleEffect, transform);
        }

        public int GetLevel()
        {
            if(currentLevel<1)
            {
                currentLevel = CalculateLevel();
            }
            return currentLevel;
        }

        private int CalculateLevel()
        {
            Experience exComp = GetComponent<Experience>();
            if (exComp == null)
            {
                return startingLevel;
            }
            float currentXP = exComp.GetCurrentExperiencePoints();
            int totalLevels = progression.GetLevels(Stat.ExperienceToLevelUp, characterClass);
            for (int currentLevel = 1; currentLevel <= totalLevels; currentLevel++)
            {
                float xpToLevelUp = progression.GetStat(Stat.ExperienceToLevelUp, characterClass, currentLevel);
                if (currentXP < xpToLevelUp)
                {
                    return currentLevel;
                }

            }
            //max level 
            return totalLevels + 1;
        }
    }

}
