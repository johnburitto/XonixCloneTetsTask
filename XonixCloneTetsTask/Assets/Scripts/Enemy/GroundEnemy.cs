using System;
using UnityEngine;

public class GroundEnemy : MonoBehaviour
{
    [SerializeField] private float _speed;

    private Vector3 _direction;
    private float _elapsedTime;

    private void Start()
    {
        _direction = new Vector2(UnityEngine.Random.Range(0, 2) * 2 - 1, UnityEngine.Random.Range(0, 2) * 2 - 1);
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
            _direction = GetBounceDirection();
        }
        if (transform.position == Player.Instance.transform.position)
        {
            Player.Instance.ApplyDamage();
        }
    }

    private Vector3 GetBounceDirection()
    {
        Vector3 newDirection = new Vector2(-_direction.y, _direction.x);

        try
        {
            if (GameField.Instance[transform.position + newDirection] == GameFieldElement.None)
            {
                return newDirection * -1;
            }
            else
            {
                return newDirection;
            }
        }
        catch (Exception e)
        {
            return newDirection * -1;
        }
    }
}
