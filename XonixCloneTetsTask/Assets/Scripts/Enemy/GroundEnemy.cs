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

    private void Update()
    {
        GameObject tileToCheck;

        try 
        {
            tileToCheck = GameField.Instance[transform.position];
        }
        catch (Exception e)
        {
            tileToCheck = null;
        }

        if (!tileToCheck)
        {
            transform.position -= _direction;
            BounceDirection();
        }
    }

    private IEnumerator Move()
    {
        while (true)
        {
            transform.position += _direction;

            yield return new WaitForSeconds(1 / _speed);
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
            if (!GameField.Instance[transform.position + newDirection])
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
