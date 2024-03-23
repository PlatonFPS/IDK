using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sc_SceneContoller : MonoBehaviour
{
    public void ChangeScene(string sceneName)
    {
        StartCoroutine(Animation(sceneName));
    }
    [SerializeField] float loadingTime = 2f;
    [SerializeField] Animator animator;
    IEnumerator Animation(string sceneName)
    {
        animator.SetTrigger("ChangeScene");
        yield return new WaitForSeconds(2 + loadingTime);
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }
}
