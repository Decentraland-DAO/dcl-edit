﻿using Assets.Scripts.EditorState;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Visuals
{

    public class LineRotatorX : MonoBehaviour
    {
        // Dependencies
        private CameraState cameraState;

        private Quaternion ZeroRotation = Quaternion.Euler(0, 0, 0);
        private Quaternion NinetyRotation = Quaternion.Euler(0, 0, 180);
        private Quaternion OneEightyRotation = Quaternion.Euler(180, 0, 180);
        private Quaternion TwoSeventyRotation = Quaternion.Euler(90, 0, 180);

        [Inject]
        public void Construct(CameraState cameraState)
        {
            this.cameraState = cameraState;
        }

        private void Update()
        {
            var relativeCamPos = transform.parent.InverseTransformPoint(cameraState.Position);

            if (relativeCamPos.x > 0 && relativeCamPos.y > 0 && relativeCamPos.z > 0)
            {
                transform.rotation = ZeroRotation;
            }
            else if (relativeCamPos.x < 0 && relativeCamPos.y > 0 && relativeCamPos.z > 0)
            {
                transform.rotation = ZeroRotation;
            }
            else if (relativeCamPos.x > 0 && relativeCamPos.y < 0 && relativeCamPos.z > 0)
            {
                transform.rotation = NinetyRotation;
            }
            else if (relativeCamPos.x < 0 && relativeCamPos.y < 0 && relativeCamPos.z > 0)
            {
                transform.rotation = NinetyRotation;
            }
            else if (relativeCamPos.x < 0 && relativeCamPos.y > 0 && relativeCamPos.z < 0)
            {
                transform.rotation = OneEightyRotation;
            }
            else if (relativeCamPos.x > 0 && relativeCamPos.y > 0 && relativeCamPos.z < 0)
            {
                transform.rotation = OneEightyRotation;
            }
            else if (relativeCamPos.x > 0 && relativeCamPos.y < 0 && relativeCamPos.z < 0)
            {
                transform.rotation = TwoSeventyRotation;
            }
            else if (relativeCamPos.x < 0 && relativeCamPos.y < 0 && relativeCamPos.z < 0)
            {
                transform.rotation = TwoSeventyRotation;
            }
        }
    }
}
