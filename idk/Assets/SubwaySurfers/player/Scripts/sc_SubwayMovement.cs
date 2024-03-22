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
        canSwitchLanes = true;
        ChangeToLanePosition(currentLane);
    }

    public float minAxisDeviation = 0.1f;
    public float forwardSpeed = 5f;
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
    }

    void ChangeToLanePosition(int lane)
    {
        Vector3 position = Lanes[lane].position;
        transform.position = new Vector3(position.x, transform.position.y, transform.position.z);
    }

    private bool canSwitchLanes;
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

    void Update()
    {
        Movement();
    }
}
