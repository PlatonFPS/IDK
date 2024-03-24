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
    private bool decided = false;
    public void Win()
    {
        if(!decided)
        {
            sc_SceneContoller.ChangeScene("Dorms", 1);
            decided = true;
        }
        
    }
    public void Lose()
    {
        if (!decided)
        {
            sc_SceneContoller.ChangeScene("Dorms", 0);
            decided = true;
        }
    }

    void Update()
    {
        FollowMouse();
    }
}
