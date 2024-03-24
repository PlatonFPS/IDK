using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sc_SceneContoller : MonoBehaviour
{
    public void ChangeScene(string sceneName, int win)
    {
        StartCoroutine(Animation(sceneName, win));
    }
    public void ChangeScene(string sceneName)
    {
        StartCoroutine(Animation(sceneName, -1));
    }
    [SerializeField] float loadingTime = 2f;
    [SerializeField] Animator fade;
    [SerializeField] Animator message;
    IEnumerator Animation(string sceneName, int win)
    {
        if(win != -1)
        {
            message.SetTrigger(win == 1 ? "Win" : "Lose");
            yield return new WaitForSeconds(2);
        }
        fade.SetTrigger("ChangeScene");
        yield return new WaitForSeconds(2 + loadingTime);
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }
}
