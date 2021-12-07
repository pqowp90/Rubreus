using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingScene : MonoBehaviour
{
    static string nextScene;
    [SerializeField]
    Image progressBar;
    [SerializeField]
    private Transform playerPos;
    public static void LoadScene(string sceneName){
        nextScene = sceneName;
        SceneManager.LoadScene("LoadingScene");
    }
    void Start()
    {
        StartCoroutine(LoadSceneProcess());
    }
    IEnumerator LoadSceneProcess(){
        AsyncOperation op = SceneManager.LoadSceneAsync(nextScene);
        op.allowSceneActivation = false;

        float timer = 0f;
        while(!op.isDone){
            yield return null;
            playerPos.position = Camera.main.ScreenToWorldPoint(new Vector3(1473.5f-2947f*progressBar.fillAmount,0f,0f));
            if(op.progress < 0.9f){
                progressBar.fillAmount = op.progress;
            }
            else{
                timer += Time.unscaledDeltaTime;
                progressBar.fillAmount = Mathf.Lerp(0.9f, 1f, timer/3f);
                
                if(progressBar.fillAmount >= 1f){
                    op.allowSceneActivation = true;
                    yield break;
                }
            }
        }
    }
}
