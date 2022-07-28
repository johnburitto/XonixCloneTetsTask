using System.Collections.Generic;
using UnityEngine;

public class GroundMaker : MonoBehaviour
{
    [SerializeField] private Tails _tails; 
    [SerializeField] private GameObject _tailContainer;
    [SerializeField] private PlayerMover _playerMover;

    private List<GameObject> _createdTails = new List<GameObject>();
    private List<Vector3> _vertexOfArea = new List<Vector3>();

    private void OnEnable()
    {
        _playerMover.ChangePosition += OnChangePosition;
        _playerMover.ChangeDirection += OnChangeDirection;
        _playerMover.Grounded += CreateSaveArea;
    }

    private void OnDisable()
    {
        _playerMover.ChangePosition -= OnChangePosition;
        _playerMover.ChangeDirection -= OnChangeDirection;
        _playerMover.Grounded -= CreateSaveArea;
    }

    private void Start()
    {
        CreateGamePlace();
    }

    public void CreateGamePlace()
    {
        for (int i = 0; i <= GameField.Instance.Width; i++)
        {
            for (int j = 0; j <= GameField.Instance.Height; j++)
            {
                if ((i == 0 || i == GameField.Instance.Width) ||
                    (j == 0 || j == GameField.Instance.Height))
                {
                    var created = Instantiate(_tails.GroundTemplate, GameField.Instance.transform);

                    created.transform.position = new Vector2(i, j);
                    GameField.Instance[i, j] = created;
                }
            }
        }
    }

    public void OnChangePosition(Vector3 position)
    {
        var created = Instantiate(_tails.TailTemplate, _tailContainer.transform);
        
        created.transform.position = position;
        _createdTails.Add(created);
    }

    private void OnChangeDirection(Vector3 position)
    {
        _vertexOfArea.Add(position);
    }

    public void CreateSaveArea(Vector3 direction)
    {
        int firstElement = ReturnFirst(_createdTails, _vertexOfArea);
        int lastElement = RetunLast(_createdTails, _vertexOfArea);

        if (firstElement == 0 && lastElement == 0)
        {
            CreateSaveArea(_createdTails, direction);
        }
        else if (firstElement == lastElement && firstElement != 0 && lastElement != 0)
        {
            CreateSaveArea(_createdTails, firstElement, direction);
        }
        else
        {
            CreateSaveArea(_createdTails, firstElement, lastElement, direction);
        }

        DeleteAllTailTails();
    }

    private int ReturnFirst(List<GameObject> createdTails, List<Vector3> vertex)
    {

        if (vertex.Count < 1)
        {
            return 0;
        }

        Vector3 target = vertex[0];


        for (int i = 0; i < createdTails.Count; i++)
        {
            if (createdTails[i].transform.position == target)
            {
                return i;
            }
        }

        return 0;
    }
    
    private int RetunLast(List<GameObject> createdTails, List<Vector3> vertex)
    {

        if (vertex.Count < 1)
        {
            return 0;
        }

        Vector3 target = vertex[vertex.Count - 1];


        for (int i = 0; i < createdTails.Count; i++)
        {
            if (createdTails[i].transform.position == target)
            {
                return i;
            }
        }

        return 0;
    }

    private void CreateSaveArea(List<GameObject> createdTails, Vector3 direction)
    {
        Vector3 newDirection = Vector3.zero;

        if (direction.x != 0)
        {
            newDirection = new Vector2(0, -1);
        }
        else if (direction.y != 0)
        {
            newDirection = new Vector2(-1, 0);
        }

        for (int i = 0; i < createdTails.Count; i++)
        {
            var length = LenghtOfLine(createdTails[i].transform.position, newDirection);

            for (int j = 0; j < length; j++)
            {
                var created = Instantiate(_tails.GroundTemplate, GameField.Instance.transform);

                created.transform.position = createdTails[i].transform.position + (j * newDirection);
                GameField.Instance[created.transform.position] = created;
            }
        }
    }

    private void CreateSaveArea(List<GameObject> createdTails, int startElement, Vector3 direction)
    {
        for(int i = 0; i < startElement; i++)
            {
            var lenght = LenghtOfLine(createdTails[i].transform.position, direction);

            for (int j = 0; j < lenght; j++)
            {
                var created = Instantiate(_tails.GroundTemplate, GameField.Instance.transform);

                created.transform.position = createdTails[i].transform.position + (j * direction);
                GameField.Instance[created.transform.position] = created;
            }
        }
    }

    private void CreateSaveArea(List<GameObject> createdTails, int startElement, int endElement, Vector3 direction)
    {
        for (int i = startElement; i < endElement; i++)
        {
            var lenght = LenghtOfLine(createdTails[i].transform.position, direction);

            for (int j = 0; j < lenght; j++)
            {
                var created = Instantiate(_tails.GroundTemplate, GameField.Instance.transform);

                created.transform.position = createdTails[i].transform.position + (j * direction);
                GameField.Instance[created.transform.position] = created;
            }
        }
    }

    private int LenghtOfLine(Vector3 current, Vector3 direction)
    {
        int lenght = 0;

        var tileToCheck = GameField.Instance[current];

        while (!tileToCheck)
        {
            lenght++;
            current += direction;
            tileToCheck = GameField.Instance[current];
        }

        return lenght;
    }

    private void DeleteAllTailTails()
    {
        _createdTails.Clear();
        _vertexOfArea.Clear();

        foreach (Transform element in _tailContainer.transform)
        {
            Destroy(element.gameObject);
        }
    }
}
