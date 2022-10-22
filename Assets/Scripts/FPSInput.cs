using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(CharacterController))] // obliga a que el GameObject tenga cierto componente
public class FPSInput : MonoBehaviour
{
    public float speed = 6.0f;
    public float gravity = -9.8f;
    public float jumpForce;
    private float ySpeed;
    private CharacterController _charController; 
    bool jumped = false;

    void Start()
    {
        _charController = GetComponent<CharacterController>();
    }
    void Update()
    {
        float deltaX = Input.GetAxis("Horizontal") * speed; // Las teclas asociadas est�n en:
        float deltaZ = Input.GetAxis("Vertical") * speed; // Edit\Project Settings\Input
        Vector3 movement = new Vector3(deltaX, 0, deltaZ);
        movement = Vector3.ClampMagnitude(movement, speed);
        movement = transform.TransformDirection(movement); // convierte desde el sistema local al global

        if(_charController.isGrounded)
        {
            if(Input.GetButtonDown("Jump")) 
            {
                jumped = true;
            }
        } else { ySpeed += gravity * Time.deltaTime; }
        
        movement.y = ySpeed;
        _charController.Move(movement * Time.deltaTime); // no movemos el transform para que se calculen
    }

    private void FixedUpdate()
    {
        if(jumped)
        {
            jumped = false;
            ySpeed = gravity * Time.deltaTime;
            ySpeed = jumpForce;
        }
    }
}

 