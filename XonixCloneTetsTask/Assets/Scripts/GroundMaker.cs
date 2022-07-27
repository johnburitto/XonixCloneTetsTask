using System.Collections.Generic;
using UnityEngine;

public class GroundMaker : MonoBehaviour
{
    [SerializeField] private float _startX;
    [SerializeField] private float _endX;
    [SerializeField] private float _startY;
    [SerializeField] private float _endY;
    [SerializeField] private Tails _tails; 
    [SerializeField] private GameObject _tailContainer;
    [SerializeField] private GameObject _groundContainer;
    [SerializeField] private Player _player;

    private List<GameObject> createdTails = new List<GameObject>();
    private List<Vector3> _vertexOfNewArea;
    private PlayerMover _playerMover;

    private void Awake()
    {
        _playerMover = _player.gameObject.GetComponent<PlayerMover>();
        _vertexOfNewArea = new List<Vector3>() { };
    }

    private void OnEnable()
    {
        _playerMover.ChangePosition += OnChangePosition;
        _playerMover.ChangeDirection += OnAddVertex;
    }

    private void OnDisable()
    {
        _playerMover.ChangePosition -= OnChangePosition;
        _playerMover.ChangeDirection -= OnAddVertex;
    }

    private void Start()
    {
        CreateGamePlace();
    }

    private void Update()
    {
        
    }

    public void CreateGamePlace()
    {
        GameObject created = new GameObject();

        for (float i = _startX; i <= _endX; i++)
        { 
            for (float j = _startY; j >= _endY; j--)
            {
                if ((i == _startX || i == _endX) || (j == _startY || j == _endY))
                {
                    created = Instantiate(_tails.GroundTemplate, _groundContainer.transform);
                    created.gameObject.GetComponent<GroundTail>().Init(_player);
                }
                else
                {
                    created = Instantiate(_tails.SeaTemplate, _groundContainer.transform);
                }
                
                created.transform.position = new Vector2(i, j);
            }
        }
    }

    public void OnChangePosition(Vector3 position)
    {
        var created = Instantiate(_tails.TailTemplate, _tailContainer.transform);
        created.transform.position = position;
        createdTails.Add(created);
    }

    public void OnAddVertex(Vector3 position)
    {
        _vertexOfNewArea.Add(position);
    }
}
