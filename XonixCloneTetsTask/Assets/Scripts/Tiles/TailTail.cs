using UnityEngine;

public class TailTail : MonoBehaviour
{
    [SerializeField] private Player _player;

    public Player Player => _player;

    public void Init(Player player)
    {
        _player = player;
    }
}
