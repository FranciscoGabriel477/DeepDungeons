using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    public static GameInput instance;
    private PlayerInputs playerInputs;
    public event EventHandler OnJumpPressed;
    public event EventHandler OnJumpHelded;
    public event EventHandler OnAttackPressed;
    public event EventHandler OnDashPressed;
    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance= this;
            DontDestroyOnLoad(gameObject);
            playerInputs= new PlayerInputs();
        }
    }
    private void OnEnable()
    {
        playerInputs.Player.Enable();     
        playerInputs.Player.Jump.performed += JumpPressed;
        playerInputs.Player.Jump.canceled += JumpHelded;
        playerInputs.Player.Attack.performed += AttackPressed;
        playerInputs.Player.Dash.performed += DashPressed;
        
    }

    private void OnDisable()
    {
        playerInputs.Player.Disable();
        playerInputs.Player.Jump.performed -= JumpPressed;
        playerInputs.Player.Jump.canceled -= JumpHelded;
        playerInputs.Player.Attack.performed -= AttackPressed;
        playerInputs.Player.Dash.performed -= DashPressed;
    }

    public Vector2 GetNormalizedMovementInput()
    {
        return playerInputs.Player.Move.ReadValue<Vector2>().normalized;
    }

    public void JumpPressed(InputAction.CallbackContext context)
    {
        OnJumpPressed?.Invoke(this,EventArgs.Empty);
    }
    public void JumpHelded(InputAction.CallbackContext context)
    {
        OnJumpHelded?.Invoke(this,EventArgs.Empty);
    }
    public void AttackPressed(InputAction.CallbackContext context)
    {
        OnAttackPressed?.Invoke(this,EventArgs.Empty);
    }
    public void DashPressed(InputAction.CallbackContext context)
    {
        OnDashPressed?.Invoke(this,EventArgs.Empty);
    }
    
}
