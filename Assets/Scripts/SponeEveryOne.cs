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
    public int hello=0;
    [Header("웨이브")]
    public List<Wave> waves = new List<Wave>();
    public List<GameObject> enemys = new List<GameObject>();
    public int wave;
    private bool skipWave;
    private int usingCor;
    void Start()
    {
        wave=0;
        
        waveText.text = string.Format("Wave:{0}", wave);
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
                if(enemy==null){enemys.Remove(enemy);break;}
                else if(enemy.activeSelf == false){enemys.Remove(enemy);break;}
                
            }
        }
        if(waves[wave].sponers.Count<hello&&usingCor == 0){
            if(enemys.Count<=0){
                for (int i = 0; i < enemys.Count; i++){
                    enemys.RemoveAt(i);
                }
                
                hello=0;
                
                wave++;
                waveText.text = string.Format("Wave:{0}", wave);
                if(waves.Count<=wave){
                    FindObjectOfType<RealBumb>().StartBBBBBBBBBBBBBBBBBBaaaaaaaaaaaaaaaannnnnnnnnngggggggggggg();
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
            
            if(waves[wave].sponers.Count>hello){
                foreach (var sponer in waves[wave].sponers[hello].enemySpones){
                    StartCoroutine(EnemySponehi(sponer.deley,sponer.Num,sponer.Index,sponer.SponePos));
                }
            }
            hello++;
            
        }
        
        
    }
    private IEnumerator EnemySponehi(float deley, int Num, int Index, Transform SponePos){
        usingCor++;
        for(int i=0;i<Num;i++){
            GameObject enemyhi;
            enemyhi = AllPoolManager.Instance.GetObjPos(Index,SponePos).gameObject;
            enemyhi.gameObject.SetActive(true);
            enemys.Add(enemyhi);
            yield return new WaitForSeconds(deley);
        }
        usingCor--;
    }
}
