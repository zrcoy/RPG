using GameDevTV.Utils;
using RPG.Control;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public class CameraFacing : MonoBehaviour
    {
        LazyValue<CameraController> followCam = null;
        private void Awake()
        {
            followCam = new LazyValue<CameraController>(GetInitializedCamera);

        }

        private CameraController GetInitializedCamera()
        {
            return FindObjectOfType<CameraController>();
        }

        private void Start()
        {
            followCam.ForceInit();
            followCam.value.AddObjectToFaceCamera(gameObject);
        }
    }
}


