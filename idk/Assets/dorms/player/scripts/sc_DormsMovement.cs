using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class sc_DormsMovement : MonoBehaviour
{
    [SerializeField] Transform model;

    private CharacterController characterController;
    private Animator animator;
    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    private void SetAnimation(string animationName, bool value)
    {
        animator.SetBool(animationName, value);
    }

    [SerializeField] float movementSpeed;

    private void RotatePlayer(float horizontalInput, float verticalInput)
    {
        if(horizontalInput != 0 || verticalInput != 0)
        {
            model.rotation = Quaternion.LookRotation(new Vector3(-horizontalInput, 0, -verticalInput));
        }
    }
    private void Movement()
    {
        if(Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            float speed = movementSpeed * Time.deltaTime;
            characterController.Move(new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) * speed);
            SetAnimation("Walk", true);
            RotatePlayer(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        }
        else 
        { 
            SetAnimation("Walk", false); 
        }
    }
    // Update is called once per frame
    void Update()
    {
        Movement();
    }
}
