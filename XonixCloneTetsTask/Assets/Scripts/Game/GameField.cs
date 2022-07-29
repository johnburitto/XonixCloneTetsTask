using UnityEngine;

public class GameField : MonoBehaviour
{
    [SerializeField] private int _width;
    [SerializeField] private int _height;

    private GameObject[,] _field;

    public static GameField Instance { get; private set; }
    public int Width => _width;
    public int Height => _height;
    public float Square { get; private set; }
    public GameObject this[Vector3 position]
    {
        get { return _field[(int)position.x, (int)position.y]; }
        set { _field[(int)position.x, (int)position.y] = value; }
    }
    public GameObject this[int x, int y]
    {
        get { return _field[x, y]; }
        set { _field[x, y] = value; }
    }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _field = new GameObject[_width, _height];
        Square = _width * _height;
    }
}
