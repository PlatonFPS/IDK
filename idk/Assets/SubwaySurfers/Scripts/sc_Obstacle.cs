using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sc_Obstacle : MonoBehaviour
{
    [SerializeField] GameObject model;
    private Collider collider;
    private void Awake()
    {
        collider = GetComponent<Collider>();
    }

    [SerializeField] float dissappearDuration = 2;
    public void Break()
    {
        StartCoroutine(Dissappear());
    }
    IEnumerator Dissappear()
    {
        model.SetActive(false);
        collider.enabled = false;
        yield return new WaitForSeconds(dissappearDuration);
        collider.enabled = true;
        model.SetActive(true);
    }
}
