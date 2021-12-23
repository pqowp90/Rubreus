using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Drone : MonoBehaviour
{
    public RangePotap rangePotap;
    public void droneMove(int num, Vector3 targetPos){

        Vector3 startPos = GameManager.Instance.GetrandomPos();
        transform.position = startPos;
        
        Transform poTap=GameManager.Instance.MakePoTapTap(num, transform);
        poTap.GetComponent<Falldown>().rangePotap = rangePotap;
        poTap.SetParent(transform);
        poTap.localScale = new Vector3(1.5f,1.5f,1.5f);

        Sequence mySequence = DOTween.Sequence();
        
        Vector3 angle = new Vector3(0f,0f,GameManager.GetAngle(targetPos, startPos));
        
        transform.DORotate(angle+new Vector3(0f,0f,90f),1f);
        mySequence.Append(transform.DOMove(targetPos, 1f).
            OnComplete(()=>{poTap.GetComponent<Falldown>().installationPotap();poTap.SetParent(null);}));
        
        mySequence.AppendInterval(0.3f);
        Vector3 newPos = (startPos-targetPos)*2f+GameManager.Instance.player.position;
        mySequence.Append(transform.DOMove(newPos, 2f).OnComplete(()=>{AllPoolManager.Instance.PoolObj(transform, 8);}).OnStart(()=>{transform.DORotate(angle+new Vector3(0f,0f,-90f),1f);}));
    }
}
