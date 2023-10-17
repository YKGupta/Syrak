using UnityEngine;
using NaughtyAttributes;

[CreateAssetMenu(menuName = "Personal/Object", fileName = "New Interactable Object")]
public class ObjectSO : ScriptableObject
{
    [BoxGroup("Rotation")]
    public bool canRotateAlongY;
    [BoxGroup("Rotation")]
    public bool canRotateAlongX;
}
