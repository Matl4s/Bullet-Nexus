using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public Camera cam;

    public float xSensitivity = 30f;
    public float ySensitivity = 30f;
    
    private float xRotation = 0f;


    public void ProcessLook(Vector2 input)
    {
        float mouseX = input.x;
        float mouseY = input.y;

        // počítaní rotace kamery
        xRotation -= (mouseY * Time.deltaTime) * ySensitivity;

        // omezuje hodnotu mezi minimální a maximální
        xRotation = Mathf.Clamp(xRotation, -80, 80f);

        // teď tohle uplatníme ke camera.transform
        cam.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);

        // rotace hráče do leva a do prava
        transform.Rotate(Vector3.up * (mouseX * Time.deltaTime) * xSensitivity);
    }

}
