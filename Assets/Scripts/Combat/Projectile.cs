using RPG.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] float speed = 2;
        [SerializeField] bool isHoming = false;

        const float TimeMaxExisting = 2f;

        Health target = null;
        float damage = 0;
        float timerForExist = 0;

        private void Start()
        {
            transform.LookAt(GetAimLocation());
        }

        void Update()
        {
            if (target == null)
            {
                return;
            }
            if (isHoming && !target.IsDead())
            {
                transform.LookAt(GetAimLocation());
            }
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
            timerForExist += Time.deltaTime;
            if (timerForExist >= TimeMaxExisting)
            {
                Destroy(gameObject);
            }
        }

        public void SetTarget(Health target, float damage)
        {
            this.target = target;
            this.damage = damage;
        }

        private Vector3 GetAimLocation()
        {
            CapsuleCollider targetCapsule = target.GetComponent<CapsuleCollider>();
            if (targetCapsule == null)
            {
                return target.transform.position;
            }
            return target.transform.position + Vector3.up * targetCapsule.height * 0.5f;
        }


        void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<Health>() != target)
            {
                return;
            }
            if (other.GetComponent<Health>().IsDead())
            {
                return;
            }
            target.TakeDamage(damage);
            Destroy(gameObject);
        }
    }

}
