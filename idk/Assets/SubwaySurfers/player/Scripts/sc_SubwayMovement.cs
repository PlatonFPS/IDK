using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sc_SubwayMovement : MonoBehaviour
{
    [SerializeField] List<Transform> Lanes = new List<Transform>();
    [SerializeField] int currentLane = 0;

    private CharacterController characterController;
    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        ChangeToLanePosition(currentLane);
    }

    [SerializeField] float minAxisDeviation = 0.1f;
    [SerializeField] float forwardSpeed = 5f;
    private void Movement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        if (Mathf.Abs(horizontalInput) > minAxisDeviation && canSwitchLanes)
        {
            characterController.enabled = false;
            StartCoroutine(SwitchLanes(horizontalInput > minAxisDeviation ? 1 : -1));
            characterController.enabled = true;
        }
        characterController.Move(transform.forward * forwardSpeed * Time.deltaTime);

        Debug.Log(characterController.isGrounded);
        if (Input.GetKey(KeyCode.Space) && !jumping && characterController.isGrounded)
        {
            Debug.Log("Jumping");
            jumping = true;
            jumpTimer = jumpDuration;
        }
    }

    [SerializeField] float jumpPower = 25f;
    [SerializeField] float jumpDuration = 1.0f;
    private bool jumping = false;
    private float jumpTimer = 0f;
    void Jump()
    {
        if((jumping && characterController.isGrounded) || (jumpTimer <= 0))
        {
            jumpTimer = 0f;
            jumping = false;
            return;
        }
        Debug.Log("Jumping | Timer: " + jumpTimer);
        jumpTimer -= Time.deltaTime;
        characterController.Move(transform.up * jumpPower * Time.deltaTime);
    }

    void ChangeToLanePosition(int lane)
    {
        Vector3 position = Lanes[lane].position;
        transform.position = new Vector3(position.x, transform.position.y, transform.position.z);
    }

    private bool canSwitchLanes = true;
    IEnumerator SwitchLanes(int direction)
    {
        canSwitchLanes = false;
        //Debug.Log("Switching Lanes | Direction: " + (direction > 0 ? "Right" : "Left"));
        int nextLane = currentLane + direction;
        if(nextLane >= 0 && nextLane < Lanes.Count)
        {
            ChangeToLanePosition(nextLane);
            currentLane = nextLane;
        }
        else
        {
            Debug.Log("Cannot switch lanes");
        }
        yield return new WaitForSeconds(0.5f);
        canSwitchLanes = true;
    }

    [SerializeField] float gravity = 9.81f;
    private void Gravity()
    {
        if (!characterController.isGrounded)
        {
            characterController.Move(new Vector3(0, -gravity, 0) * Time.deltaTime);
        }
    }

    void Update()
    {
        Jump();
        Movement();
        Gravity();
    }
}
