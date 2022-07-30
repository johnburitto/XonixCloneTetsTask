using UnityEngine;
using UnityEngine.UI;

public class PauseButton : MonoBehaviour
{
    [SerializeField] private Text _buttonText;
    private bool _isClicked = false;

    public void PauseGame()
    {
        if (!_isClicked)
        {
            _isClicked = true;
            _buttonText.text = "Resume";
            Time.timeScale = 0;
        }
        else
        {
            _isClicked = false;
            _buttonText.text = "Pause";
            Time.timeScale = 1;
        }
    }
} 
