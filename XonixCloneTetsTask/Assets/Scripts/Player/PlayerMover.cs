using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Player))]
public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float _speed;

    private Vector3 _direction;
    private Player _player;
    private float _elapsedTime;

    public Vector3 Direction => _direction;
    public Player Player => _player;

    public event UnityAction<Vector3> ChangePosition;
    public event UnityAction<Vector3> Grounded;

    private void Start()
    {
        _direction = Vector3.zero;
        _player = GetComponent<Player>();
    }

    private void Update()
    {
        _elapsedTime += Time.deltaTime;

        if (_elapsedTime >= (1f / _speed))
        {
            transform.Translate(_direction);
            CollisionDetection();
            ChangePosition?.Invoke(transform.position);
            _elapsedTime = 0;
        }
    }

    private void CollisionDetection()
    {
        IsHitCorner();

        var tileToCheck = GameField.Instance[transform.position];

        if (tileToCheck == GameFieldElement.Ground)
        {
            Grounded?.Invoke(_direction);
        }
        if (tileToCheck == GameFieldElement.Tail)
        {
            _player.ApplyDamage();
        }
    }

    private void IsHitCorner()
    {
        if (transform.position.x < 0)
        {
            ResetDirection();
            transform.position = new Vector2(0, transform.position.y);
        }
        if (transform.position.x > GameField.Instance.Width - 1)
        {
            ResetDirection();
            transform.position = new Vector2(GameField.Instance.Width - 1, transform.position.y);
        }
        if (transform.position.y < 0)
        {
            ResetDirection();
            transform.position = new Vector2(transform.position.x, 0);
        }
        if (transform.position.y > GameField.Instance.Height - 1)
        {
            ResetDirection();
            transform.position = new Vector2(transform.position.x, GameField.Instance.Height - 1);
        }
    }

    public void ResetDirection()
    {
        _direction = Vector3.zero;
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
        }
    }
}
