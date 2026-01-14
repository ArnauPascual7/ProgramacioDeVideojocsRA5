using Unity.Cinemachine;
using UnityEngine;

[RequireComponent(typeof(CharacterController), typeof(PlayerInputs))]
public class Prayer : MonoBehaviour
{
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private CinemachineCamera _playerCamera;

    public float walkSpeed = 3f;
    public float sprintSpeed = 6f;

    public float lookSensitivityH = 0.1f;
    public float lookSensitivityV = 0.1f;
    public float lookLimitV = 80.0f;

    private PlayerInputs _playerInputs;
    private Vector3 _currentMovement = Vector3.zero;
    private Vector2 _cameraRotation = Vector2.zero;

    private void Awake()
    {
        _playerInputs = GetComponent<PlayerInputs>();
    }

    private void Update()
    {
        Movement();
        Rotation();
    }

    private void Movement()
    {
        Vector3 moveDirection = new Vector3(_playerInputs.MoveInput.x, 0, _playerInputs.MoveInput.y);

        _currentMovement = _playerInputs.SprintInput ? moveDirection * sprintSpeed : moveDirection * walkSpeed;

        _characterController.Move(_currentMovement * Time.deltaTime);
    }

    private void Rotation()
    {
        _cameraRotation.x += _playerInputs.LookInput.x * lookSensitivityH;
        _cameraRotation.y -= _playerInputs.LookInput.y * lookSensitivityV;
        _cameraRotation.y = Mathf.Clamp(_cameraRotation.y, -lookLimitV, lookLimitV);

        transform.rotation = Quaternion.Euler(0, _cameraRotation.x, 0);
        _playerCamera.transform.localRotation = Quaternion.Euler(_cameraRotation.y, 0, 0);
    }
}
