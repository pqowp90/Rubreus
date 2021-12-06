using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuUi : MonoSingleton<MenuUi>
{
    private GameObject option;
    public bool isOption=false;
    private void Awake(){
        option=Instantiate(Resources.Load<GameObject>("Option"));
        option.transform.SetParent(transform);
    }
    public void Option(){
        option.SetActive(true);
    }
}
