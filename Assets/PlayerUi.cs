using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUi : MonoBehaviour
{
    [SerializeField]
    private Text bulletNum;
    [SerializeField]
    private Image bulletBar;
    void Start()
    {
        transform.eulerAngles = new Vector3(90f,transform.eulerAngles.y,transform.eulerAngles.z);
    }


    public void UpdateUi()
    {
        bulletNum.text = string.Format("{0}",Player.bullet);
        Debug.Log(1f-Player.bullet/Player.maxBullet);
        bulletBar.fillAmount = (float)Player.bullet/(float)Player.maxBullet;
    }
}
