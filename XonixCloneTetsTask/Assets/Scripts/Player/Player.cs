using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [SerializeField] private int _health;

    public event UnityAction Dead;

    private void Update()
    {
        if (_health <= 0)
        {
            Die();
        }
    }

    public void ApplyDamage()
    {
        _health--;
    }

    public void Die()
    {
        Dead?.Invoke();
        Destroy(gameObject);
    }
}
