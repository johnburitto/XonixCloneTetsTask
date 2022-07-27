using UnityEngine;

[RequireComponent(typeof(PlayerMover))]
public class PlayerInput : MonoBehaviour
{
    private PlayerMover _playerMover;

    private void Start()
    {
        _playerMover = GetComponent<PlayerMover>();
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.W))
        {
            _playerMover.MoveUp();
        }

        if (Input.GetKeyUp(KeyCode.S))
        {
            _playerMover.MoveDown();
        }
        
        if (Input.GetKeyUp(KeyCode.A))
        {
            _playerMover.MoveLeft();
        }
        
        if (Input.GetKeyUp(KeyCode.D))
        {
            _playerMover.MoveRight();
        }
    }
}
