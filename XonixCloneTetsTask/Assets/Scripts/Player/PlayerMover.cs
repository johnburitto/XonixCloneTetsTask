using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Player))]
public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float _startX;
    [SerializeField] private float _endX;
    [SerializeField] private float _startY;
    [SerializeField] private float _endY;
    [SerializeField] private float _speed;

    private Vector3 _direction;
    private Player _player;

    public event UnityAction<Vector3> ChangeDirection;
    public event UnityAction<Vector3> ChangePosition;

    private void Start()
    {
        _direction = Vector3.zero;
        _player = GetComponent<Player>();
        StartCoroutine(Move());
    }

    private IEnumerator Move()
    {
        while (true)
        {
            if (_direction != Vector3.zero)
            {
                ChangePosition?.Invoke(transform.position);
            }
            
            transform.position += _direction;
            IsHitCorner();

            yield return new WaitForSeconds(1 / _speed);
        }
    }

    private void IsHitCorner()
    {
        if (transform.position.x < _startX)
        {
            _direction = Vector3.zero;
            transform.position = new Vector2(_startX, transform.position.y);
        }
        if (transform.position.x > _endX)
        {
            _direction = Vector3.zero;
            transform.position = new Vector2(_endX, transform.position.y);
        }
        if (transform.position.y > _startY)
        {
            _direction = Vector3.zero;
            transform.position = new Vector2(transform.position.x, _startY);
        }
        if (transform.position.y < _endY)
        {
            _direction = Vector3.zero;
            transform.position = new Vector2(transform.position.x, _endY);
        }
    }

    public void MoveUp()
    {
        if (_direction == Vector3.down)
        {
            _player.ApplyDamage();
        }
        else
        {
            _direction = Vector3.up;
            ChangeDirection?.Invoke(transform.position);
        }
    }

    public void MoveDown()
    {
        if (_direction == Vector3.up)
        {
            _player.ApplyDamage();
        }
        else
        {
            _direction = Vector3.down;
            ChangeDirection?.Invoke(transform.position);
        }
    }

    public void MoveLeft()
    {
        if (_direction == Vector3.right)
        {
            _player.ApplyDamage();
        }
        else
        {
            _direction = Vector3.left;
            ChangeDirection?.Invoke(transform.position);
        }
    }

    public void MoveRight()
    {
        if (_direction == Vector3.left)
        {
            _player.ApplyDamage();
        }
        else
        {
            _direction = Vector3.right;
            ChangeDirection?.Invoke(transform.position);
        }
    }
}
