using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputs : MonoBehaviour, InputSystem_Actions.IPlayerActions
{
    public InputSystem_Actions InputActions { get; private set; }
    public Vector3 MoveInput { get; private set; }
    public bool SprintInput { get; private set; }
    public bool JumpInput { get; private set; }
    public Vector2 LookInput { get; private set; }
    public bool DanceInput { get; private set; }
    
    private void OnEnable()
    {
        InputActions = new InputSystem_Actions();
        InputActions.Enable();

        InputActions.Player.Enable();
        InputActions.Player.SetCallbacks(this);
    }

    private void OnDisable()
    {
        InputActions.Player.Disable();
        InputActions.Disable();
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        
    }

    public void OnDance(InputAction.CallbackContext context)
    {
        DanceInput = context.ReadValueAsButton();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        JumpInput = context.ReadValueAsButton();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        LookInput = context.ReadValue<Vector2>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        MoveInput = context.ReadValue<Vector2>();
    }

    public void OnSprint(InputAction.CallbackContext context)
    {
        SprintInput = context.ReadValueAsButton();
    }
}
