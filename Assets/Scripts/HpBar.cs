using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBar : MonoBehaviour
{
    public GameObject hpBarPrefab;
    public Transform canvas;
    [SerializeField]
    private RectTransform hpBarT;
    private Image[] hpbar = new Image[2];
    private float showTime=0f;
    
    void OnEnable()
    {
        canvas = GameManager.Instance.hpBarCanvas.transform;
        hpBarT = AllPoolManager.Instance.GetObjTransform(2 , GameManager.Instance.hpBarCanvas.transform).GetComponent<RectTransform>();
        hpbar[0] = hpBarT.GetChild(0).GetComponentInChildren<Image>();
        hpbar[1] = hpBarT.GetComponent<Image>();
        hpBarT.gameObject.SetActive(true);
        hpbar[0].enabled = false;
        hpbar[1].enabled = false;
    }

    void Update()
    {
        if(showTime>=0f){
            showTime-=Time.deltaTime;
        }

        hpbar[0].enabled = !(showTime<0f);
        hpbar[1].enabled = !(showTime<0f);
        
        Vector3 _hpBarPos = Camera.main.WorldToScreenPoint(new Vector3(transform.position.x,transform.position.y+0.5f,0f));
        hpBarT.position  = new Vector3(transform.position.x,transform.position.y+0.4f,0f);
    }
    public void SetHp(float hp, float maxHp){
        if(hpbar==null)return;
        hpbar[0].fillAmount = hp/maxHp;
        showTime = 2f;
    }
    public void DestroyHpbar(){
        AllPoolManager.Instance.PoolObj(hpBarT, 2);
    }
}
