using System.Collections;
using UnityEngine;

public class Thing : MonoBehaviour
{
    public enum WalkableDirection
    {
        Right,
        Left
    }

    private WalkableDirection _walkDirection;
    private Vector2 walkDirectionVector;
    private Color originalColor;
    private SpriteRenderer renderer;
    private Animator _animator;
    private Rigidbody2D rb;
    public float health = 30f;
    public float walkSpeed = 5f;

    public WalkableDirection WalkDirection
    {
        get { return _walkDirection; }
        set
        {
            if (_walkDirection != value)
            {
                _walkDirection = value;
                
                if (_walkDirection == WalkableDirection.Right)
                {
                    renderer.flipX = true;
                    walkDirectionVector = Vector2.right;
                }
                else if (_walkDirection == WalkableDirection.Left)
                {
                    renderer.flipX = false;
                    walkDirectionVector = Vector2.left;
                }
            }
        }
    }
    
    private void Awake()
    {
        renderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        WalkDirection = WalkableDirection.Left;
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(walkDirectionVector.x * walkSpeed, rb.velocity.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Renderer>().sortingLayerName == "Wall")
        {
            WalkDirection = (WalkDirection == WalkableDirection.Right) ? WalkableDirection.Left : WalkableDirection.Right;
        }
    }
    
    public void Hit(float damage)
    {
        if (0 < health)
        {
            health -= damage;
            StartCoroutine(ChangeColorOnHit());
            if (health <= 0)
            {
                Death();
            }
        }
    }

    private IEnumerator ChangeColorOnHit()
    {
        Color originalColor = renderer.color;
        renderer.color = Color.red;
        yield return new WaitForSeconds(0.3f);
        renderer.color = originalColor;
    }
    
    void Death()
    {
        _animator.SetTrigger("death");
        Destroy(this.gameObject, 0.5f);
    }
}