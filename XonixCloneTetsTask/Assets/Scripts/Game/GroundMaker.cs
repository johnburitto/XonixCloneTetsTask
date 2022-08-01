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
        DeleteAllTailTiles();
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
            created.gameObject.GetComponent<TailTile>().Init(player);
            _createdTails.Add(created);
            GameField.Instance[created.transform.position] = GameFieldElement.Tail;
        }
        if (GameField.Instance[position] == GameFieldElement.Enemy)
        {
            DeleteAllTailTiles();
            _player.ApplyDamage();
        }
    }

    public void CreateSaveArea(Vector3 direction)
    {
        if (_createdTails.Count > 0)
        {
            CreateEdge();
            
            if (IsLine(direction))
            {
                FillUnderLine(direction);
            }
            else
            {
                Vector3 seedPosition = GenerateSeedPosition(direction);
                FillPoly(seedPosition);
            }

            DeleteAllTailTiles();
        }

        CapturingTerritory?.Invoke();
    }

    private void CreateEdge()
    {
        for (int i = 0; i < _createdTails.Count; i++)
        {
            var created = Instantiate(_tails.GroundTemplate, GameField.Instance.transform);

            created.transform.position = _createdTails[i].transform.position;
            GameField.Instance[created.transform.position] = GameFieldElement.Ground;
        }
    }

    private bool IsLine(Vector3 direction)
    {
        float firstX = _createdTails[0].transform.position.x;
        float firstY = _createdTails[0].transform.position.y;

        for (int i = 1; i < _createdTails.Count; i++)
        {
            if (_createdTails[i].transform.position.x != firstX &&
                _createdTails[i].transform.position.y != firstY)
            {
                if (direction.y != 0)
                {
                    return IsBrokenVerticalLine(direction.y);
                }
                if (direction.x != 0)
                {
                    return IsBrokenHorizontalLine(direction.x);
                } 
            }
        }

        return true;
    }

    private bool IsBrokenVerticalLine(float idealValue)
    {
        if (_createdTails[_createdTails.Count - 1].transform.position.y - _createdTails[_createdTails.Count - 2].transform.position.y == 0)
        {
            return false;
        } 
        
        if (_createdTails[0].transform.position.y - _createdTails[1].transform.position.y == 0)
        {
            return false;
        } 

        for (int i = 0; i < _createdTails.Count - 1; i++)
        {
            if ((_createdTails[i + 1].transform.position.y - _createdTails[i].transform.position.y) == -idealValue)
            {
                return false;
            }
        }

        return true;
    }
    
    private bool IsBrokenHorizontalLine(float idealValue)
    {
        if (_createdTails[_createdTails.Count - 1].transform.position.x - _createdTails[_createdTails.Count - 2].transform.position.x == 0)
        {
            return false;
        } 
        
        if (_createdTails[0].transform.position.x - _createdTails[1].transform.position.x == 0)
        {
            return false;
        } 

        for (int i = 0; i < _createdTails.Count - 1; i++)
        {
            if ((_createdTails[i + 1].transform.position.x - _createdTails[i].transform.position.x) == -idealValue)
            {
                return false;
            }
        }

        return true;
    }

    private Vector3 GenerateSeedPosition(Vector3 direction)
    {
        var firstPosition = _createdTails[0].transform.position;
        var lastPosition = _createdTails[_createdTails.Count - 1].transform.position;

        return StandartCondition(firstPosition, lastPosition, direction);
    }

    private Vector3 StandartCondition(Vector3 firstPosition, Vector3 lastPosition, Vector3 direction)
    {
        if (direction == Vector3.up || direction == Vector3.down)
        {
            if (firstPosition.x > lastPosition.x)
            {
                if (GameField.Instance[firstPosition + Vector3.left] == GameFieldElement.None)
                {
                    return firstPosition + Vector3.left;
                }

                return lastPosition + Vector3.right;
            }
            else
            {
                if (GameField.Instance[firstPosition + Vector3.right] == GameFieldElement.None)
                {
                    return firstPosition + Vector3.right;
                }

                return lastPosition + Vector3.left;
            }
        }
        if (direction == Vector3.left || direction == Vector3.right)
        {
            if (firstPosition.y > lastPosition.y)
            {
                if (GameField.Instance[firstPosition + Vector3.down] == GameFieldElement.None)
                {
                    return firstPosition + Vector3.down;
                }

                return lastPosition + Vector3.up;
            }
            else
            {
                if (GameField.Instance[firstPosition + Vector3.up] == GameFieldElement.None)
                {
                    return firstPosition + Vector3.up;
                }

                return lastPosition + Vector3.down;
            }
        }

        return Vector3.zero;
    }

    private void FillUnderLine(Vector3 direction)
    {
        Vector3 seedPosition = ChooseUnderLineSeed(_createdTails[0].transform.position, direction);

        FillPoly(seedPosition);
    }

    private Vector3 ChooseUnderLineSeed(Vector3 startPosition, Vector3 direction)
    {
        if (direction.x != 0)
        {
            int distanceToNearestUp = 1;
            int distanceToNearestDown = 1;

            while (true) 
            {
                GameFieldElement upTile = GameField.Instance[startPosition + distanceToNearestUp * Vector3.up];
                GameFieldElement downTile = GameField.Instance[startPosition + distanceToNearestDown * Vector3.down];

                if (upTile != GameFieldElement.Ground)
                {
                    distanceToNearestUp++;
                }
                if (downTile != GameFieldElement.Ground)
                {
                    distanceToNearestDown++;
                }

                if (upTile == GameFieldElement.Ground || downTile == GameFieldElement.Ground)
                {
                    return distanceToNearestUp > distanceToNearestDown ? startPosition + Vector3.down : startPosition + Vector3.up;
                }
            }
        }
        else
        {
            int distanceToNearestLeft = 1;
            int distanceToNearestRight = 1;

            while (true)
            {
                GameFieldElement leftTile = GameField.Instance[startPosition + distanceToNearestLeft * Vector3.left];
                GameFieldElement rightTile = GameField.Instance[startPosition + distanceToNearestRight * Vector3.right];

                if (leftTile != GameFieldElement.Ground)
                {
                    distanceToNearestLeft++;
                }
                if (rightTile != GameFieldElement.Ground)
                {
                    distanceToNearestRight++;
                }

                if (leftTile == GameFieldElement.Ground || rightTile == GameFieldElement.Ground)
                {
                    return distanceToNearestLeft > distanceToNearestRight ? startPosition + Vector3.right : startPosition + Vector3.left;
                }
            }
        }
    }

    private void FillPoly(Vector3 position)
    {
        if (GameField.Instance[position] != GameFieldElement.Ground)
        {
            var created = Instantiate(_tails.GroundTemplate, GameField.Instance.transform);

            created.transform.position = position;
            GameField.Instance[created.transform.position] = GameFieldElement.Ground;

            FillPoly(position + Vector3.up);
            FillPoly(position + Vector3.down);
            FillPoly(position + Vector3.left);
            FillPoly(position + Vector3.right);
        }
    }

    private void DeleteAllTailTiles()
    {
        _createdTails.Clear();

        foreach (Transform element in _tailContainer.transform)
        {
            Destroy(element.gameObject);
        }
    }
}