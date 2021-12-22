using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class LoadAPI : MonoBehaviour
{
    public bool done;
    [SerializeField]
    private const string URL = "https://docs.google.com/spreadsheets/d/1NXGvm20BzzGt2KcDWHzhMR1geS_7gzQCvUoxdcfXTd0/export?format=tsv&range=A2:F11";
    [SerializeField]
    private EnemySO enemySO;
    private void Start(){
        StartCoroutine(DownloadInfo());
    }
    private IEnumerator DownloadInfo(){
        UnityWebRequest www = UnityWebRequest.Get(URL);
        
        yield return www.SendWebRequest();
        SetInfo(www.downloadHandler.text);
    }
    private void SetInfo(string tsv){
        
        
        string[] row = tsv.Split('\n');
        int rowSize = row.Length;
        int columnSize = row[0].Split('\t').Length;

        for(int i=0;i<rowSize;i++){
            string[] column = row[i].Split('\t');
            for(int j=0;j<columnSize;j++){
                EnemyInfo targetEnemy = enemySO.enemies[i];

                targetEnemy.enemtName = column[0];
                targetEnemy.maxHp = int.Parse(column[1]);
                targetEnemy.damage = float.Parse(column[2]);
                targetEnemy.attackDeley = float.Parse(column[3]);
                targetEnemy.attackRange = float.Parse(column[4]);
                targetEnemy.index = int.Parse(column[5]);
            }
        }
        done = true;

    }
}
