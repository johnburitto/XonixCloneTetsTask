using UnityEngine;
using UnityEngine.UI;

public class PauseButton : MonoBehaviour
{
    [SerializeField] private Text _buttonText;

    public void PauseGame()
    {
        if (!Game.Instance.IsPaused)
        {
            Game.Instance.IsPaused = true;
            _buttonText.text = "Resume";
            Time.timeScale = 0;
        }
        else
        {
            Game.Instance.IsPaused = false;
            _buttonText.text = "Pause";
            Time.timeScale = 1;
        }
    }
} 
