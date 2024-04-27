using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    [RequireComponent(typeof(PlayerInput))]
    public sealed class InputParser : MonoBehaviour
    {
        private const string MOVE_ACTION = "Move";
        private const string RUN_ACTION = "Run";
        private const string JUMP_ACTION = "Jump";

        [SerializeField] private Movement movement;
        [SerializeField] private Jump jump;
        
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
            float runInput = _playerControlsActions[RUN_ACTION].ReadValue<float>();
            
            if (moveInput != Vector2.zero)
                movement.Walk(moveInput, runInput);
            else
                movement.SetIdle();
        }

        private void OnEnable() => AddListeners();

        private void OnDisable() => RemoveListeners();

        private void AddListeners()
        {
            _playerControlsActions[JUMP_ACTION].performed += Jump;
        }

        private void RemoveListeners()
        {
            _playerControlsActions[JUMP_ACTION].performed -= Jump;
        }
        
        private void Jump(InputAction.CallbackContext context) => jump.ActivateJump();
    }
}