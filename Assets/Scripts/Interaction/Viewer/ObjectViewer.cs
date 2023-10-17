using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectViewer : MonoBehaviour
{
    public float mouseSensitivity = 100f;

    private Transform target;
    private float xRotation = 0f;

    private void Update()
    {
        if(target == null) // Puase check
            return;

        ObjectSO item = target.GetComponent<ObjectInteractable>().obj;
        if(item.canRotateAlongX)
            RotateAlongX();
        if(item.canRotateAlongY)
            RotateAlongY();
    }

    private void RotateAlongY()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;  
        target.Rotate(Vector3.up * mouseX);
    }

    private void RotateAlongX()
    {  
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // -= to invert the rotation
        xRotation -= mouseY;
        // Clamping to limit the rotation angle
        // xRotation = Mathf.Clamp(xRotation, -80f, 80f);
        target.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }

    public void ResetTargetProperties()
    {
        xRotation = 0f;
    }

    public void SetTarget(Transform _target)
    {
        target = _target;
    }
}
