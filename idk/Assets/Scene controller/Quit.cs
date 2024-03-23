using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quit : MonoBehaviour
{
    public void CloseApplication()
    {
        StartCoroutine(Close());
    }

    [SerializeField] float time;
    IEnumerator Close()
    {
        yield return new WaitForSeconds(time);
        Application.Quit();
    }

    private void Update()
    {
        
    }
}
