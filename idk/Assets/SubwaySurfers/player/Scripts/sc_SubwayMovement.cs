using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sc_SubwayMovement : MonoBehaviour
{
    [SerializeField] List<Transform> Lanes;
    [SerializeField] int currentLane;
    private Rigidbody rigidbody;
    private CapsuleCollider collider;
    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        collider = GetComponent<CapsuleCollider>();
        ChangeToLanePosition(currentLane);
    }

    [SerializeField] float minAxisDeviation;
    [SerializeField] float forwardSpeed;
    [SerializeField] float jumpPower;
    [SerializeField] float axisDeviation;
    private float jumpCoeffient = 10f;
    private void Movement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        if (Mathf.Abs(horizontalInput) > minAxisDeviation && canSwitchLanes)
        {
            StartCoroutine(SwitchLanes(horizontalInput > minAxisDeviation ? 1 : -1));
        }
        rigidbody.position += transform.forward * forwardSpeed * Time.deltaTime;

        if ((Input.GetKey(KeyCode.Space) || Input.GetAxis("Vertical") > axisDeviation) && isGrounded && !crouching)
        {
            rigidbody.AddForce(transform.up * jumpPower * jumpCoeffient, ForceMode.Acceleration);
            isGrounded = false;
        }

        if ((Input.GetKey(KeyCode.LeftControl) || Input.GetAxis("Vertical") < -axisDeviation) && isGrounded && !crouching)
        {
            Crouch();
        }
    }

    private float laneTargetPosition;
    void ChangeToLanePosition(int lane)
    {
        laneTargetPosition = Lanes[lane].position.x;
    }

    private float interpolation = 0.05f;
    void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, new Vector3(laneTargetPosition, transform.position.y, transform.position.z), interpolation);
    }

    [SerializeField] float laneSwitchDelay;
    private bool canSwitchLanes = true;
    IEnumerator SwitchLanes(int direction)
    {
        canSwitchLanes = false;
        int nextLane = currentLane + direction;
        if(nextLane >= 0 && nextLane < Lanes.Count)
        {
            ChangeToLanePosition(nextLane);
            currentLane = nextLane;
            yield return new WaitForSeconds(laneSwitchDelay);
        }       
        canSwitchLanes = true;
    }

    private bool crouching = false;
    private void Crouch()
    {
        crouching = true;
        crouchTimer = CrouchTime;
        collider.height = 1f;
        collider.center = new Vector3(0, -0.5f, 0);
        SetAnimation("test", false);
    }
    private float crouchTimer = 0;
    [SerializeField] float CrouchTime;
    private void Crouching()
    {
        if (crouching)
        {
            if (crouchTimer <= 0)
            {
                UnCrouch();
                crouchTimer = 0;
                return;
            }
            crouchTimer -= Time.deltaTime;
        }
    }
    private void UnCrouch()
    {
        crouching = false;
        collider.height = 2f;
        collider.center = new Vector3(0, 0, 0);
        SetAnimation("test", true);
    }

    [SerializeField] Transform Animator;
    private void SetAnimation(string name, bool value)
    {
        Animator.gameObject.SetActive(value);
    }

    void Update()
    {
        Crouching();
        Movement();
        Gravity();
    }

    [SerializeField] float gravity;
    [SerializeField] float fallingGravity;
    private bool isGrounded = false;
    private void Gravity()
    {
        if(rigidbody.velocity.y < 0)
        {
            rigidbody.AddForce(Physics.gravity * fallingGravity * Time.deltaTime, ForceMode.Acceleration);
        } 
        else
        {
            rigidbody.AddForce(Physics.gravity * gravity * Time.deltaTime, ForceMode.Acceleration);
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}
