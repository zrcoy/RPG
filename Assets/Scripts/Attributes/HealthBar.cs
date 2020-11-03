using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Attributes
{
    public class HealthBar : MonoBehaviour
    {
        Health health = null;
        Canvas canvas = null;

        private void Start()
        {
            health = GetComponentInParent<Health>();
            canvas = GetComponentInParent<Canvas>();
        }
        private void Update()
        {
            if (health != null)
            {
                if (Mathf.Approximately(health.GetFraction(), 0) || Mathf.Approximately(health.GetFraction(),1))
                {
                    canvas.enabled = false;
                    return;
                }
                canvas.enabled = true;
                GetComponent<RectTransform>().localScale = new Vector3(health.GetFraction(), 1, 1);

            }
        }
    }

}
