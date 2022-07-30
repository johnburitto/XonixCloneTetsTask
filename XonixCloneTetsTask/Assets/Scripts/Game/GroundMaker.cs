using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GroundMaker : MonoBehaviour
{
    [SerializeField] private Tiles _tails;
    [SerializeField] private GameObject _tailContainer;
    [SerializeField] private PlayerMover _playerMover;
    [SerializeField] private Player _player;

    private List<GameObject> _createdTails = new List<GameObject>();

    public event UnityAction CapturingTerritory;

    private void OnEnable()
    {
        _player.GetDamage += OnGetDamage;
        _playerMover.ChangePosition += OnChangePosition;
        _playerMover.Grounded += CreateSaveArea;
    }

    private void OnDisable()
    {
        _player.GetDamage -= OnGetDamage;
        _playerMover.ChangePosition -= OnChangePosition;
        _playerMover.Grounded -= CreateSaveArea;
    }

    public void CreateGamePlace()
    {
        for (int i = 0; i < GameField.Instance.Width; i++)
        {
            for (int j = 0; j < GameField.Instance.Height; j++)
            {
                if ((i == 0 || i == GameField.Instance.Width - 1) ||
                    (j == 0 || j == GameField.Instance.Height - 1) || 
                    (i == 1 || i == GameField.Instance.Width - 2) ||
                    (j == 1 || j == GameField.Instance.Height - 2))
                {
                    var created = Instantiate(_tails.GroundTemplate, GameField.Instance.transform);

                    created.transform.position = new Vector2(i, j);
                    GameField.Instance[i, j] = GameFieldElement.Ground;
                }
            }
        }
    }

    public void ResetGamePlace()
    {
        GameField.Instance.ResetGameField();
        CreateGamePlace();
    }

    private void OnGetDamage()
    {
        foreach (Transform element in _tailContainer.transform)
        {
            GameField.Instance[element.position] = GameFieldElement.None;
            Destroy(element.gameObject);
        }

        _createdTails.Clear();
    }

    public void OnChangePosition(Vector3 position)
    {
        if (GameField.Instance[position] == GameFieldElement.None)
        {
            var created = Instantiate(_tails.TailTemplate, _tailContainer.transform);
            var player = _playerMover.gameObject.GetComponent<Player>();

            created.transform.position = position;
            created.gameObject.GetComponent<TailTail>().Init(player);
            _createdTails.Add(created);
            GameField.Instance[created.transform.position] = GameFieldElement.Tail;
        }
    }

    public void CreateSaveArea(Vector3 direction)
    {
        if (_createdTails.Count > 0)
        {
            if (IsLine())
            {
                FillUnderLine(direction);
            }
            else
            {
                FillPoly(direction);
            }

            DeleteAllTailTails();
        }

        CapturingTerritory?.Invoke();

        return;
    }

    private bool IsLine()
    {
        float firstX = _createdTails[0].transform.position.x;
        float firstY = _createdTails[0].transform.position.y;

        for (int i = 1; i < _createdTails.Count; i++)
        {
            if (_createdTails[i].transform.position.x != firstX &&
                _createdTails[i].transform.position.y != firstY)
            {
                return false;
            }
        }

        return true;
    }

    private void FillUnderLine(Vector3 direction)
    {
        Vector3 newDirection = Vector3.zero;

        if (direction.x != 0)
        {
            if (_playerMover.transform.position.y <= GameField.Instance.Height / 2)
            {
                newDirection = Vector3.down;
            }
            else
            {
                newDirection = Vector3.up;
            }
        }
        else
        {
            if (_playerMover.transform.position.x <= GameField.Instance.Width / 2)
            {
                newDirection = Vector3.left;
            }
            else
            {
                newDirection = Vector3.right;
            }
        }

        FillPoly(newDirection);
    }

    private void FillPoly(Vector3 direction)
    {
        for (int i = 0; i < _createdTails.Count; i++)
        {
            GameField.Instance[_createdTails[i].transform.position] = GameFieldElement.None;

            var tileToCheck = GameField.Instance[_createdTails[i].transform.position];
            int index = 0;

            while (tileToCheck == GameFieldElement.None)
            {
                var created = Instantiate(_tails.GroundTemplate, GameField.Instance.transform);

                created.transform.position = _createdTails[i].transform.position + (index * direction);
                GameField.Instance[created.transform.position] = GameFieldElement.Ground;
                index++;
                tileToCheck = GameField.Instance[_createdTails[i].transform.position + (index * direction)];

                if (tileToCheck == GameFieldElement.Enemy)
                {
                    GameField.Instance[_createdTails[i].transform.position + (index * direction)] = GameFieldElement.None;
                    tileToCheck = GameFieldElement.None;
                }
            }
        }
    }

    private void DeleteAllTailTails()
    {
        _createdTails.Clear();

        foreach (Transform element in _tailContainer.transform)
        {
            Destroy(element.gameObject);
        }
    }
}