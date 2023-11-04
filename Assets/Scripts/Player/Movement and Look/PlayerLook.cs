using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    public Transform playerBody;
    public PauseManager pauseManager;
    public float defaultMouseSensitivity = 100f;

    private float xRotation = 0f;
    private float mouseSensitivity;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        mouseSensitivity = PlayerPrefs.GetFloat("Sensitivity", defaultMouseSensitivity);
        pauseManager.sensitivityChanged += OnSensitivityChanged;
    }

    private void Update()
    {
        if(!PlayerManager.instance.isPlayerAllowedToLook)
            return;

        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;    
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // -= to invert the rotation
        xRotation -= mouseY;
        // Clamping to limit the rotation angle
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        playerBody.Rotate(Vector3.up * mouseX);
    }

    public void OnSensitivityChanged()
    {
        mouseSensitivity = PlayerPrefs.GetFloat("Sensitivity", defaultMouseSensitivity);
    }
}
