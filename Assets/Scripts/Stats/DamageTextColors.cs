using RPG.Stats;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "DamageTextColors", menuName = "Stats/New Damage Text Color Table", order = 1)]
public class DamageTextColors : ScriptableObject
{
    [SerializeField] public Color normalHitPlayerDeal;
    [SerializeField] public Color criticalHitPlayerDeal;
    [SerializeField] public Color heal;
    [SerializeField] public Color damagePlayerReceive;
    
}
