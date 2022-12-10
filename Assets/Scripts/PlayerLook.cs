using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    [Header("References")]
    [SerializeField] WallRun wallRun;

    [SerializeField] private float sensX = 100f;
    [SerializeField] private float sensY = 100f;

    [SerializeField] Transform cam = null;
    [SerializeField] Transform orientation = null;
    [SerializeField] Transform playerTransform = null;

    float mouseX;
    float mouseY;

    float multiplier = 0.01f;

    float xRotation = 0f;
    float yRotation;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        yRotation = playerTransform.eulerAngles.y; // GameObject.Find("Player&Camera").transform.rotation.y;

    }

    private void Update()
    {
        if(!PauseMenu.GameIsPaused && !GameOver.GameIsOver & !NivelCompleto.GameIsOver) {
            mouseX = Input.GetAxisRaw("Mouse X");
            mouseY = Input.GetAxisRaw("Mouse Y");
            
            yRotation += mouseX * sensX * multiplier;
            xRotation -= mouseY * sensY * multiplier;

            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            cam.transform.rotation = Quaternion.Euler(xRotation, yRotation, wallRun.tilt);
            orientation.transform.rotation = Quaternion.Euler(0, yRotation, 0);
            }
    }
}
