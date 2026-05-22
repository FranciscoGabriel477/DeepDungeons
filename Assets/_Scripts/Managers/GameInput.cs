using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    public static GameInput instance;
    private PlayerInputs playerInputs;
    public event EventHandler OnJumpPressed;
    public event EventHandler OnJumpHelded;
    public event EventHandler OnRunPressed;
    public event EventHandler OnRunHelded;
    public event EventHandler OnAttackPressed;
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
            playerInputs.Player.Jump.performed += JumpPressed;
            playerInputs.Player.Jump.canceled += JumpHelded;
            playerInputs.Player.Run.performed += RunPressed;
            playerInputs.Player.Attack.performed += AttackPressed;
            playerInputs.Player.Run.canceled += RunHelded;
        }
    }

    private void Start()
    {
        playerInputs.Player.Enable();
    }

    private void OnDestroy()
    {
        playerInputs.Player.Disable();
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
    public void RunPressed(InputAction.CallbackContext context)
    {
        OnRunPressed?.Invoke(this,EventArgs.Empty);
    }
    public void AttackPressed(InputAction.CallbackContext context)
    {
        OnAttackPressed?.Invoke(this,EventArgs.Empty);
    }
    public void RunHelded(InputAction.CallbackContext context)
    {
        OnRunHelded?.Invoke(this,EventArgs.Empty);
    }
    
}
