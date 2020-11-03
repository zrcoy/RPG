using RPG.Combat;
using RPG.Attributes;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthDisplay : MonoBehaviour
{
    Fighter playerFighter;

    private void Awake()
    {
        playerFighter = GameObject.FindGameObjectWithTag("Player").GetComponent<Fighter>();
    }

    private void Update()
    {
        Health health = playerFighter.GetTarget();

        if (health != null)
        {
            GetComponent<Text>().text = string.Format("{0:0} / {1:0}", health.GetHealthPoints(), health.GetMaxHealthPoints());
        }
        else
        {
            GetComponent<Text>().text = "N/A";
        }

    }
}
