using System;
using UnityEngine;

namespace Utils.CameraUtils
{
    public class CameraAspectRatio : MonoBehaviour
    {
        private UnityEngine.Camera _cam;
        [SerializeField] private Vector2 targetResolution; //the resolution the game is designed for

        void Awake(){

            _cam = GetComponent<Camera>();

            float targetAspect = targetResolution.x / targetResolution.y;
            float currentAspect = Screen.width / (float)Screen.height;
            var startSize = _cam.orthographicSize;
            
            if (Math.Abs(currentAspect - targetAspect) > 0.01f){
                _cam.orthographicSize /= currentAspect / targetAspect;
            }
        }
    }
}