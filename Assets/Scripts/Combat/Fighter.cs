using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;
using RPG.Core;
using System;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
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
            mover = GetComponent<Mover>();
            EquipWeapon(defaultWeapon);
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
                currentWeapon.LaunchProjectile(rightHandTransform, leftHandTransform, target);
            }
            else
            {

                target.TakeDamage(currentWeapon.GetWeaponDamage());
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
    }
}

