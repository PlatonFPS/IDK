using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sc_Obstacle : MonoBehaviour
{
    private MeshRenderer renderer;
    private Collider collider;
    private void Awake()
    {
        renderer = GetComponent<MeshRenderer>();
        collider = GetComponent<Collider>();
    }

    [SerializeField] float dissappearDuration = 2;
    public void Break()
    {
        StartCoroutine(Dissappear());
    }
    IEnumerator Dissappear()
    {
        renderer.enabled = false;
        collider.enabled = false;
        yield return new WaitForSeconds(dissappearDuration);
        collider.enabled = true;
        renderer.enabled = true;
    }
}
