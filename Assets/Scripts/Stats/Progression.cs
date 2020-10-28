using System;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Stats
{
    [CreateAssetMenu(fileName = "Progression", menuName = "Stats/New Progression", order = 0)]
    public class Progression : ScriptableObject
    {
        [SerializeField] ProgressionCharacterClass[] characterClasses = null;

        Dictionary<CharacterClass, Dictionary<Stat, float[]>> lookupTable = null;

        public float GetStat(Stat stat, CharacterClass characterClass, int level)
        {
            BuildLookup();

            float[] levels = lookupTable[characterClass][stat];
            if (level > levels.Length)
            {
                return 0;
            }
            return levels[level - 1];
        }

        public int GetLevels(Stat stat, CharacterClass characterClass)
        {
            BuildLookup();
            float[] levels = lookupTable[characterClass][stat];
            return levels.Length;
        }

        private void BuildLookup()
        {
            if(lookupTable!=null)
            {
                return;
            }
            lookupTable = new Dictionary<CharacterClass, Dictionary<Stat, float[]>>();
            foreach(ProgressionCharacterClass pc in characterClasses)
            {
                Dictionary<Stat, float[]> statsLookupTable = new Dictionary<Stat, float[]>();
                foreach(ProgressionStat ps in pc.stats)
                {
                    statsLookupTable[ps.stat] = ps.levels;
                }
                lookupTable[pc.characterClass] = statsLookupTable;
            }
        }

        [System.Serializable]
        class ProgressionCharacterClass
        {
            public CharacterClass characterClass;
            //public float[] health;
            public ProgressionStat[] stats;
        }

        [System.Serializable]
        class ProgressionStat
        {
            public Stat stat;
            public float[] levels;

        }
    }
}