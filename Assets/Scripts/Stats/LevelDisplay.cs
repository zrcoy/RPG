using UnityEngine;
using UnityEngine.UI;

namespace RPG.Stats
{
    public class LevelDisplay : MonoBehaviour
    {
        GameObject player = null;

        private void Awake()
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
        private void Update()
        {
            GetComponent<Text>().text = string.Format("{0:0}", player.GetComponent<BaseStats>().GetLevel());
        }
    }
}

