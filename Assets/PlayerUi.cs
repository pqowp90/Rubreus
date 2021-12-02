using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PlayerUi : MonoBehaviour
{
    private CanvasGroup canvasGroup;
    [SerializeField]
    private Text bulletNum;
    [SerializeField]
    private Image bulletBar;
    private float alphaTime;
    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        transform.eulerAngles = new Vector3(90f,transform.eulerAngles.y,transform.eulerAngles.z);
    }

    private void Update(){
        if(alphaTime>0f)alphaTime-=Time.deltaTime;
        canvasGroup.alpha = Mathf.Lerp(canvasGroup.alpha,(alphaTime>0f)?1f:0f,0.1f);
    }
    public void UpdateUi()
    {
        
        bulletNum.text = string.Format("{0}",Player.bullet);
        Debug.Log(1f-Player.bullet/Player.maxBullet);
        bulletBar.fillAmount = (float)Player.bullet/(float)Player.maxBullet;
    }
    public void OnUI(float time){
        alphaTime=time;
    }
}
