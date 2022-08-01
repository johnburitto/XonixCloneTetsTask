using UnityEngine;
using UnityEngine.UI;

public class HeathBar : MonoBehaviour
{
    [SerializeField] Player _player;
    [SerializeField] Text _healthBar;

    private void OnEnable()
    {
        _player.HealthChange += OnHealthChange;
    }

    private void OnDisable()
    {
        _player.HealthChange -= OnHealthChange;
    }

    private void OnHealthChange()
    {
        _healthBar.text = "Health " + _player.Health.ToString();
    }
}
