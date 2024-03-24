using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sc_SceneContoller : MonoBehaviour
{
    [SerializeField] bool noLoadingScreenStart;
    public bool noLoadingScreenEnd { get; set; }
    private void SetBools()
    {
        fade.SetBool("AltStart", noLoadingScreenStart);
        fade.SetBool("AltEnd", noLoadingScreenEnd);
    }

    private void Awake()
    {
        noLoadingScreenEnd = false;
        SetBools();
    }

    public void ChangeScene(string sceneName, bool win)
    {
        if(win == false)
        {
            message.SetTrigger("Lose");
        }
        else
        {
            StartCoroutine(Animation(sceneName, true));
        }
    }
    public void ChangeScene(string sceneName)
    {
        StartCoroutine(Animation(sceneName));
    }
    [SerializeField] float loadingTime = 2f;
    [SerializeField] Animator fade;
    [SerializeField] Animator message;
    IEnumerator Animation(string sceneName, bool win = false)
    {
        if (win == true)
        {
            message.SetTrigger("Win");
            yield return new WaitForSeconds(2);
        }
        SetBools();
        fade.SetTrigger("ChangeScene");
        yield return new WaitForSeconds(2 + loadingTime);
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }
}
