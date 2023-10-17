using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;
    [HideInInspector]
    public bool isPlayerAllowedToMove = true;
    [HideInInspector]
    public bool isPlayerAllowedToLook = true;
    [HideInInspector]
    public bool isPlayerAllowedToOpenInventory = true;
    [HideInInspector]
    public bool isPlayerAllowedToInteractWithObjects = true;

    private void Awake()
    {
        instance = this;
    }

    public void SetPlayerState(bool state)
    {
        isPlayerAllowedToMove = state;
        isPlayerAllowedToLook = state;
        isPlayerAllowedToOpenInventory = state;
        isPlayerAllowedToInteractWithObjects = state;
    }
}
