using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [SerializeField] private int _health;

    public static Player Instance;
    public int Health => _health;

    public event UnityAction GetDamage;
    public event UnityAction Dead;
    public event UnityAction HealthChange;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        HealthChange?.Invoke();
    }

    private void Update()
    {
        if (_health <= 0)
        {
            Die();
        }
    }

    public void ResetPlayer(int health)
    {
        _health = health;
        HealthChange?.Invoke();
        transform.position = Vector3.zero;
        gameObject.GetComponent<PlayerMover>().ResetDirection();
    }

    public void ApplyDamage()
    {
        _health--;
        HealthChange?.Invoke();
        GetDamage?.Invoke();
    }

    public void Die()
    {
        Dead?.Invoke();
    }
}
