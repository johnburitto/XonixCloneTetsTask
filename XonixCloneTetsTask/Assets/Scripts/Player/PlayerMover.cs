using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Player))]
public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float _speed;

    private Vector3 _direction;
    private Player _player;

    public Vector3 Direction => _direction;

    public event UnityAction<Vector3> ChangeDirection;
    public event UnityAction<Vector3> ChangePosition;
    public event UnityAction<Vector3> Grounded;

    private void Start()
    {
        _direction = Vector3.zero;
        _player = GetComponent<Player>();
        StartCoroutine(Move());
    }

    private void Update()
    {
        var tileToCheck = GameField.Instance[transform.position];

        if (tileToCheck)
        {
            if (tileToCheck.gameObject.TryGetComponent(out GroundTail groundTail))
            {
                Grounded?.Invoke(_direction);
            }
        }
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
        if (transform.position.x < 0)
        {
            _direction = Vector3.zero;
            transform.position = new Vector2(0, transform.position.y);
        }
        if (transform.position.x > GameField.Instance.Width)
        {
            _direction = Vector3.zero;
            transform.position = new Vector2(GameField.Instance.Width, transform.position.y);
        }
        if (transform.position.y < 0)
        {
            _direction = Vector3.zero;
            transform.position = new Vector2(transform.position.x, 0);
        }
        if (transform.position.y > GameField.Instance.Height)
        {
            _direction = Vector3.zero;
            transform.position = new Vector2(transform.position.x, GameField.Instance.Height);
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
