using UnityEngine;
using UnityEngine.UI;

public class CapturedTerritory : MonoBehaviour
{
    [SerializeField] private GroundMaker _groundMaker;
    [SerializeField] private Text _capturedTerritory;

    private void OnEnable()
    {
        _groundMaker.CapturingTerritory += OnCapturingTerritory;
    }

    private void OnDisable()
    {
        _groundMaker.CapturingTerritory -= OnCapturingTerritory;
    }

    private void OnCapturingTerritory()
    {
        _capturedTerritory.text = "Territory " + 
            Mathf.Round((float)GameField.Instance.transform.childCount / GameField.Instance.Square * 100).ToString() + " - 80";
    }
}
