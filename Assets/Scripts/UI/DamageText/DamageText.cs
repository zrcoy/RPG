using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RPG.UI.DamageText
{
    
    public class DamageText : MonoBehaviour
    {
        [SerializeField] float speed = 1f;
        
        
        Vector3 velocity;

        void Awake()
        {
            velocity = Camera.main.transform.up + new Vector3(Random.Range(-3f, 3f), Random.Range(0f, 1f), Random.Range(-3f, 3f));
        }

        private void Update()
        {
            transform.position += velocity * speed * Time.deltaTime;
        }




    }
}


