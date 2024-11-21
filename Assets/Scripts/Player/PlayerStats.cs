using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private float _maxHealth = 100f;
    [SerializeField] private float _currentHealth;
    public float runSpeed = 8f;
    public float jumpForce = 10f;
    public float damage = 10f;
    private Animator _animator;

    public float MaxHealth
    {
        get => _maxHealth;
        set => _maxHealth = value;
    }

    public float CurrentHealth
    {
        get => _currentHealth;
        set => _currentHealth = value;
    }

    public void Awake()
    {
        _animator = GetComponent<Animator>();
        _currentHealth = _maxHealth;
    }

    public void Heal(int healthRestore)
    {
        _currentHealth += healthRestore;

        if (_currentHealth >= _maxHealth)
        {
            _currentHealth = _maxHealth;
        }
    }

    public void Hit(float damage)
    {
        if (0 < _currentHealth)
        {
            _currentHealth -= damage;
            _animator.SetTrigger("hit");
            if (_currentHealth <= 0)
            {
                Death();
            }
        }
    }
    
    void Death()
    {
        _animator.SetTrigger("death");
        // Invoke("GameOver", 1.5f);
    }
}
