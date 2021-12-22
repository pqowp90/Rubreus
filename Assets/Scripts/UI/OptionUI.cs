using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionUI : MonoBehaviour
{
    public void GoRobby(){
        if(!(SceneManager.GetActiveScene().name == "MainRobby"))
            LoadingScene.LoadScene("MainRobby");
        transform.parent.gameObject.SetActive(false);
    }
    private void OnEnable(){
        ClickSound.Instance.GoSound(0);
        MenuUi.Instance.isOption = true;
    }
    private void OnDisable(){
        ClickSound.Instance.GoSound(1);
        MenuUi.Instance.isOption = false;
    }
    
}
