using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoSingleton<GameManager>
{
    [SerializeField]
    private Transform moneyTransform;
    [SerializeField]
    private Transform waveTransform;
    [SerializeField]
    private Text clearText;
    [SerializeField]
    private GameObject hihihihihihi;
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
    private int money;
    private GameObject panelboom;
    [SerializeField]
    private Text moneyText;
    public MousePos look;
    public bool gameEnd;
    
    public void CreatePenal(){
        DownLoadUI.Instance.panels.Clear();
        for(int j=0;j<user.potapList.Count;j++){
            GameObject newPanel = null;
            newPanel = Instantiate(potapPenal,potapPenal.transform.parent);
            //newPanel.SetActive(true);
            DownLoadUI.Instance.panels.Add(newPanel.transform);
            Penal penal = newPanel.GetComponent<Penal>();
            penal.potapImage.sprite = turrets[j];
            penal.num = j;
            penal.costText.text = "$"+user.potapList[j].cost;
            if(j == 6){
                panelboom = penal.gameObject;
            }
        }
    }
    public int GetMoney(){return money;}
    public void AddMoney(int money){
        this.money += money;
        // Transform textTransform = AllPoolManager.Instance.GetObjTransform(32, moneyTransform);
        // textTransform.localPosition = Vector3.zero;
        // textTransform.localScale = new Vector3(1f, 1f, 1f);
        // textTransform.gameObject.SetActive(true);
        moneyText.text = string.Format("Money:${0}", this.money);
    }
    public void BumbHello(){
        isBumbCharging = true;
        DownLoadUI.Instance.panels.Remove(panelboom.transform);
        Destroy(panelboom);
        hihihihihihi.SetActive(true);
        bumbPos = FindObjectOfType<RealBumb>().transform;
        player.GetComponent<Player>().responePos = bumbPos;
    }
    public Transform MakePoTapTap(int num, Transform pos){
        Transform poTap = AllPoolManager.Instance.GetObjPos(user.potapList[num].indexNum, pos);
        Falldown falldown = poTap.GetComponent<Falldown>();
        if(falldown!=null){
            falldown.maxLifeTime = user.potapList[num].maxLifeTime;
            falldown.index = user.potapList[num].indexNum;
        }
            
        poTap.gameObject.SetActive(true);
        return poTap;
    }
    public void GameClear(bool hihi){
        clearText.text = (hihi)?"mission clear":"mission fail";
        gameEnd = true;
    }
    private void Awake(){
        look = FindObjectOfType<MousePos>();
        money = 350;
        moneyText.text = string.Format("Money:${0}", this.money);
        playerUi = FindObjectOfType<PlayerUi>();
        player = FindObjectOfType<Player>().transform;
        CreatePenal();
    }
    private void Update(){
        if(Input.GetKeyDown(KeyCode.Escape)){
            MenuUi.Instance.Option();
            
        }
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
