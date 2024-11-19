using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private float _maxHealth = 100f;
    private float _currentHealth;
    public float runSpeed = 8f;
    public float jumpForce = 10f;
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

    public void Hit(float damage)
    {
        if (0 < _currentHealth)
        {
            _currentHealth -= damage;
            if (_currentHealth <= 0)
            {
                Death();
            }
        }
    }
    void Death()
    {
        _animator.SetTrigger("Death");
        Invoke("GameOver", 1.5f);
    }
}
