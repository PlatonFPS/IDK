using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class sc_SpaceBar : MonoBehaviour
{
    [SerializeField] float cooldown;
    public bool SpaceAvailable { get; set; }

    private float timer = 0;
    void Update()
    {
        if(timer <= 0  && !SpaceAvailable)
        {
            SpaceAvailable = true;
            timer = cooldown;
        }
        if(timer > 0 && !SpaceAvailable)
        {
            timer -= Time.deltaTime;
        }
        UpdateImage();
    }

    [SerializeField] Image progressBar;
    private void UpdateImage()
    {
        if (SpaceAvailable)
        {
            progressBar.fillAmount = 1;
            return;
        }
        progressBar.fillAmount = 1 - timer / cooldown;
    }
}
