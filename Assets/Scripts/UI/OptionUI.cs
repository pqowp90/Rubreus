using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionUI : MonoBehaviour
{
    private void OnEnable(){
        MenuUi.Instance.isOption = true;
    }
    private void OnDisable(){
        ClickSound.Instance.GoSound(1);
        MenuUi.Instance.isOption = false;
    }
    private void Update(){
        if(Input.GetKeyDown(KeyCode.Escape)){
            transform.parent.gameObject.SetActive(false);
        }
    }
}
