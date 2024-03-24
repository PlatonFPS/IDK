using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneChange : MonoBehaviour
{
    [SerializeField] sc_SceneContoller sc_SceneContoller;
    [SerializeField] string scene_name;
    [SerializeField] sc_MusicContoller sc_MusicContoller;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            sc_MusicContoller.Stop();
            sc_SceneContoller.ChangeScene(scene_name);
        }
    }
}
