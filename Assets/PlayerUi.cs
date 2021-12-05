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
    [SerializeField]
    private Image HpBar;
    [SerializeField]
    private Text HpText;
    [SerializeField]
    private Image StaminaBar;
    private float alphaTime;
    [SerializeField]
    private Transform gunUi;
    private float nowHp, hp;
    void Start()
    {
        canvasGroup = gunUi.GetComponent<CanvasGroup>();
        gunUi.eulerAngles = new Vector3(90f,gunUi.eulerAngles.y,gunUi.eulerAngles.z);
    }

    private void Update(){
        if(alphaTime>0f)alphaTime-=Time.deltaTime;
        canvasGroup.alpha = Mathf.Lerp(canvasGroup.alpha,(alphaTime>0f)?1f:0f,0.1f);
        hp = Mathf.Lerp(hp, nowHp, 0.1f);
        HpText.text = string.Format("{0}",(int)hp);
    }
    public void UpdateGunUi()
    {
        
        bulletNum.text = string.Format("{0}",Player.bullet);
        bulletBar.fillAmount = (float)Player.bullet/(float)Player.maxBullet;
    }
    public void SetHpUi(float hp, float maxHp)
    {
        HpBar.fillAmount = hp/maxHp;
        nowHp = hp;
        
    }
    public void OnUI(float time){
        alphaTime=time;
    }
}
