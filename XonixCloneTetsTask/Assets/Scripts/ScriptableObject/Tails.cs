using UnityEngine;

[CreateAssetMenu(fileName = "New Tiles", menuName = "Tails", order = 51)]
public class Tails : ScriptableObject
{
    [SerializeField] private GameObject _groundTemplate;
    [SerializeField] private GameObject _tailTemplate;
    [SerializeField] private GameObject _seaTemplate;

    public GameObject GroundTemplate => _groundTemplate;
    public GameObject TailTemplate => _tailTemplate;
    public GameObject SeaTemplate => _seaTemplate;
}
