using GameDevTV.Utils;
using RPG.Core;
using RPG.Saving;
using RPG.Stats;
using System;
using UnityEngine;
using UnityEngine.Events;

namespace RPG.Resources
{
    public class Health : MonoBehaviour, ISaveable
    {
        [SerializeField] float regenerationPercentage = 70;
        [SerializeField] TakeDamageEvent takeDamage;

        [System.Serializable]
        public class TakeDamageEvent : UnityEvent<float>
        {
        }

        [SerializeField] float hp = 0;//debug use
        LazyValue<float> healthPoints;
        bool isDead = false;

        //debug use
        private void Update()
        {
            hp = healthPoints.value;
        }


        private void Awake()
        {
            healthPoints = new LazyValue<float>(GetInitialHealth);
        }

        private float GetInitialHealth()
        {
            return GetComponent<BaseStats>().GetStat(Stat.Health);
        }

        void Start()
        {
            healthPoints.ForceInit();

        }

        private void OnEnable()
        {
            GetComponent<BaseStats>().onLevelUp += RegenerateHealth;
        }

        private void OnDisable()
        {
            GetComponent<BaseStats>().onLevelUp -= RegenerateHealth;
        }


        public void TakeDamage(GameObject instigator, float damage)
        {
            print(gameObject.name + " took damage: " + damage);
            healthPoints.value = Mathf.Max(healthPoints.value - damage, 0);

            //print(healthPoints);
            if (healthPoints.value == 0)
            {
                //prevent multiple times xp added
                if (!isDead)
                {
                    AwardExperience(instigator);
                }
                Die();
            }

            takeDamage.Invoke(damage);

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
            return healthPoints.value;
        }

        public float GetMaxHealthPoints()
        {
            return GetComponent<BaseStats>().GetStat(Stat.Health);
        }

        public float GetPercentage()
        {
            return 100 * (healthPoints.value / GetComponent<BaseStats>().GetStat(Stat.Health));
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
            healthPoints.value = Mathf.Max(regenerateHP, healthPoints.value);
        }

        public object CaptureState()
        {
            return healthPoints.value;
        }

        public void RestoreState(object state)
        {
            healthPoints.value = (float)state;

            if (healthPoints.value == 0)
            {
                Die();
            }
        }
    }
}

