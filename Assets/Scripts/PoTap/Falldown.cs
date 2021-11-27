using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Falldown : MonoBehaviour
{
    private void Start()
    {
        
        transform.localScale = new Vector3(1.5f,1.5f,1.5f);
        
    }
    public void installationPotap(){
        PoTapBase poTapBase = GetComponent<PoTapBase>();
        
        transform.DOScale(new Vector3(1f,1f,1f), 1f).OnComplete(()=>{if(poTapBase!=null) StartCoroutine(poTapBase.RepeatingFind());AllPoolManager.Instance.GetObjPos(7, transform).gameObject.SetActive(true);});
    }
}
