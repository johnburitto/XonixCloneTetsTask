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
        var tileToCheck = GameField.Instance[transform.position];

        if (tileToCheck)
        {
            if (tileToCheck.gameObject.TryGetComponent(out GroundTail groundTail))
            {
                transform.position -= _direction;
                BounceDirection();
            }
            if (tileToCheck.gameObject.TryGetComponent(out TailTail tailTail))
            {
                transform.position -= _direction;
                _direction = Vector3.zero;
                tailTail.Player.ApplyDamage();
            }
        }
        else
        {
            GameField.Instance[transform.position - _direction] = null;
            GameField.Instance[transform.position] = gameObject;
        };
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

        if (GameField.Instance[transform.position + newDirection])
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
            if (BlockedSides() > 2)
            {
                Destroy(gameObject);
            }
        }
        catch (Exception e)
        {
            Destroy(gameObject);
        }
    }

    private int BlockedSides()
    {
        int numberOfSides = 0;

        if (GameField.Instance[transform.position + Vector3.up])
        {
            numberOfSides++;
        }
        if (GameField.Instance[transform.position + Vector3.down])
        {
            numberOfSides++;
        }
        if (GameField.Instance[transform.position + Vector3.left])
        {
            numberOfSides++;
        }
        if (GameField.Instance[transform.position + Vector3.right])
        {
            numberOfSides++;
        }

        return numberOfSides;
    }
}
