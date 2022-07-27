using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _stepSize;

    private Vector3 _targetPosition;
    private Player _player;

    private void Start()
    {
        _targetPosition = transform.position;
        _player = GetComponent<Player>();
    }

    private void Update()
    {
        if (_targetPosition != transform.position)
        {
            transform.position = Vector3.MoveTowards(transform.position, _targetPosition, _speed * Time.deltaTime);
        }
    }

    public void MoveUp()
    {
        if (_targetPosition.y < transform.position.y)
        {
            _player.ApplyDamage();
        }
        else
        {
            _targetPosition = new Vector2(transform.position.x, transform.position.y + _stepSize);
        }
    }

    public void MoveDown()
    {
        if (_targetPosition.y > transform.position.y)
        {
            _player.ApplyDamage();
        }
        else
        {
            _targetPosition = new Vector2(transform.position.x, transform.position.y - _stepSize);
        }
    }

    public void MoveLeft()
    {
        if (_targetPosition.x > transform.position.x)
        {
            _player.ApplyDamage();
        }
        else
        {
            _targetPosition = new Vector2(transform.position.x - _stepSize, transform.position.y);
        }
    }

    public void MoveRight()
    {
        if (_targetPosition.x < transform.position.x)
        {
            _player.ApplyDamage();
        }
        else
        {
            _targetPosition = new Vector2(transform.position.x + _stepSize, transform.position.y);
        }
    }
}
