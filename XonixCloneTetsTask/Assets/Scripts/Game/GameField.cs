using UnityEngine;

public class GameField : MonoBehaviour
{
    [SerializeField] private int _width;
    [SerializeField] private int _height;

    private GameFieldElement[,] _field;

    public static GameField Instance { get; private set; }
    public int Width => _width;
    public int Height => _height;
    public float Square { get; private set; }
    public GameFieldElement this[Vector3 position]
    {
        get { return _field[(int)position.x, (int)position.y]; }
        set { _field[(int)position.x, (int)position.y] = value; }
    }
    public GameFieldElement this[int x, int y]
    {
        get { return _field[x, y]; }
        set { _field[x, y] = value; }
    }

    private void Awake()
    {
        Instance = this;
        _field = new GameFieldElement[_width, _height];
    }

    private void Start()
    {
        Square = _width * _height;
    }

    public void ResetGameField()
    {
        for (int i = 0; i < _width; i++)
        {
            for (int j = 0; j < _height; j++)
            {
                _field[i, j] = GameFieldElement.None;
            }
        } 

        foreach (Transform elemnt in transform)
        {
            Destroy(elemnt.gameObject);
        }
    }
}

public enum GameFieldElement
{
    None,
    Ground,
    Tail,
    Enemy
}
