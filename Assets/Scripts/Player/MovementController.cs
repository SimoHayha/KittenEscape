using UnityEngine;
using System.Collections;

public class MovementController : MonoBehaviour {

    public float gravity = 20.0f;
    public float speed = 6.0f;
    public float jumpSpeed = 8.0f;

    private Vector3 moveDirection = Vector3.zero;
    private Vector3 previousSpeed = Vector3.zero;
    private Vector3 newSpeed = Vector3.zero;

    private Animation animation;
    private CharacterController controller;

    void Start()
    {
        animation = GetComponentInChildren<Animation>();
        controller = GetComponent<CharacterController>();
    }

    void CalculateMovement()
    {
        //newSpeed = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
        //newSpeed = transform.TransformDirection(newSpeed);
        //newSpeed *= speed;

        //if (!controller.isGrounded)
        //{
        //    previousSpeed.x *= 0.1f;
        //    previousSpeed.z *= 0.1f;
        //    previousSpeed.y -= gravity * Time.deltaTime;
        //}
        //else
        //{
        //    if (Input.GetButtonDown("Jump"))
        //        newSpeed.y = jumpSpeed;
        //    previousSpeed.x = newSpeed.x;
        //    previousSpeed.z = newSpeed.z;
        //}

        //moveDirection = newSpeed + previousSpeed;
        //previousSpeed = moveDirection;
        //controller.Move(moveDirection * Time.deltaTime);


    }

    void Update()
    {

        CalculateMovement();

        if (animation)
        {
            if (moveDirection.x != 0.0f || moveDirection.z != 0.0f)
                animation.Play("chaplinwalk");
            else
            {
                animation.Play("idle");
            }
        }
    }

}
