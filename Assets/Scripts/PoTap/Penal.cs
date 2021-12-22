using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Penal : MonoBehaviour
{
    public int num;
    public Image potapImage;
    public Text costText;
    public void MakePoTap(){
        if(GameManager.Instance.CurrentUser.potapList[num].cost>GameManager.Instance.GetMoney())return;
        

        if(GameManager.Instance.isMakeingPotap==true)return;
        GameManager.Instance.isMakeingPotap = true;
        
        RangePotap drone = AllPoolManager.Instance.GetObj(9).GetComponent<RangePotap>();
        drone.num = num;
        drone.gameObject.SetActive(true);
    }
}
