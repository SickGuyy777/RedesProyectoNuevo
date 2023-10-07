using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookCam : MonoBehaviour
{
    public float sensitivity = 2.0f; // Sensibilidad del ratón/cámara
    public Transform playerBody; // Objeto que representa el cuerpo del jugador
    float verticalRotation = 0.0f;
    public Transform playerHead;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    private void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity;

        // Rotar la cámara verticalmente
        verticalRotation -= mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, -90f, 90f);
        transform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);

        playerBody.Rotate(Vector3.up * mouseX);


        playerHead.Rotate(Vector3.right * mouseY);
    }
}
