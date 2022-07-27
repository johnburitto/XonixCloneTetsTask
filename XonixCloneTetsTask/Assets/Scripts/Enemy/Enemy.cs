using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _speed;

    private Vector3 _direction;
    private Vector3 _targetPosition;

    private void Start()
    {
        _direction = new Vector3(Random.Range(-3f, 3f), Random.Range(-3f, 3f), 0);
        _targetPosition = transform.position;
    }

    private void Update()
    {
        _targetPosition = transform.position + _direction;

        if (_targetPosition != transform.position)
        {
            transform.position = Vector3.MoveTowards(transform.position, _targetPosition, _speed * Time.deltaTime);
        }
    }

    public void ReverseDirection()
    {
        _direction *= -1f;
    }
}
