using Cinemachine;
using RPG.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace RPG.Control
{
    public class CameraController : MonoBehaviour
    {
        [Range(0, 1)] [SerializeField] float rotationSpeed = 0.7f;
        [Range(0, 300)] [SerializeField] float rotationClamp = 200f;
        [Range(2, 4)] [SerializeField] float cameraDistanceMin = 2.5f;
        [Range(8, 12)] [SerializeField] float cameraDistanceMax = 10f;
        [SerializeField] List<GameObject> objsToFaceCamera = null;


        float mouseClickPosX;


        void Update()
        {
            if (Input.GetMouseButtonDown(1))
            {
                mouseClickPosX = Input.mousePosition.x;
            }

            if (Input.GetMouseButton(1))
            {
                float yRotation = Mathf.Clamp(Input.mousePosition.x - mouseClickPosX, -rotationClamp, rotationClamp) * rotationSpeed * Time.deltaTime;
                transform.Rotate(0, yRotation, 0, Space.World);
            }

            if (Input.GetAxis("Mouse ScrollWheel") > 0f || Input.GetAxis("Mouse ScrollWheel") < 0f)
            {
                GetTransposer().m_CameraDistance -= Input.GetAxis("Mouse ScrollWheel") * 20;
                GetTransposer().m_CameraDistance = Mathf.Clamp(GetTransposer().m_CameraDistance, cameraDistanceMin, cameraDistanceMax);
            }

        }

        void LateUpdate()
        {
            foreach (var obj in objsToFaceCamera)
            {
                if (obj != null)
                {
                    obj.transform.up = transform.up;
                    obj.transform.forward = transform.forward;
                }

            }
        }


        private CinemachineFramingTransposer GetTransposer()
        {
            return GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineFramingTransposer>();
        }


        public void AddObjectToFaceCamera(GameObject cameraFacingObj)
        {
            objsToFaceCamera.Add(cameraFacingObj);
        }
    }

}
