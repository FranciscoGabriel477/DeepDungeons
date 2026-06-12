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
    public event EventHandler OnSkill1Pressed;
    public event EventHandler OnSkill2Pressed;
    public event EventHandler OnSkill1Helded;
    public event EventHandler OnSkill2Helded;
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
        playerInputs.Player.Skill1.performed += Skill1Pressed;
        playerInputs.Player.Skill2.performed += Skill2Pressed;
        playerInputs.Player.Skill1.canceled += Skill1Helded;
        playerInputs.Player.Skill2.canceled += Skill2Helded;
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
        playerInputs.Player.Skill1.performed -= Skill1Pressed;
        playerInputs.Player.Skill2.performed -= Skill2Pressed;
        playerInputs.Player.Skill1.canceled -= Skill1Helded;
        playerInputs.Player.Skill2.canceled -= Skill2Helded;
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
    public void Skill1Pressed(InputAction.CallbackContext context)
    {
        OnSkill1Pressed?.Invoke(this,EventArgs.Empty);
    }
    public void Skill2Pressed(InputAction.CallbackContext context)
    {
        OnSkill2Pressed?.Invoke(this,EventArgs.Empty);
    }
    public void Skill1Helded(InputAction.CallbackContext context)
    {
        OnSkill1Helded?.Invoke(this,EventArgs.Empty);
    }
    public void Skill2Helded(InputAction.CallbackContext context)
    {
        OnSkill2Helded?.Invoke(this,EventArgs.Empty);
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
