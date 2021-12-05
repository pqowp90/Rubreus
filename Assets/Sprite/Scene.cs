using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene : MonoBehaviour
{
    [SerializeField]
    private float nextDeley;
    [SerializeField]
    private string sceneName;
    private void Start(){
        Invoke("Next", nextDeley);
    }
    private void Next(){
        SceneManager.LoadScene(sceneName);
    }
}
