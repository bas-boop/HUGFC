using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    [RequireComponent(typeof(PlayerInput))]
    public sealed class InputParser : MonoBehaviour
    {
        private const string MOVE_ACTION = "Move";

        [SerializeField] private Movement movement;
        
        private PlayerInput _playerInput;
        private InputActionAsset _playerControlsActions;

        private void Awake()
        {
            _playerInput = GetComponent<PlayerInput>();
            _playerControlsActions = _playerInput.actions;
        }

        private void FixedUpdate()
        {
            Vector2 moveInput = _playerControlsActions[MOVE_ACTION].ReadValue<Vector2>();
            movement.Walk(moveInput);
        }
    }
}