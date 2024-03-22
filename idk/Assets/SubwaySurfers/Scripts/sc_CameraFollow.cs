using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sc_CameraFollow : MonoBehaviour
{
    [SerializeField] Transform player;

    float zOffset;

    private void Awake()
    {
        zOffset = transform.position.z - player.position.z;
    }

    private void FollowPlayer()
    {
        float newZOffset = transform.position.z - player.position.z;
        transform.position += new Vector3(0, 0, zOffset - newZOffset);
    }


    float interpolationSpeed = 0.05f;
    float horizontalOffsetCoefficient = 1.5f;
    private void FollowLanes()
    {
        float newX = transform.position.x + (player.position.x / horizontalOffsetCoefficient - transform.position.x) * interpolationSpeed;
        transform.position = new Vector3(newX, transform.position.y, transform.position.z);
    }

    void Update()
    {
        FollowPlayer();
        FollowLanes();
    }
}
