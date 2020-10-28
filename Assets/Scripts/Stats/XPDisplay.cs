using UnityEngine;
using UnityEngine.UI;

namespace RPG.Stats
{
    public class XPDisplay : MonoBehaviour
    {
        Experience xp;

        private void Awake()
        {
            xp = GameObject.FindGameObjectWithTag("Player").GetComponent<Experience>();
        }
        private void Update()
        {
            GetComponent<Text>().text = string.Format("{0:0}", xp.GetCurrentExperiencePoints());
        }
    }
}

