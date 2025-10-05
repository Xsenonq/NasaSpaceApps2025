using TMPro;
using UnityEngine;

public class SpaceUnit : MonoBehaviour
{
    public Vector3 Pos => transform.position;
    [field: SerializeField] public AudioClip AudioClip { get; private set; }
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField][field:TextArea(10, 30)] public string Description { get; private set; }
}
