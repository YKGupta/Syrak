using UnityEngine;

public class Billboard : MonoBehaviour
{
    public GameObject interactionWindowPanel;

    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void LateUpdate()
    {
        interactionWindowPanel.transform.LookAt(mainCamera.transform);
        interactionWindowPanel.transform.Rotate(0, 180, 0);
    }
}
