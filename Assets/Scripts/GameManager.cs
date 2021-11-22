using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    private Transform player;
    [SerializeField]
    private GameObject potapPenal;
    [SerializeField]
    private User user = null;
    public User CurrentUser {get{return user;}}
    [SerializeField]
    private Sprite[] turrets;
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
    public void MakePoTapTap(int num){
        Transform poTap = AllPoolManager.Instance.GetObjPos(user.potapList[num].indexNum, player);
        poTap.gameObject.SetActive(true);
    }
    private void Start(){
        player = FindObjectOfType<Player>().transform;
        CreatePenal();
    }
}
