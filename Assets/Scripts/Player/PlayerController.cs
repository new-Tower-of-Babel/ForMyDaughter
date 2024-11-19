using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    
    private Vector2 moveInput;
    private PlayerStats playerStats;
    private Rigidbody2D rb;
    private Animator animator;
    private bool _isMoving = false;
    private bool _isJumping = false;
    private bool _isFalling = false;
    private bool _isSittingDown = false;
    private bool _isAttacking = false;
    private bool _isFacingRight = true;
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        playerStats = GetComponent<PlayerStats>();
    }
    
    private void FixedUpdate()
    {
        rb.velocity = new Vector2(moveInput.x * playerStats.runSpeed, rb.velocity.y);
    }

    private void Update()
    {
        CheckIfFalling();
        CheckIfLanded();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        IsMoving = moveInput != Vector2.zero;
        SetFacingDirection(moveInput);
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed && !IsJumping)
        {
            rb.velocity = new Vector2(rb.velocity.x, playerStats.jumpForce);
            IsJumping = true;
            _isFalling = false;
        }
    }

    public void OnSit(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            IsSittingDown = true;
            IsMoving = false;
        }
        else if (context.canceled)
        {
            IsSittingDown = false;
        }
    }
    
    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.performed && !_isAttacking)
        {
            _isAttacking = true;
            animator.SetTrigger("attack");
        }
    }

    private void CheckIfFalling()
    {
        if (rb.velocity.y < 0 && !IsJumping)
        {
            IsFalling = true;
        }
    }
    
    private void CheckIfLanded()
    {
        if ((IsJumping || IsFalling) && rb.velocity.y == 0)
        {
            IsJumping = false;
            IsFalling = false;
        }
    }

    private void SetFacingDirection(Vector2 moveInput)
    {
        if (moveInput.x > 0 && !IsFacingRight)
        {
            //face the right
            IsFacingRight = true;
        }
        else if (moveInput.x < 0 && IsFacingRight)
        {
            //face the left
            IsFacingRight = false;
        }
    }
    
    public bool IsMoving
    {
        get => _isMoving;
        private set
        {
            _isMoving = value;
            animator.SetBool("isMoving", value);
        }
    }

    public bool IsJumping
    {
        get => _isJumping;
        set
        {
            _isJumping = value;
            animator.SetBool("isJumping", value);
        }
    }

    public bool IsFalling
    {
        get => _isFalling;
        set
        {
            _isFalling = value;
            animator.SetBool("isFalling", value);
        }
    }

    public bool IsSittingDown
    {
        get => _isSittingDown;
        set
        {
            _isSittingDown = value;
            animator.SetBool("isSitting", value);
        }
    }
    
    public bool IsFacingRight
    {
        get => _isFacingRight;
        set
        {
            if (_isFacingRight != value)
            {
                transform.localScale *= new Vector2(-1, 1);
            }
            
            _isFacingRight = value;
        }
    }
}
