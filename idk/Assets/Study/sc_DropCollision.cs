using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sc_DropCollision : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<sc_PenFollow>().Lose();
        }
    }

    private float speed = 0f;
    public void StartMove(Vector3 position, Vector3 target, float speed, float travelDistance)
    {
        transform.position = position;
        float tan = (target.y - position.y) / (target.x - position.x);
        int dir = target.x - position.x < 0 ? 1 : -1;
        float angle = Mathf.Atan(tan) * Mathf.Rad2Deg + +(dir > 0 ? 180 : 0);
        transform.rotation = Quaternion.Euler(0, 0, angle);
        this.speed = speed;
        this.trabelDistance = travelDistance;
    }

    [SerializeField] sc_DropController sc_DropController;
    private void StopMove()
    {
        distance = 0f;
        trabelDistance = 0f;
        speed = 0f;
        sc_DropController.AddDrop(this);
    }

    private float distance = 0f;
    private float trabelDistance = 0f;
    private void Update()
    {
        transform.Translate(Time.deltaTime * speed, 0, 0);
        if(speed > 0)
        {
            distance += Time.deltaTime * speed;
            if(distance > trabelDistance)
            {
                StopMove();
            }
        }
    }
}
