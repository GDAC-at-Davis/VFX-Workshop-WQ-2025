using UnityEngine;
using UnityEngine.Events;

namespace Gdac3PWorkshopPack.Protag.Scripts
{
    /// <summary>
    ///     3P controller
    /// </summary>
    public class PlayerController : MonoBehaviour
    {
        [Header("Dependencies")]

        [SerializeField]
        private CharacterController _charController;

        [SerializeField]
        private Transform _meshBody;

        [SerializeField]
        private MovementStats _moveStats;

        [SerializeField]
        private Animator _anim;

        [Header("Events")]

        public UnityEvent OnStartRunning;

        public UnityEvent OnStopRunning;
        public UnityEvent OnJump;

        private readonly int _animState = Animator.StringToHash("State");

        private Vector2 _currentHorizontalVelocity;
        private float _yVelocity;
        private bool _wasGrounded;

        private void Update()
        {
            float xInput = Input.GetAxisRaw("Horizontal");
            float zInput = Input.GetAxisRaw("Vertical");
            var horizontalInput = new Vector2(xInput, zInput);

            bool jumpPressed = Input.GetKeyDown(KeyCode.Space);

            HandleHorizontalMovement(horizontalInput);

            HandleVerticalMovement(jumpPressed);

            HandleAnimation(horizontalInput);
        }

        private void HandleHorizontalMovement(Vector2 input)
        {
            var moveDirection = new Vector3(input.x, 0, input.y);

            // Rotate moveDirection based on camera (so axis are relative to camera)
            moveDirection = Camera.main.transform.TransformDirection(moveDirection);

            // project onto the xz plane, in case camera is tilted
            moveDirection.y = 0;
            moveDirection.Normalize();

            // Accelerate towards target velocity
            Vector2 targetHorizontalVelocity = new Vector2(moveDirection.x, moveDirection.z) * _moveStats.MoveSpeed;
            Vector2 newHorizontalVelocity;
            if (moveDirection.magnitude > 0)
            {
                newHorizontalVelocity = Vector2.MoveTowards(_currentHorizontalVelocity, targetHorizontalVelocity,
                    _moveStats.MoveAccel * Time.deltaTime);
            }
            else
            {
                newHorizontalVelocity = Vector2.MoveTowards(_currentHorizontalVelocity, Vector2.zero,
                    _moveStats.MoveFriction * Time.deltaTime);
            }

            _wasGrounded = _charController.isGrounded;

            // Move!
            Vector3 moveStep = new Vector3(newHorizontalVelocity.x, _yVelocity, newHorizontalVelocity.y) *
                               Time.deltaTime;
            _charController.Move(moveStep);

            // Character controller's internal velocity is recalculated after Move(), so save it after
            Vector3 velocity = _charController.velocity;
            _currentHorizontalVelocity = new Vector2(velocity.x, velocity.z);
        }

        private void HandleVerticalMovement(bool jumpPressed)
        {
            // Gravity accel
            _yVelocity += Physics.gravity.y * Time.deltaTime;

            // While grounded apply a downward force to stick to the ground
            if (_charController.isGrounded && _yVelocity < 0)
            {
                _yVelocity = -10f;
            }

            // Prevent the sticking force from causing a fast fall right after stepping off a ledge
            if (!_charController.isGrounded && _wasGrounded)
            {
                _yVelocity = Mathf.Max(_yVelocity, 0);
            }

            // Jump
            if (jumpPressed && _charController.isGrounded)
            {
                OnJump.Invoke();
                _yVelocity = _moveStats.JumpVelocity;
            }
        }

        private void HandleAnimation(Vector2 input)
        {
            // Smoothly rotate player to face movement direction
            if (input.magnitude > 0)
            {
                Quaternion currentRotation = _meshBody.rotation;
                var horizontalVel = new Vector3(input.x, 0, input.y);
                Quaternion targetRotation = Quaternion.LookRotation(horizontalVel);

                float t = 1 - Mathf.Pow(1 - 0.999f, Time.deltaTime);
                _meshBody.rotation = Quaternion.Lerp(currentRotation, targetRotation, t);
            }

            // Animation
            var finalState = 0;
            int currentState = _anim.GetInteger(_animState);

            if (_charController.isGrounded)
            {
                finalState = input.magnitude > 0 ? 1 : 0;
            }
            else
            {
                finalState = 2;
            }

            _anim.SetInteger(_animState, finalState);

            if (finalState == 1 && currentState != 1)
            {
                OnStartRunning.Invoke();
            }
            else if (finalState != 1 && currentState == 1)
            {
                OnStopRunning.Invoke();
            }
        }

        public void Launch(float jumpHeight)
        {
            _yVelocity = Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y);
        }
    }
}