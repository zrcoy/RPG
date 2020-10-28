using UnityEngine;
using RPG.Movement;
using RPG.Core;
using RPG.Saving;
using RPG.Resources;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction, ISaveable
    {
        [SerializeField] float timeBetweenAttacks = 1f;
        [SerializeField] Transform rightHandTransform = null;
        [SerializeField] Transform leftHandTransform = null;
        [SerializeField] Weapon defaultWeapon = null;


        Health target;
        Mover mover;
        float timeSinceLastAtttack = Mathf.Infinity;
        Weapon currentWeapon = null;

        private void Start()
        {
            if (currentWeapon == null)
            {
                EquipWeapon(defaultWeapon);
            }
            mover = GetComponent<Mover>();
        }


        private void Update()
        {
            timeSinceLastAtttack += Time.deltaTime;
            if (target == null) return;
            if (target.IsDead()) return;
            if (!GetIsInRange())
            {
                mover.MoveTo(target.transform.position, 0.8f);
            }
            else
            {
                mover.Cancel();
                AttackBehaviour();
            }
        }

        public void EquipWeapon(Weapon weapon)
        {
            currentWeapon = weapon;
            Animator animator = GetComponent<Animator>();
            currentWeapon.Spawn(rightHandTransform, leftHandTransform, animator);
        }

        public Health GetTarget()
        {
            return target;
        }

        public void AttackBehaviour()
        {
            transform.LookAt(target.transform);
            if (timeSinceLastAtttack >= timeBetweenAttacks)
            {
                // this will trigger the Hit() event
                TriggerAttack();
                timeSinceLastAtttack = 0;
            }
        }

        private void TriggerAttack()
        {
            GetComponent<Animator>().ResetTrigger("stopAttack");
            GetComponent<Animator>().SetTrigger("attack");
        }



        // Animation Event
        void Hit()
        {
            if (target == null)
            {
                return;
            }
            if (currentWeapon.HasProjectile())
            {
                currentWeapon.LaunchProjectile(rightHandTransform, leftHandTransform, target,gameObject);
            }
            else
            {
                target.TakeDamage(gameObject, currentWeapon.GetWeaponDamage());
            }
        }

        // Animation Event
        void Shoot()
        {
            Hit();
        }

        public bool GetIsInRange()
        {
            return Vector3.Distance(transform.position, target.transform.position) < currentWeapon.GetWeaponRange();
        }



        public void Cancel()
        {
            TriggerStopAttack();
            target = null;
            GetComponent<Mover>().Cancel();
        }

        private void TriggerStopAttack()
        {
            GetComponent<Animator>().ResetTrigger("attack");
            GetComponent<Animator>().SetTrigger("stopAttack");
        }

        public bool CanAttack(GameObject combatTarget)
        {
            if (combatTarget == null)
            {
                return false;
            }
            Health tar = combatTarget.GetComponent<Health>();
            return (tar != null && !tar.IsDead());
        }

        public void Attack(GameObject combatTarget)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            target = combatTarget.GetComponent<Health>();
        }

        public object CaptureState()
        {
            return currentWeapon.name;
        }

        public void RestoreState(object state)
        {
            string weaponName = (string)state;
            Weapon weapon = UnityEngine.Resources.Load<Weapon>(weaponName);
            EquipWeapon(weapon);
        }
    }
}

