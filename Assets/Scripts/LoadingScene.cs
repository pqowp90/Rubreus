using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingScene : MonoBehaviour
{
    [SerializeField]
    private Image image;
    [SerializeField]
    private Sprite[] sprites;
    private Vector2 screenSize;
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
        image.sprite = sprites[Random.Range(0,sprites.Length)];
        screenSize.y = Camera.main.orthographicSize;
        screenSize.x = screenSize.y*Camera.main.aspect;
        StartCoroutine(LoadSceneProcess());
    }
    IEnumerator LoadSceneProcess(){
        AsyncOperation op = SceneManager.LoadSceneAsync(nextScene);
        op.allowSceneActivation = false;

        float timer = 0f;
        while(!op.isDone){
            yield return null;
            playerPos.position = (new Vector3((-screenSize.x)+screenSize.x*2f*progressBar.fillAmount,0f,10f));
            if(op.progress < 0.9f){
                progressBar.fillAmount = op.progress;
            }
            else{
                timer += Time.unscaledDeltaTime;
                progressBar.fillAmount = Mathf.Lerp(0.9f, 1f, timer/2f);
                
                if(progressBar.fillAmount >= 1f){
                    op.allowSceneActivation = true;
                    yield break;
                }
            }
        }
    }
}
