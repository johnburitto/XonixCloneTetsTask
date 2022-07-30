using UnityEngine;

[CreateAssetMenu(fileName = "New LevelConfig", menuName = "LevelConfig", order = 52)]
public class LevelConfig : ScriptableObject
{
    [SerializeField] private GameObject _seaEnemyTemplate;
    [SerializeField] private GameObject _groundEnemyTemplate;
    [SerializeField] private int _numberOfEnemies;

    public GameObject SeaEnemyTamplate => _seaEnemyTemplate;
    public GameObject GroundEnemyTemplate => _groundEnemyTemplate;
    public int NumberOfEnemies => _numberOfEnemies;

    public void ResetLevelConfig()
    {
        _numberOfEnemies = 1;
    }

    public void Nextlevel()
    {
        _numberOfEnemies += 1;
    }
}
