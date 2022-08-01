using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class SeaEnemy : MonoBehaviour
{
    [SerializeField] private float _speed;

    private Vector3 _direction;

    public event UnityAction Dead;

    private void OnEnable()
    {
        Dead += Die;
    }

    private void OnDisable()
    {
        Dead += Die;
    }

    private void Start()
    {
        _direction = new Vector2(UnityEngine.Random.Range(0, 2) * 2 - 1, UnityEngine.Random.Range(0, 2) * 2 - 1);
        StartCoroutine(Move());
    }

    private void Update()
    {
        Dead?.Invoke();
    }

    private IEnumerator Move()
    {
        while (true)
        {
            transform.position += _direction;

            CollisionDetection();

            yield return new WaitForSeconds(1 / _speed);
        }
    }

    private void CollisionDetection()
    {
        var tileToCheck = GameField.Instance[transform.position];

        if (tileToCheck == GameFieldElement.Ground)
        {
            transform.position -= _direction;
            BounceDirection();
        }
        if (tileToCheck == GameFieldElement.Tail)
        {
            transform.position -= _direction;
            Player.Instance.ApplyDamage();
        }
        if (tileToCheck == GameFieldElement.None)
        {
            GameField.Instance[transform.position - _direction] = GameFieldElement.None;
            GameField.Instance[transform.position] = GameFieldElement.Enemy;
        }
    }

    private void BounceDirection()
    {
        Vector3 newDirection = Vector3.zero;

        if (_direction.x > 0)
        {
            if (_direction.y < 0)
            {
                newDirection = new Vector2(-_direction.x, _direction.y);
            }
            else
            {
                newDirection = new Vector2(_direction.x, -_direction.y);
            }
        }
        else
        {
            if (_direction.y < 0)
            {
                newDirection = new Vector2(_direction.x, -_direction.y);
            }
            else
            {
                newDirection = new Vector2(-_direction.x, _direction.y);
            }
        }

        if (GameField.Instance[transform.position + newDirection] == GameFieldElement.Ground)
        {
            _direction = newDirection * -1;
        }
        else
        {
            _direction = newDirection;
        }
    }

    private void Die()
    {
        try
        {
            if (IsBlocked())
            {
                GameField.Instance[transform.position] = GameFieldElement.Ground;
                Destroy(gameObject);
            }
        }
        catch (Exception e)
        {
            GameField.Instance[transform.position] = GameFieldElement.Ground;
            Destroy(gameObject);
        }
    }

    private bool IsBlocked()
    {
        if (GameField.Instance[transform.position + Vector3.up] != GameFieldElement.None &&
            GameField.Instance[transform.position + Vector3.down] != GameFieldElement.None &&
            GameField.Instance[transform.position + Vector3.left] != GameFieldElement.None &&
            GameField.Instance[transform.position + Vector3.right] != GameFieldElement.None)
        {
            return true;
        }

        return false;
    }
}
