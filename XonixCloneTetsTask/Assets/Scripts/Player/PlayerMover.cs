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

    public event UnityAction<Vector3> ChangePosition;
    public event UnityAction<Vector3> Grounded;

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

            CollisionDetection();

            yield return new WaitForSeconds(1 / _speed);
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
        if (tileToCheck == GameFieldElement.Enemy || tileToCheck == GameFieldElement.Tail)
        {
            _player.ApplyDamage();
        }
    }

    private void IsHitCorner()
    {
        if (transform.position.x < 0)
        {
            _direction = Vector3.zero;
            transform.position = new Vector2(0, transform.position.y);
        }
        if (transform.position.x > GameField.Instance.Width - 1)
        {
            _direction = Vector3.zero;
            transform.position = new Vector2(GameField.Instance.Width - 1, transform.position.y);
        }
        if (transform.position.y < 0)
        {
            _direction = Vector3.zero;
            transform.position = new Vector2(transform.position.x, 0);
        }
        if (transform.position.y > GameField.Instance.Height - 1)
        {
            _direction = Vector3.zero;
            transform.position = new Vector2(transform.position.x, GameField.Instance.Height - 1);
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

    public void ResetDirection()
    {
        _direction = Vector3.zero;
    }
}
