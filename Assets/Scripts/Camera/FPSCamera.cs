using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSCamera : MonoBehaviour
{
    public float sens;

    public Transform orientation;

    float rotationX;
    float rotationY;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sens;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sens;


        rotationY += mouseX;
        rotationX -= mouseY;

        rotationX = Mathf.Clamp(rotationX, -15f, 15f);

        transform.rotation = Quaternion.Euler(rotationX, rotationY, 0);
        orientation.rotation = Quaternion.Euler(rotationX, rotationY, 0);
    }
}
