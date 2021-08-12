using UnityEngine;

namespace Misc
{
    public class LookAtCamera : MonoBehaviour
    {
        private Camera mainCamera;

        private void Awake()
        {
            mainCamera = Camera.main;
        }

        private void Update()
        {
            if (transform.rotation != mainCamera.transform.rotation)
                transform.rotation = mainCamera.transform.rotation;
        }
    }
}