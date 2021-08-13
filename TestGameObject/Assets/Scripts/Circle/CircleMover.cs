using UnityEngine;

namespace Assets.Scripts.Circle
{
    public class CircleMover : MonoBehaviour
    {
        [Header("Характеристики шарика")]
        public float speedRun;
        public float speedRotateNormal;
        public float jumpForse;

        private float speedRotate = 0F;
        private Rigidbody rigidbodyCircle;
        private Ray ray;
        private RaycastHit raycastHit = new RaycastHit();

        private void Start() => rigidbodyCircle = GetComponent<Rigidbody>();

        private void Update()
        {
            ray = new Ray(gameObject.transform.position, -Vector3.up);
            rigidbodyCircle.AddForce(Vector3.right * speedRun * UnityEngine.Time.deltaTime * 0.5F);

#if !UNITY_ANDROID && UNITY_EDITOR
            if (Input.GetKeyDown(KeyCode.Space))
            {
                JumpCircle();
            }

            Physics.Raycast(ray, out raycastHit);
            if (raycastHit.distance < 0.8F)
            {
                float horizontalMove = Input.GetAxis("Horizontal");
                rigidbodyCircle.AddForce(Vector3.forward * speedRotateNormal * -horizontalMove);
            }
#endif
        }

        private void JumpCircle()
        {
            Physics.Raycast(ray, out raycastHit);
            if (raycastHit.distance < 0.5F && raycastHit.distance > 0.2F)
            {
                rigidbodyCircle.AddForce(Vector3.up * jumpForse);
            }
        }

        public void OnButtonRight()
        {
            Physics.Raycast(ray, out raycastHit);
            if (!(raycastHit.distance < 0.8F)) return;
            speedRotate = -speedRotateNormal;
            rigidbodyCircle.AddForce(Vector3.forward * speedRotate);
        }

        public void OnButtonLeft()
        {
            Physics.Raycast(ray, out raycastHit);
            if (!(raycastHit.distance < 0.8F)) return;
            speedRotate = speedRotateNormal;
            rigidbodyCircle.AddForce(Vector3.forward * speedRotate);
        }

        public void OnButtonNotDown() => speedRotate = 0F;

        public void OnButtonUp() => JumpCircle();
    }
}
