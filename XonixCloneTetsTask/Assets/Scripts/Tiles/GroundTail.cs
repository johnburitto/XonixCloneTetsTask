using UnityEngine;

public class GroundTail : MonoBehaviour
{
    [SerializeField] private GroundMaker _groundMaker;
    private Player _player;

    public void Init(Player player)
    {
        _player = player;
    }

    private void Update()
    {
        if (_player.transform.position == this.transform.position)
        {
            Debug.Log($"Player => {_player.transform.position}; tail => {this.transform.position}");
        }
    }
}
