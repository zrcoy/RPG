using RPG.Core;
using RPG.Saving;
using RPG.Stats;
using UnityEngine;


namespace RPG.Resources
{
    public class Health : MonoBehaviour, ISaveable
    {
        [SerializeField] float healthPoints = 100f;

        bool isDead = false;


        void Start()
        {
            healthPoints = GetComponent<BaseStats>().GetHealth();
        }


        public void TakeDamage(float damage)
        {
            healthPoints = Mathf.Max(healthPoints - damage, 0);
            print(healthPoints);
            if (healthPoints == 0)
            {
                Die();
            }
        }

        public float GetPercentage()
        {
            return 100*(healthPoints / GetComponent<BaseStats>().GetHealth());
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

        public object CaptureState()
        {
            return healthPoints;
        }

        public void RestoreState(object state)
        {
            healthPoints = (float)state;

            if(healthPoints==0)
            {
                Die();
            }
        }
    }
}

