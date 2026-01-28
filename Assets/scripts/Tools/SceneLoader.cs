using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [Header("事件监听")]
    public SceneLoadEvent loadEvent;
    public GameScene firstloadscene;
    
    public VoidEvent Level1;

    [Header("事件广播")]
    //public SceneTransition transition;
    public VoidEvent afterSceneLoaded;

    private GameScene currentLoadedScene;
    private GameScene sceneToLoad;
    //private Vector3 positionToGo;
    //private bool sceneTransition;
    //private bool loading = true;
    private bool loading;
    private bool isFirstScene = true;
    //public float fadeTime;
     
    //public float fadeTimeIn;
    private void Awake()
    {
        //Addressables.LoadSceneAsync(firstloadscene.sceneReference, LoadSceneMode.Additive);
        //currentLoadedScene = firstloadscene;
        //currentLoadedScene.sceneReference.LoadSceneAsync(LoadSceneMode.Additive);
    }

    private void Start()
    {
        Level();
    }
    private void OnEnable()
    {
        loadEvent.LoadRequestEvent += OnLoadRequestEvent;
        //Level1.OnEventRaised += Level;
    }

    private void OnDisable()
    {
        loadEvent.LoadRequestEvent -= OnLoadRequestEvent;
        //Level1.OnEventRaised -= Level;
    }

    private void Level()
    {
        
        sceneToLoad = firstloadscene;
        OnLoadRequestEvent(sceneToLoad, false);
    }


    private void OnLoadRequestEvent(GameScene location,  bool fadeScreen)
    {
        
        if (loading)
        {
            return;
        }
        sceneToLoad = location;
        //positionToGo = posToGo;
        //this.sceneTransition = fadeScreen;

        if (currentLoadedScene != null)
        {
            Debug.Log("xxx");
            StartCoroutine(UnloadPreviousScene());
        }
        else
        {
            isFirstScene = false;
            LoadNewScene();
        }
        Debug.Log(sceneToLoad.sceneReference.SubObjectName);

    }

    private IEnumerator UnloadPreviousScene()
    {
        //if (sceneTransition)
        //{
        //    transition.SceneTransIn(fadeTime);
        //}
        //yield return new WaitForSeconds(fadeTime);
        yield return currentLoadedScene.sceneReference.UnLoadScene();
        
        LoadNewScene();
        
    }

    private void LoadNewScene()
    {
        
        var loadingOption = sceneToLoad.sceneReference.LoadSceneAsync(LoadSceneMode.Additive, true);
        loadingOption.Completed += OnLoadCompleted;
    }

    private void OnLoadCompleted(AsyncOperationHandle<SceneInstance> obj)
    {
        currentLoadedScene = sceneToLoad;
        //if (sceneTransition)
        //{
        //    transition.SceneTransOut(fadeTime);
        //}

        loading = false;


        //呼叫场景加载内容
        afterSceneLoaded.RaiseEvent();
    }
}