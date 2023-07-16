using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Transform handTransform;
    public float moveSpeed = 5f;
    public float rotationSpeed = 180f;
    public float lookSpeed = 2f;
    public float gravity = 9.8f;
    public bool isGrounded = true;


    private CharacterController controller;
    private float pitch = 0f;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void FixedUpdate()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        float mouseXInput = Input.GetAxis("Mouse X");
        float mouseYInput = Input.GetAxis("Mouse Y");

        Vector3 moveDirection = new Vector3(horizontalInput, 0f, verticalInput);
        moveDirection = handTransform.TransformDirection(moveDirection);
        moveDirection.y = 0f;
        moveDirection.Normalize();

        controller.Move(moveDirection * moveSpeed * Time.deltaTime);

       
        // Karakterin dönmesini sağla
        transform.Rotate(Vector3.up, mouseXInput * rotationSpeed * Time.deltaTime);

        
    }

    void Update()
    {
        float mouseXInput = Input.GetAxis("Mouse X");
        float mouseYInput = Input.GetAxis("Mouse Y");
        // Kameranın yukarı-aşağı bakış açısını kontrol et
        pitch -= mouseYInput * lookSpeed;
        pitch = Mathf.Clamp(pitch, -90f, 90f);
        Camera.main.transform.localRotation = Quaternion.Euler(pitch, 0f, 0f);

         if (!isGrounded || !controller.isGrounded)
        {
            moveDirection.y -= gravity * Time.fixedDeltaTime;
        }

    }
    
    private void OnCollisionStay(Collision other) {
        if (other.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
        else
            isGrounded = false;
    }

    private void OnCollisionExit(Collision other) {
        if (other.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
        else
            isGrounded = false;
    }
}
