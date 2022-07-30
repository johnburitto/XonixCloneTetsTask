using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] private Game _game;
    [SerializeField] private Text _timer;

    private void Update()
    {
        _timer.text = "Left time: " + Mathf.CeilToInt(_game.Timer).ToString();
    }
}
