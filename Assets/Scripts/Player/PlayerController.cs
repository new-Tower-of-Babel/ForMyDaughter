using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Collider2D attackCollider;
    private Vector2 moveInput;
    private PlayerStats playerStats;
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer renderer;
    public AudioClip[] footstepClips;
    private AudioSource audioSource;
    public float footstepThreshold;
    public float footstepRate;
    private float footstepTime;
    private bool _isMoving = false;
    private bool _isJumping = false;
    private bool _isFalling = false;
    private bool _isSittingDown = false;
    private bool _isAttacking = false;
    private bool _isFacingRight = true;
    
    private void Awake()
    {
        playerStats = GetComponent<PlayerStats>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        renderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
    }
    
    private void FixedUpdate()
    {
        rb.velocity = new Vector2(moveInput.x * playerStats.runSpeed, rb.velocity.y);
    }

    private void Update()
    {
        CheckIfFalling();
        CheckIfLanded();
        if (Mathf.Abs(rb.velocity.magnitude) > footstepThreshold)
        {
            if (Time.time - footstepTime > footstepRate)
            {
                footstepTime = Time.time;
                audioSource.PlayOneShot(footstepClips[Random.Range(0, footstepClips.Length)]);
            }
        }
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
        if (context.performed)
        {
            _isAttacking = true;
            animator.SetTrigger("attack");
            attackCollider.enabled = true;
        }
    }
    
    public void EndAttack()
    {
        _isAttacking = false;
        attackCollider.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_isAttacking && other.CompareTag("Enemy"))
        {
            other.GetComponent<Thing>().Hit(playerStats.damage);
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
        if (moveInput.x > 0)
        {
            //face the right
            renderer.flipX = false;
            attackCollider.offset = new Vector2(Mathf.Abs(attackCollider.offset.x), attackCollider.offset.y);
        }
        else if (moveInput.x < 0)
        {
            //face the left
            renderer.flipX = true;
            attackCollider.offset = new Vector2(-Mathf.Abs(attackCollider.offset.x), attackCollider.offset.y);
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
}
