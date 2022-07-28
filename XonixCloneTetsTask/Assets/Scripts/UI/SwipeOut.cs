using UnityEngine;
using UnityEngine.UI;

public class SwipeOut : MonoBehaviour
{
    [SerializeField] private Text _text;

    private void Update()
    {
        //_text.text = SwipeDetector.Distance.ToString();
        if (SwipeDetector.SwipeUp())
        {
            _text.text = "Up";
        }

        if (SwipeDetector.SwipeDown())
        {
            _text.text = "Down";
        }

        if (SwipeDetector.SwipeLeft())
        {
            _text.text = "Left";
        }

        if (SwipeDetector.SwipeRight())
        {
            _text.text = "Right";
        }
    }
}
