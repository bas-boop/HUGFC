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
        private const string CAMERA_ACTION = "Camera";

        [SerializeField] private Movement movement;
        [SerializeField] private Jump jump;
        [SerializeField] private Rotator rotator;
        
        private PlayerInput _playerInput;
        private InputActionAsset _playerControlsActions;
        private bool _isWalking;

        private void Awake()
        {
            _playerInput = GetComponent<PlayerInput>();
            _playerControlsActions = _playerInput.actions;
        }

        private void FixedUpdate()
        {
            Vector2 moveInput = _playerControlsActions[MOVE_ACTION].ReadValue<Vector2>();
            float runInput = _playerControlsActions[RUN_ACTION].ReadValue<float>();

            if (moveInput == Vector2.zero)
            {
                if (_isWalking)
                {
                    movement.SetIdle();
                    _isWalking = false;
                }
            }
            else
            {
                movement.Walk(moveInput, runInput);
                _isWalking = true;
            }

            Vector2 cameraInput = _playerControlsActions[CAMERA_ACTION].ReadValue<Vector2>();
            rotator.Rotate(cameraInput);
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