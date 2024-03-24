using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class sc_Letter : MonoBehaviour
{
    private Vector3 down;
    private Vector3 up;
    private float speed = 0f;
    public void StartMove(Vector3 position, Vector3 down, Vector3 up, float speed)
    {
        transform.position = position;
        this.down = down;
        this.up = up;
        this.speed = speed;
    }

    [SerializeField] sc_PenFollow sc_PenFollow;
    [SerializeField] sc_LetterController sc_LetterController;
    [SerializeField] sc_SpaceBar sc_SpaceBar;
    void Update()
    {
        transform.Translate(new Vector3(0, -speed * Time.deltaTime, 0));
        if(transform.position.y < down.y)
        {
            sc_PenFollow.Lose();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            
            if(transform.position.y < up.y)
            {
                if(sc_SpaceBar.SpaceAvailable)
                {
                    sc_SpaceBar.SpaceAvailable = false;
                    sc_LetterController.ReturnLetter(this);
                    transform.localPosition = Vector3.zero;
                    speed = 0f;
                }
            }
        }
    }
}
