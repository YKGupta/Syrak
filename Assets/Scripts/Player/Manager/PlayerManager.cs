using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;
    [HideInInspector]
    public bool isPlayerAllowedToMove = true;
    [HideInInspector]
    public bool isPlayerAllowedToLook = true;

    private void Awake()
    {
        instance = this;
    }
}
