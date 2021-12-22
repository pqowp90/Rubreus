using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Falldown : MonoBehaviour
{
    private HpBar hpBar;
    public int index;
    public int lifeTime, maxLifeTime;
    public RangePotap rangePotap;
    [SerializeField]
    private float lifeCutPerSecond;
    private void Start(){
        hpBar = GetComponent<HpBar>();
        if(lifeCutPerSecond>0){
            StartCoroutine(CutLife());
        }
    }
    private IEnumerator CutLife(){
        while(true){
            lifeTime--;
            hpBar.SetHp(lifeTime, maxLifeTime);
            yield return new WaitForSeconds(lifeCutPerSecond);
        }
    }
    public void LosePotapHp(){
        lifeTime--;
        hpBar.SetHp(lifeTime, maxLifeTime);
        if(lifeTime<=0){
            hpBar.DestroyHpbar();
            AllPoolManager.Instance.PoolObj(transform, index);
        }
    }
    public void EndPosingPotap(){

        transform.localScale = new Vector3(1.5f,1.5f,1.5f);
    }
    public void installationPotap(){
        hpBar = GetComponent<HpBar>();
        if(hpBar!=null){
            lifeTime = maxLifeTime;
            hpBar.SetHp(lifeTime , maxLifeTime);
        }
        PoTapBase poTapBase = GetComponent<PoTapBase>();
        
        transform.DOScale(new Vector3(1f,1f,1f), 1f).OnComplete(()=>{if(poTapBase!=null) StartCoroutine(poTapBase.RepeatingFind());AllPoolManager.Instance.GetObjPos(7, transform).gameObject.SetActive(true);});
    }
}
