using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;
    public bool isPlayerAllowedToMove = true;
    public bool isPlayerAllowedToLook = true;

    private void Awake()
    {
        instance = this;
    }
}
