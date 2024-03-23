using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class sc_Letter : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI letter;

    private Vector3 down;
    private Vector3 up;
    private float speed = 0f;
    public void StartMove(Vector3 position, Vector3 down, Vector3 up, float speed, string letter)
    {
        transform.position = position;
        this.down = down;
        this.up = up;
        this.speed = speed;
        this.letter.text = letter;
    }

    [SerializeField] sc_PenFollow sc_PenFollow;
    [SerializeField] sc_LetterController sc_LetterController;
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
                sc_LetterController.ReturnLetter(this);
                transform.localPosition = Vector3.zero;
                speed = 0f;
            }
        }
    }
}
