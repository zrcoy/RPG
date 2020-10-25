using RPG.Combat;
using RPG.Resources;
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
            GetComponent<Text>().text = string.Format("{0:0}%", health.GetPercentage());
        }
        else
        {
            GetComponent<Text>().text = "N/A";
        }

    }
}
