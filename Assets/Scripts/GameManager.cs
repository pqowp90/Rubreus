using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    public PlayerUi playerUi;
    public Canvas hpBarCanvas;
    [SerializeField]
    private Transform[] randomPos;
    public Transform player;
    [SerializeField]
    private GameObject potapPenal;
    [SerializeField]
    private User user = null;
    public User CurrentUser {get{return user;}}
    [SerializeField]
    private Sprite[] turrets;
    public bool isMakeingPotap=false;
    public bool isBumbCharging;
    public Transform bumbPos;
    public void CreatePenal(){        
        for(int j=0;j<user.potapList.Count;j++){
            GameObject newPanel = null;
            newPanel = Instantiate(potapPenal,potapPenal.transform.parent);
            newPanel.SetActive(true);
            Penal penal = newPanel.GetComponent<Penal>();
            penal.potapImage.sprite = turrets[j];
            penal.num = j;
        }
    }
    public void BumbHello(){
        isBumbCharging = true;
        bumbPos = FindObjectOfType<RealBumb>().transform;
    }
    public Transform MakePoTapTap(int num, Transform pos){
        Transform poTap = AllPoolManager.Instance.GetObjPos(user.potapList[num].indexNum, pos);
        poTap.gameObject.SetActive(true);
        return poTap;
    }
    private void Awake(){
        playerUi = FindObjectOfType<PlayerUi>();
        player = FindObjectOfType<Player>().transform;
        CreatePenal();
    }
    public Vector3 GetrandomPos(){
        //Debug.Log(randomPos.Length);
        if(randomPos.Length==0)
            return Vector3.zero;
        return randomPos[Random.Range(0,randomPos.Length)].position;
    }
    public static float GetAngle (Vector3 vStart, Vector3 vEnd)
    {
        Vector3 v = vEnd - vStart;
        v.Normalize();
 
        return Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg;
    }
    public IEnumerator DestroyEnemy(){
        yield return new WaitForSeconds(1f);
    }
}
