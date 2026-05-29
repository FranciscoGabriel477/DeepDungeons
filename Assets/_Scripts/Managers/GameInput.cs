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
    public event EventHandler OnBlockPressed;
    public event EventHandler OnBlockReleased;
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
        playerInputs.Player.Block.performed += BlockPressed;
        playerInputs.Player.Block.canceled += BlockReleased;
        
    }

    private void OnDisable()
    {
        playerInputs.Player.Disable();
        playerInputs.Player.Jump.performed -= JumpPressed;
        playerInputs.Player.Jump.canceled -= JumpHelded;
        playerInputs.Player.Attack.performed -= AttackPressed;
        playerInputs.Player.Dash.performed -= DashPressed;
        playerInputs.Player.Block.performed -= BlockPressed;
        playerInputs.Player.Block.canceled -= BlockReleased;
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
    public void BlockPressed(InputAction.CallbackContext context)
    {
        OnBlockPressed?.Invoke(this,EventArgs.Empty);
    }
    public void BlockReleased(InputAction.CallbackContext context)
    {
        OnBlockReleased?.Invoke(this,EventArgs.Empty);
    }
    
}
