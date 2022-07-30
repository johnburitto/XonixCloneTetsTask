using System;
using System.Collections;
using UnityEngine;

public class GroundEnemy : MonoBehaviour
{
    [SerializeField] private float _speed;

    private Vector3 _direction;

    private void Start()
    {
        _direction = new Vector2(UnityEngine.Random.Range(0, 2) * 2 - 1, UnityEngine.Random.Range(0, 2) * 2 - 1);
        StartCoroutine(Move());
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
        GameFieldElement tileToCheck;

        try
        {
            tileToCheck = GameField.Instance[transform.position];
        }
        catch (Exception e)
        {
            tileToCheck = GameFieldElement.None;
        }

        if (tileToCheck == GameFieldElement.None)
        {
            transform.position -= _direction;
            BounceDirection();
        }
        if (tileToCheck == GameFieldElement.Ground)
        {
            try
            {
                GameField.Instance[transform.position - _direction] = GameFieldElement.Ground;
                GameField.Instance[transform.position] = GameFieldElement.Enemy;
            }
            catch (Exception e)
            {
                GameField.Instance[transform.position] = GameFieldElement.Enemy;
            }
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

        try
        {
            if (GameField.Instance[transform.position + newDirection] == GameFieldElement.None)
            {
                _direction = newDirection * -1;
            }
            else
            {
                _direction = newDirection;
            }
        }
        catch (Exception e)
        {
            _direction = newDirection * -1;
        }
    }
}
