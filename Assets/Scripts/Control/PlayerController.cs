using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using RPG.Movement;
using RPG.Combat;

namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {
        Mover mover;
        // Start is called before the first frame update
        void Start()
        {
            mover = GetComponent<Mover>();
        }

        // Update is called once per frame
        void Update()
        {
            if (InteractWithCombat()) return;
            else if (InteractWithMovement()) return;
            print("nothing to do");
        }

        private bool InteractWithCombat()
        {
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
            foreach (RaycastHit hit in hits)
            {
                CombatTarget ct = hit.transform.GetComponent<CombatTarget>();
                if (!GetComponent<Fighter>().CanAttack(ct)) continue;

                if (Input.GetMouseButtonDown(0))
                {
                    GetComponent<Fighter>().Attack(ct);
                }
                return true;

            }
            // not any target to interactive
            return false;
        }

        private bool InteractWithMovement()
        {
            RaycastHit hit;
            bool isHit = Physics.Raycast(GetMouseRay(), out hit);
            if (isHit)
            {
                if (Input.GetMouseButton(0))
                {
                    mover.StartMoveAction(hit.point);
                }
                return true;
            }
            return false;
        }

        private static Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }

}