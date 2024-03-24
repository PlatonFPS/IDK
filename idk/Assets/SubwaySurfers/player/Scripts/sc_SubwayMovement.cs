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
        SetAnimation("Run", true);

        last = Time.frameCount;
    }

    [SerializeField] float minAxisDeviation;
    [SerializeField] float forwardSpeed;
    [SerializeField] float jumpPower;
    private float jumpCoeffient = 10f;
    private float interpolation = 0.1f;
    private void Movement()
    {
        rigidbody.position = Vector3.Lerp(transform.position, new Vector3(laneTargetPosition, transform.position.y, transform.position.z), interpolation);

        float horizontalInput = Input.GetAxis("Horizontal");

        if (Mathf.Abs(horizontalInput) > minAxisDeviation && canSwitchLanes)
        {
            StartCoroutine(SwitchLanes(horizontalInput > minAxisDeviation ? 1 : -1));
        }

        rigidbody.position += transform.forward * forwardSpeed * Time.smoothDeltaTime;
        //Debug.Log(forwardSpeed * Time.smoothDeltaTime + " | Delta Time: " + Time.smoothDeltaTime);

        if ((Input.GetKey(KeyCode.Space) || Input.GetAxis("Vertical") > minAxisDeviation) && isGrounded && !crouching)
        {
            rigidbody.AddForce(transform.up * jumpPower * jumpCoeffient, ForceMode.Acceleration);
            isGrounded = false;
        }

        if ((Input.GetKey(KeyCode.LeftControl) || Input.GetAxis("Vertical") < -minAxisDeviation) && isGrounded && !crouching)
        {
            Crouch();
        }
    }

    private float laneTargetPosition;
    void ChangeToLanePosition(int lane)
    {
        laneTargetPosition = Lanes[lane].position.x;
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
        SetAnimation("Crouch", true);
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
            crouchTimer -= Time.smoothDeltaTime;
        }
    }
    private void UnCrouch()
    {
        crouching = false;
        collider.height = 2f;
        collider.center = new Vector3(0, 0, 0);
        SetAnimation("Crouch", false);
    }

    [SerializeField] Animator Animator;
    private void SetAnimation(string name, bool value)
    {
        Animator.SetBool(name, value);
    }

    private bool playing = true;
    float Timer = 0;
    private int last;
    void Update()
    {
        Timer += Time.deltaTime;
        if(Timer >= 1.0f)
        {
            Timer = 0.0f;
            Debug.Log($"FPS: {Time.frameCount - last} | Delta Time: {Time.deltaTime} | Expected FPS: {1.0f / Time.deltaTime}");
            last = Time.frameCount;
        }
        if (playing)
        {
            Crouching();
            Movement();
            Gravity();
        }
        else if(win)
        {
            WinMovement();
        }
    }

    [SerializeField] sc_CameraFollow sc_CameraFollow;
    private bool win = false;
    public void Win()
    {
        playing = false;
        win = true;
        sc_CameraFollow.enabled = false;
        sc_SceneContoller.ChangeScene("Study", 1);
    }

    private void WinMovement()
    {
        rigidbody.position += transform.forward * forwardSpeed * Time.smoothDeltaTime;
    }

    private int lives = 2;
    private void Crash(GameObject collisionObject)
    {
        if(!playing) return;
        lives -= 1;
        if(lives <= 0)
        {
            Crashed();
            return;
        }
        if(lives == 1)
        {
            StartCoroutine(Wounded());
        }
        collisionObject.GetComponent<sc_Obstacle>().Break();
    }

    [SerializeField] float WoundedTime;
    IEnumerator Wounded()
    {
        SetAnimation("Wounded", true);
        yield return new WaitForSeconds(WoundedTime);
        SetAnimation("Wounded", false);
        lives += 1;
    }

    [SerializeField] sc_SceneContoller sc_SceneContoller;
    private void Crashed()
    {
        playing = false;
        SetAnimation("Run", false);
        SetAnimation("Wounded", false);
        sc_SceneContoller.ChangeScene("Dorms", 0);
    }

    [SerializeField] float gravity;
    [SerializeField] float fallingGravity;
    private bool isGrounded = false;
    private int groundedCount = 0;
    private void Gravity()
    {
        if(rigidbody.velocity.y < 0)
        {
            rigidbody.AddForce(Physics.gravity * fallingGravity * Time.smoothDeltaTime, ForceMode.Acceleration);
        } 
        else
        {
            rigidbody.AddForce(Physics.gravity * gravity * Time.smoothDeltaTime, ForceMode.Acceleration);
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            groundedCount += 1;
            if (groundedCount >= 1 && !isGrounded)
            {
                isGrounded = true;
                SetAnimation("Jump", false);
            }
        }
        if (collision.gameObject.CompareTag("SubwayObstacle"))
        {
            Crash(collision.gameObject);
        }
    }
    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            groundedCount -= 1;
            if(groundedCount <= 0)
            {
                isGrounded = false;
                SetAnimation("Jump", true);
            }
        }
    }
}
