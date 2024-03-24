using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sc_PenFollow : MonoBehaviour
{
    private void FollowMouse()
    {
        Vector3 mousePosition = Input.mousePosition;
        Ray castPoint = Camera.main.ScreenPointToRay(mousePosition);
        RaycastHit hit;
        if(Physics.Raycast(castPoint, out hit, Mathf.Infinity))
        {
            transform.position = hit.point + new Vector3(0, 0, -0.0222f);
        }
    }

    [SerializeField] sc_SceneContoller sc_SceneContoller;
    public void Win()
    {
        sc_SceneContoller.ChangeScene("Dorms", 1);
    }
    public void Lose()
    {
        sc_SceneContoller.ChangeScene("Dorms", 0);
    }

    void Update()
    {
        FollowMouse();
    }
}
