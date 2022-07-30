using UnityEngine;

[CreateAssetMenu(fileName = "New Tiles", menuName = "Tiles", order = 51)]
public class Tiles : ScriptableObject
{
    [SerializeField] private GameObject _groundTemplate;
    [SerializeField] private GameObject _tailTemplate;

    public GameObject GroundTemplate => _groundTemplate;
    public GameObject TailTemplate => _tailTemplate;
}
