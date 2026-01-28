using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;

public class LevelChooser : MonoBehaviour
{
    [Header("事件监听")]
    public SceneLoadEvent loadEvent;
    [Header("事件广播")]
    public VoidEvent intoTheLevel;

    [Header("关卡参数")]
    public GameScene levelToGo;
    //public Animator transition;

    private void OnEnable()
    {
        intoTheLevel.OnEventRaised += IntoTheGame;
        //intoTheLevel.OnEventRaised += 
    }

    private void OnDisable()
    {
        intoTheLevel.OnEventRaised -= IntoTheGame;
    }

    public void IntoTheGame()
    {
        //Debug.Log(levelToGo);
        if (loadEvent == null)
        {
            Debug.LogError("loadEvent is not assigned in LevelChooser!", this);
            return;
        }

        if (levelToGo == null)
        {
            Debug.LogError("levelToGo is not assigned in LevelChooser!", this);
            return;
        }
        //loadEvent.LoadRequestEvent(levelToGo, false);
        loadEvent.RaiseLoadRequestEvent(levelToGo, false);
    }
}
