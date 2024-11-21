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
    private SpriteRenderer renderer;
    private Animator _animator;
    private Rigidbody2D rb;
    private Color originalColor;
    public float health = 30f;
    public float walkSpeed = 5f;

    public WalkableDirection WalkDirection
    {
        get { return _walkDirection; }
        set
        {
            if (_walkDirection != value)
            {
                gameObject.transform.localScale = new Vector2(gameObject.transform.localScale.x * -1, gameObject.transform.localScale.y);

                if (value == WalkableDirection.Right)
                {
                    walkDirectionVector = Vector2.right;
                }
                else if (value == WalkableDirection.Left)
                {
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

    private void FixedUpdate()
    {
        // //enemy flip when touch the wall
        //  if ()
        //  {
        //      renderer.flipX = false;
        //  }
        //  else if ()
        //  {
        //      renderer.flipX = true;
        //  }
         
        rb.velocity = new Vector2(walkSpeed * walkDirectionVector.x, rb.velocity.y);
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
        Destroy(this.gameObject, 1f);
    }
}