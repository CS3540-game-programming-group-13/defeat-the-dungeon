using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float moveSpeed = 10;
    public float jumpHeight = 10;
    public float gravity = 9.81f;
    public float airControl = 5;
    private float pitch = 0;
    private Vector3 input, moveDirection;
    private CharacterController controller;
    private Animator animator;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if (!LevelManager.instance.IsGameOver())
        {
            UpdatePosition();
        }
    }

    private void UpdatePosition()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        input = transform.right * moveHorizontal + transform.forward * moveVertical;
        input *= moveSpeed;
        if (controller.isGrounded)
        {
            moveDirection = input;
            // we can jump
            if (Input.GetButton("Jump"))
            {
                moveDirection.y = Mathf.Sqrt(2 * jumpHeight * gravity);
            }
            else
            {
                moveDirection.y = 0.0f;
            }
        }
        else
        {
            // in the air
            input.y = moveDirection.y;
            moveDirection = Vector3.Lerp(moveDirection, input, airControl * Time.deltaTime);
        }
        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);
        animator.SetFloat("moveSpeed", moveVertical);
    }

}
