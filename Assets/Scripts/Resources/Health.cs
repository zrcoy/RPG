using RPG.Core;
using RPG.Saving;
using RPG.Stats;
using System;
using UnityEngine;


namespace RPG.Resources
{
    public class Health : MonoBehaviour, ISaveable
    {
        [NonSerialized] float regenerationPercentage = 70;

        float healthPoints = -1f;
        bool isDead = false;

        void Start()
        {
            GetComponent<BaseStats>().onLevelUp += RegenerateHealth;
            //if hp <0, meaning that we haven't restore the state yet
            if (healthPoints < 0)
            {
                healthPoints = GetComponent<BaseStats>().GetStat(Stat.Health);
            }
        }



        public void TakeDamage(GameObject instigator, float damage)
        {
            print(gameObject.name + " took damage: " + (int)damage);
            healthPoints = Mathf.Max(healthPoints - damage, 0);
            //print(healthPoints);
            if (healthPoints == 0)
            {
                //prevent multiple times xp added
                if (!isDead)
                {
                    AwardExperience(instigator);
                }
                Die();
            }
        }

        private void AwardExperience(GameObject instigator)
        {
            Experience ex = instigator.GetComponent<Experience>();
            if (ex == null)
            {
                return;
            }
            ex.GainExperience(GetComponent<BaseStats>().GetStat(Stat.ExperienceReward));
        }

        public float GetHealthPoints()
        {
            return healthPoints;
        }

        public float GetMaxHealthPoints()
        {
            return GetComponent<BaseStats>().GetStat(Stat.Health);
        }

        public float GetPercentage()
        {
            return 100 * (healthPoints / GetComponent<BaseStats>().GetStat(Stat.Health));
        }

        private void Die()
        {
            if (isDead)
            {
                return;
            }
            GetComponent<Animator>().SetTrigger("die");
            isDead = true;
            GetComponent<ActionScheduler>().CancelCurrentAction();


        }

        public bool IsDead()
        {
            return isDead;
        }

        private void RegenerateHealth()
        {
            float regenerateHP = GetComponent<BaseStats>().GetStat(Stat.Health) * (regenerationPercentage / 100);
            healthPoints = Mathf.Max(regenerateHP, healthPoints);
        }

        public object CaptureState()
        {
            return healthPoints;
        }

        public void RestoreState(object state)
        {
            healthPoints = (float)state;

            if (healthPoints == 0)
            {
                Die();
            }
        }
    }
}

