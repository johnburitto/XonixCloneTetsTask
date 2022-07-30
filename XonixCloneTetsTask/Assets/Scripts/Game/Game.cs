using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private GroundMaker _groundMaker;
    [SerializeField] private EnemySpawner _enemySpawner;
    [SerializeField] private Player _player;
    [SerializeField] private LevelConfig _levelConfig;

    private float _timer;

    public float Timer => _timer;

    private void OnEnable()
    {
        _player.GetDamage += OnGetDamage;
        _player.Dead += GameOver;
        _groundMaker.CapturingTerritory += OnIsLevelCompleted;
    }

    private void OnDisable()
    {
        _player.GetDamage -= OnGetDamage;
        _player.Dead -= GameOver;
        _groundMaker.CapturingTerritory -= OnIsLevelCompleted;
    }

    private void Start()
    {
        _timer = 60;
        _levelConfig.ResetLevelConfig();
        _groundMaker.CreateGamePlace();
        _enemySpawner.SpawnEnemies(_levelConfig);
    }

    private void Update()
    {
        _timer -= Time.deltaTime;

        if (_timer <= 0)
        {
            GameOver();
        }
    }

    private void OnGetDamage()
    {
        _player.ResetPlayer(Player.Instance.Health);
    } 

    private void GameOver()
    {
        _timer = 60;
        _groundMaker.ResetGamePlace();
        _enemySpawner.ResetEnemies(_levelConfig);
        _player.ResetPlayer(3);
    }

    private void OnIsLevelCompleted()
    {
        if (((float)GameField.Instance.transform.childCount / GameField.Instance.Square > 0.8f) ||
            _enemySpawner.transform.childCount == _enemySpawner.NumberOfGroundEnemies)
        {
            _timer = 60;
            _levelConfig.Nextlevel();
            _groundMaker.ResetGamePlace();
            _enemySpawner.ResetEnemies(_levelConfig);
            _player.ResetPlayer(3);
        }
    }
}
