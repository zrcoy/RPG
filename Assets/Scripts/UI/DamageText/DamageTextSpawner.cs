using RPG.Stats;
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
        [SerializeField] DamageTextColors colorTable;



        public void Spawn(float damage, CharacterClass instigator)
        {
            DamageText instance = Instantiate<DamageText>(damageTextPrefab, transform);
            Text textComponent = instance.GetComponentInChildren<Text>();
            textComponent.text = String.Format("{0:0}", damage);
            
            //all types for heal
            if (instigator == CharacterClass.Healer)
            {
                SetTextColor(textComponent, colorTable.heal);
            }
            //all types for damage dealt
            else
            {
                if (instigator == CharacterClass.Player)
                {
                    SetTextColor(textComponent, colorTable.normalHitPlayerDeal);
                }
                else
                {
                    SetTextColor(textComponent, colorTable.damagePlayerReceive);
                }
            }
        }

        private void SetTextColor(Text textComponent, Color color)
        {
            textComponent.color = new Color(color.r, color.g, color.b);
        }

    }
}
