using UnityEngine;

namespace Assets.Scripts.Camera
{
    public class CameraMover : MonoBehaviour
    {
        public Transform transformCircle, transformCamera;

        private void LateUpdate()
        {
            transformCamera.position = transformCircle.position + new Vector3(-1, 0, 0);
        }
    }
}
