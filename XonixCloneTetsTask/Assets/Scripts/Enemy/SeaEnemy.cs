using UnityEngine;

public class SeaEnemy : MonoBehaviour
{
    [SerializeField] private float _speed;

    private Vector3 _direction;
    private float _elapsedTime;

    private void Start()
    {
        _direction = new Vector2(Random.Range(0, 2) * 2 - 1, Random.Range(0, 2) * 2 - 1);
    }

    private void Update()
    {
        _elapsedTime += Time.deltaTime;

        if (_elapsedTime >= (1f / _speed))
        {
            transform.position += _direction;
            CollisionDetection();
            _elapsedTime = 0;
        }
    }

    private void CollisionDetection()
    {
        var tileToCheck = GameField.Instance[transform.position];

        if (tileToCheck == GameFieldElement.Ground)
        {
            transform.position -= _direction;
            _direction = GetBounceDirection();
        }
        if (tileToCheck == GameFieldElement.Tail)
        {
            transform.position -= _direction;
            Player.Instance.ApplyDamage();
        }
        if (transform.position == Player.Instance.transform.position)
        {
            Player.Instance.ApplyDamage();
        }
        if (IsBlocked())
        {
            Die();
        }
    }

    private bool IsBlocked()
    {
        if (GameField.Instance[transform.position + Vector3.up] == GameFieldElement.Ground &&
            GameField.Instance[transform.position + Vector3.down] == GameFieldElement.Ground &&
            GameField.Instance[transform.position + Vector3.left] == GameFieldElement.Ground &&
            GameField.Instance[transform.position + Vector3.right] == GameFieldElement.Ground)
        {
            return true;
        }

        return false;
    }
    
    private void Die()
    {
        GameField.Instance[transform.position] = GameFieldElement.Ground;
        Destroy(gameObject);
    }

    private Vector3 GetBounceDirection()
    {
        Vector3 newDirection = new Vector2(-_direction.y, _direction.x);

        if (GameField.Instance[transform.position + newDirection] == GameFieldElement.Ground)
        {
            return newDirection * -1;
        }
        else
        {
            return newDirection;
        }
    }
}
