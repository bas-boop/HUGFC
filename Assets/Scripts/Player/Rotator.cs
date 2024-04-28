using UnityEngine;

namespace Player
{
    public sealed class Rotator : MonoBehaviour
    {
        [SerializeField] private float yawSensitivity = 1f;

        private void Start() => Cursor.lockState = CursorLockMode.Locked;
        
        public void Rotate(Vector2 input)
        {
            float yaw = input.x * yawSensitivity;
            transform.Rotate(Vector3.up * yaw);
        }
    }
}