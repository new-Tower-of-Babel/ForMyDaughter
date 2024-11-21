using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float fallThreshold = -40f;
    [SerializeField] private Collider2D attackCollider;
    private Transform attackTransform;
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
        attackTransform = attackCollider.transform.parent;
    }
    
    private void FixedUpdate()
    {
        rb.velocity = new Vector2(moveInput.x * playerStats.runSpeed, rb.velocity.y);
    }

    private void Update()
    {
        CheckIfLanded();
        CheckFallBelowMap();
        if (Mathf.Abs(rb.velocity.magnitude) > footstepThreshold && CheckIfGrounded())
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

        if (other.CompareTag("Boss"))
        {
            playerStats.Death();
        }
    }
    
    private void CheckIfLanded()
    {
        if (IsJumping && Mathf.Approximately(rb.velocity.y, 0f))
        {
            IsJumping = false;
        }
    }

    private bool CheckIfGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.5f);
        bool isGrounded = hit.collider != null && hit.collider.gameObject.GetComponent<Renderer>().sortingLayerName == "Ground";
        return isGrounded;
    }

    private void CheckFallBelowMap()
    {
        if (transform.position.y < fallThreshold)
        {
            playerStats.Death();
        }
    }

    private void SetFacingDirection(Vector2 moveInput)
    {
        if (moveInput.x > 0)
        {
            //face the right
            renderer.flipX = false;
            attackTransform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (moveInput.x < 0)
        {
            //face the left
            renderer.flipX = true;
            attackTransform.rotation = Quaternion.Euler(0, 180, 0);
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
}
