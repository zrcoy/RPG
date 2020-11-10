using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbitor : MonoBehaviour
{
    [SerializeField] float speed = 350f;
    [SerializeField] Transform target = null;

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            transform.RotateAround(target.position, Vector3.up, speed * Time.deltaTime);
        }
    }
}
