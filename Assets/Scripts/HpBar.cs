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
    
    // Start is called before the first frame update
    void Start()
    {
        hpBarT = Instantiate(hpBarPrefab , canvas).GetComponent<RectTransform>();
        hpbar = hpBarT.GetComponentInChildren<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 _hpBarPos = Camera.main.WorldToScreenPoint(new Vector3(transform.position.x,transform.position.y+0.5f,0f));
        hpBarT.position  = _hpBarPos;
    }
    public void SetHp(float hp, float maxHp){
        hpbar.fillAmount = hp/maxHp;
    }
    public void DestroyHpbar(){
        Destroy(hpBarT.gameObject);
    }
}
