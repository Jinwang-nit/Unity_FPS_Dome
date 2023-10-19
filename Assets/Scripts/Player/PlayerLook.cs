using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    public Camera cam;
    private float xRotation = 0f;

    public float xSensitivity = 25f;
    public float ySensitivity = 25f;

    public void ProcessLook(Vector2 input)
    {
        float mouseX = input.x;
        float mouseY = input.y;
        // ��ͷ����
        xRotation -= (mouseY * Time.deltaTime) * ySensitivity;
        xRotation  = Mathf.Clamp(xRotation, -80f, 80f);
        cam.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);

        // ���ҿ�
        transform.Rotate(Vector3.up * (mouseX * Time.deltaTime) * xSensitivity);
    }

}
