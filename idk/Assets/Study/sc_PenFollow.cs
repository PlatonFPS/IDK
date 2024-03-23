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
        Debug.Log("Win");
        sc_SceneContoller.ChangeScene("Dorms");
    }
    public void Lose()
    {
        Debug.Log("Lose");
        sc_SceneContoller.ChangeScene("Dorms");
    }

    void Update()
    {
        FollowMouse();
    }
}
