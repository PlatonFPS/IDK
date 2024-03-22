using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sc_SubwayMovement : MonoBehaviour
{
    [SerializeField] List<Transform> Lanes;
    [SerializeField] int currentLane;
    private Rigidbody rigidbody;
    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        ChangeToLanePosition(currentLane);
    }

    [SerializeField] float minAxisDeviation;
    [SerializeField] float forwardSpeed;
    [SerializeField] float jumpPower;
    private float jumpCoeffient = 10f;
    private void Movement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        if (Mathf.Abs(horizontalInput) > minAxisDeviation && canSwitchLanes)
        {
            StartCoroutine(SwitchLanes(horizontalInput > minAxisDeviation ? 1 : -1));
        }
        rigidbody.position += transform.forward * forwardSpeed * Time.deltaTime;

        if (Input.GetKey(KeyCode.Space) && isGrounded)
        {
            rigidbody.AddForce(transform.up * jumpPower * jumpCoeffient, ForceMode.Acceleration);
            isGrounded = false;
        }
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
    void Update()
    {
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
        Debug.Log("Entered");
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        Debug.Log("Exited");
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}
