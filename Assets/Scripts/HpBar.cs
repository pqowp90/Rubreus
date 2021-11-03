using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBar : MonoBehaviour
{
    public GameObject hpBarPrefab;
    public Transform canvas;

    private RectTransform hpBarT;
    private Image hpbar;
    
    void Start()
    {
        hpBarT = Instantiate(hpBarPrefab , canvas).GetComponent<RectTransform>();
        hpbar = hpBarT.GetChild(0).GetComponentInChildren<Image>();
    }

    void Update()
    {
        Vector3 _hpBarPos = Camera.main.WorldToScreenPoint(new Vector3(transform.position.x,transform.position.y+0.5f,0f));
        hpBarT.position  = _hpBarPos;
    }
    public void SetHp(float hp, float maxHp){
        hpbar.fillAmount = hp/maxHp;
        Debug.Log(hpbar.gameObject.name);
    }
    public void DestroyHpbar(){
        Destroy(hpBarT.gameObject);
    }
}
