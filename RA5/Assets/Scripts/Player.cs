using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour, InputSystem_Actions.IPlayerActions
{
    // https://www.youtube.com/watch?v=muAzcpAg3lg
    public Animator Animator;

    [SerializeField] private float walkSpeed = 3f;
    [SerializeField] private float sprintMultiplier = 2f;

    [SerializeField] private float mouseSensitivity = 2f;
    [SerializeField] private float upDownRange = 80f;

    private InputSystem_Actions _inputActions;

    private CharacterController _characterController;
    private Camera _mainCamera;

    private Vector2 _direction;
    private Vector2 _look;
    private float _speedMultiplier = 1f;
    private float _verticalRotation;
    private Vector3 currentMovement = Vector3.zero;
    private bool isDancing = false;
    private bool isMoving = false;

    private void Awake()
    {
        _inputActions = new InputSystem_Actions();
        _inputActions.Player.SetCallbacks(this);

        _characterController = GetComponent<CharacterController>();
        _mainCamera = Camera.main;
    }

    private void OnEnable()
    {
        _inputActions.Enable();
    }

    private void OnDisable()
    {
        _inputActions.Disable();
    }

    private void Update()
    {
        Movement();
        Rotation();
    }

    private void Movement()
    {
        //Animator.SetFloat("Velocity", _direction.magnitude);

        isMoving = _direction.magnitude > 0;
        Animator.SetFloat("Velocity", isMoving ? walkSpeed * _speedMultiplier : 0);

        float verticalSpeed = _direction.y * walkSpeed * _speedMultiplier;
        float horizontalSpeed = _direction.x * walkSpeed * _speedMultiplier;


        Vector3 horizontalMovement = new Vector3(horizontalSpeed, 0, verticalSpeed);
        horizontalMovement = transform.rotation * horizontalMovement;

        currentMovement.x = horizontalMovement.x;
        currentMovement.z = horizontalMovement.z;

        _characterController.Move(currentMovement * Time.deltaTime);
    }

    private void Rotation()
    {
        float mouseXRotation = _look.x * mouseSensitivity;
        transform.Rotate(0, mouseXRotation, 0);

        _verticalRotation -= _look.y * mouseSensitivity;
        _verticalRotation = Mathf.Clamp(_verticalRotation, -upDownRange, upDownRange);
        _mainCamera.transform.localRotation = Quaternion.Euler(_verticalRotation, 0, 0);
    }


    public void OnAttack(InputAction.CallbackContext context)
    {

    }

    public void OnJump(InputAction.CallbackContext context)
    {

    }

    public void OnLook(InputAction.CallbackContext context)
    {
        _look = context.ReadValue<Vector2>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        _direction = context.ReadValue<Vector2>();

        isDancing = false;
        Animator.SetBool("Dance", isDancing);
    }

    public void OnSprint(InputAction.CallbackContext context)
    {
        _speedMultiplier = context.ReadValue<float>() > 0 ? sprintMultiplier : 1f;

        /*if (context.started)
        {
            Animator.SetBool("Sprint", true);
        }
        else if (context.canceled)
        {
            Animator.SetBool("Sprint", false);
        }*/
    }

    public void OnDance(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            isDancing = !isMoving && !isDancing;

            Animator.SetBool("Dance", isDancing);
        }
    }
}
