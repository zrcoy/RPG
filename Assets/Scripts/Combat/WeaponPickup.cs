using RPG.Attributes;
using RPG.Control;
using RPG.Movement;
using System.Collections;
using UnityEngine;


namespace RPG.Combat
{
    public class WeaponPickup : MonoBehaviour, IRaycastable
    {
        [SerializeField] WeaponConfig weapon = null;
        [SerializeField] float healthToRestore = 0;
        [SerializeField] float respawnTime = 5;
        [SerializeField] float maxRangeGetPickup = 30f;


        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Player")
            {
                Pickup(other.gameObject);
            }
        }

        private void Pickup(GameObject obj)
        {
            if (healthToRestore > 0)
            {
                obj.GetComponent<Health>().Heal(healthToRestore);
            }
            else if (weapon != null)
            {
                obj.GetComponent<Fighter>().EquipWeapon(weapon);
            }
            StartCoroutine(HideForSeconds(respawnTime));
        }

        private IEnumerator HideForSeconds(float seconds)
        {
            ShowPickup(false);
            yield return new WaitForSeconds(seconds);
            ShowPickup(true);
        }

        private void ShowPickup(bool show)
        {
            GetComponent<CapsuleCollider>().enabled = show;
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(show);
            }
        }

        public bool HandleRaycast(PlayerController callingController)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if(!isWithinRange(callingController))
                {
                    return false;
                }
                Pickup(callingController.gameObject);
            }
            return true;
        }

        private bool isWithinRange(PlayerController callingController)
        {
            return Vector3.Distance(callingController.transform.position, transform.position) <= maxRangeGetPickup;
        }

        public CursorType GetCursorType()
        {
            return CursorType.WeaponPickup;
        }
    }

}
