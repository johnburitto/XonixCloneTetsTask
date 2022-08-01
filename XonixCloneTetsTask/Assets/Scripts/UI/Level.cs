using UnityEngine;
using UnityEngine.UI;

public class Level : MonoBehaviour
{
    [SerializeField] private LevelConfig _levelConfig;
    [SerializeField] private Text _level;

    private void OnEnable()
    {
        _levelConfig.NextLevel += OnNextLevel;
    }

    private void OnDisable()
    {
        _levelConfig.NextLevel -= OnNextLevel;
    }

    private void OnNextLevel()
    {
        _level.text = "Lvl " + _levelConfig.NumberOfEnemies;
    }
}