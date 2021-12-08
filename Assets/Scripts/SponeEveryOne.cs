using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SponeEveryOne : MonoBehaviour
{
    [SerializeField]
    private Text waveText;
    [SerializeField]
    private GameObject hi;
    private float time=5f;
    private int hello=0;
    [Header("웨이브")]
    public List<Wave> waves = new List<Wave>();
    public List<GameObject> enemys = new List<GameObject>();
    public int wave;
    private bool skipWave;
    void Start()
    {
        wave=0;
        
        waveText.text = string.Format("{0}", wave+1);
        int a=0;
        foreach (var wave in waves){
            foreach (var sponer in wave.sponers){
                a++;
                sponer.Name = (a*5)+"초";
            }
        }
    }
    public void NextWave(){
        skipWave = false;
    }

    void FixedUpdate()
    {
        if(skipWave)return;
        hi.SetActive(false);
        if(enemys.Count!=0){
            foreach (var enemy in enemys){
                if(enemy==null)continue;
                if(enemy.activeSelf == false||(enemy.gameObject == null)){
                    
                    enemys.Remove(enemy);
                    break;
                }
            }
        }
        if(waves[wave].sponers.Count<=hello){
            if(enemys.Count<=1){
                hello=0;
                
                wave++;
                waveText.text = string.Format("{0}", wave+1);
                if(waves.Count<=wave){
                    Debug.Log("와 게임을 클리어 하셨어요 축하축하");
                    LoadingScene.LoadScene("MainRobby");
                    Destroy(gameObject);
                }
                skipWave = true;
                hi.SetActive(true);
            }
            return;
        }
        
        time+=Time.fixedDeltaTime;
        
        if(time >= 5f){
            time = 0f;
            
            foreach (var sponer in waves[wave].sponers[hello].enemySpones){
                StartCoroutine(EnemySponehi(sponer.deley,sponer.Num,sponer.Index,sponer.SponePos));
            }
            
            hello++;
        }
        
        
    }
    private IEnumerator EnemySponehi(float deley, int Num, int Index, Transform SponePos){
        for(int i=0;i<Num;i++){
            GameObject enemyhi;
            enemyhi = AllPoolManager.Instance.GetObjPos(Index,SponePos).gameObject;
            enemyhi.gameObject.SetActive(true);
            enemys.Add(enemyhi);
            yield return new WaitForSeconds(deley);
        }
    }
}
