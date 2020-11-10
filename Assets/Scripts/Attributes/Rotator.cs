using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField] float rotateSpeed = 1.5f;
    [SerializeField] Vector3 axisRotateAround;
    [SerializeField] Space space; 


    void Update()
    {
        if (axisRotateAround != Vector3.zero)
        {
            transform.Rotate(axisRotateAround.x * (rotateSpeed + Time.deltaTime),
                             axisRotateAround.y * (rotateSpeed + Time.deltaTime),
                             axisRotateAround.z * (rotateSpeed + Time.deltaTime),
                             space);
        }

    }
}
