using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.UI.DamageText
{
    public class DamageTextSpawner : MonoBehaviour
    {
        [SerializeField] DamageText damageTextPrefab = null;

        public void Spawn(float damage)
        {
            DamageText instance = Instantiate<DamageText>(damageTextPrefab, transform);

            instance.GetComponentInChildren<Text>().text = String.Format("{0:0}", damage); 
        }
    }
}
