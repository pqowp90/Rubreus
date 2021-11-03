using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBar : MonoBehaviour
{
    public GameObject hpBarPrefab;
    public Transform canvas;

    private RectTransform hpBarT;
    private Image[] hpbar = new Image[2];
    private float showTime=0f;
    
    void Start()
    {
        hpBarT = Instantiate(hpBarPrefab , canvas).GetComponent<RectTransform>();
        hpbar[0] = hpBarT.GetChild(0).GetComponentInChildren<Image>();
        hpbar[1] = hpBarT.GetComponent<Image>();
    }

    void Update()
    {
        if(showTime>=0f){
            showTime-=Time.deltaTime;
        }

        hpbar[0].enabled=!(showTime<0f);
        hpbar[1].enabled=!(showTime<0f);
        
        Vector3 _hpBarPos = Camera.main.WorldToScreenPoint(new Vector3(transform.position.x,transform.position.y+0.5f,0f));
        hpBarT.position  = new Vector3(transform.position.x,transform.position.y+0.5f,0f);
    }
    public void SetHp(float hp, float maxHp){
        hpbar[0].fillAmount = hp/maxHp;
        showTime = 2f;
    }
    public void DestroyHpbar(){
        Destroy(hpBarT.gameObject);
    }
}
