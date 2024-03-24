using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class sc_SpaceBar : MonoBehaviour
{
    [SerializeField] float cooldown;
    public int SpaceAvailable { get; set; }

    private float timer = 0;
    void Update()
    {
        if (SpaceAvailable == -1) SpaceAvailable = 0;
        if(timer <= 0  && SpaceAvailable == 0)
        {
            SpaceAvailable = 1;
            timer = cooldown;
        }
        if(timer > 0 && SpaceAvailable == 0)
        {
            timer -= Time.deltaTime;
        }
        UpdateImage();
    }

    [SerializeField] Image progressBar;
    private void UpdateImage()
    {
        if (SpaceAvailable == 1)
        {
            progressBar.fillAmount = 1;
            return;
        }
        progressBar.fillAmount = 1 - timer / cooldown;
    }
}
