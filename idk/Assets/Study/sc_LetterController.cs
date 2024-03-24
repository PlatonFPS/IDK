using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class sc_LetterController : MonoBehaviour
{
    [SerializeField] string sentence;

    private float progress = 0;
    [SerializeField] sc_PenFollow pen;
    public void ReturnLetter(sc_Letter letter)
    {
        letters.Add(letter);
        progress += 1;
        UpdateBar();
        if(progress == sentence.Length)
        {
            pen.Win();
        }
    }

    [SerializeField] Image progressBar;
    private void UpdateBar()
    {
        progressBar.fillAmount = progress / sentence.Length;
    }

    [SerializeField] float initialDelay;
    [SerializeField] float delay;
    private float Timer;
    private List<sc_Letter> letters = new List<sc_Letter>();
    [SerializeField] Transform letterOrigin;
    private void Awake()
    {
        Timer = initialDelay;
        for(int i = 0; i < letterOrigin.childCount; i++)
        {
            letters.Add(letterOrigin.GetChild(i).GetComponent<sc_Letter>());
        }
    }

    void Update()
    {
        if(index < sentence.Length)
        {
            if (Timer > 0)
            {
                Timer -= Time.deltaTime;
            }
            if (Timer < 0)
            {
                SendSentence();
                Timer = delay;
            }
        }
    }

    [SerializeField] Transform left;
    [SerializeField] Transform right;
    [SerializeField] Transform up;
    [SerializeField] Transform down;
    [SerializeField] float speed;
    private int index = 0;
    private void SendSentence()
    {
        Vector3 position = new Vector3(Random.Range(left.position.x, right.position.x), left.position.y, left.position.z);
        letters[0].StartMove(position, down.position, up.position, speed, sentence[Mathf.FloorToInt(index)].ToString());
        letters.RemoveAt(0);
        index += 1;
    }
}
