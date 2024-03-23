using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sc_DropController : MonoBehaviour
{
    private List<sc_DropCollision> sc_DropCollisions = new List<sc_DropCollision>();
    [SerializeField] float delay;
    [SerializeField] float startDelay;
    private float Timer;
    private void Awake()
    {
        Timer = startDelay;
        for(int i = 0; i < transform.childCount; i++)
        {
            Debug.Log(transform.GetChild(i).name);
            sc_DropCollisions.Add(transform.GetChild(i).GetComponent<sc_DropCollision>());
            Debug.Log(transform.GetChild(i).name);
        }
    }

    public void AddDrop(sc_DropCollision drop)
    {
        sc_DropCollisions.Add(drop);
    }

    [SerializeField] float radius;
    [SerializeField] float speed;
    [SerializeField] Transform pen;
    private void SendDrop()
    {
        if(sc_DropCollisions.Count == 0) return;
        int index = Random.Range(0, sc_DropCollisions.Count);
        float angle = Random.Range(0, 360);
        Vector3 dropPosition = new Vector3(Mathf.Cos(angle) * radius, Mathf.Sin(angle) * radius, 0);
        sc_DropCollisions[index].StartMove(dropPosition, pen.position, speed, radius * 2);
        sc_DropCollisions.RemoveAt(index);
    }

    void Update()
    {
        if(Timer > 0)
        {
            Timer -= Time.deltaTime;
        }
        else
        {
            Timer = delay;
            SendDrop();
        }
    }
}
