using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTransform : MonoBehaviour
{
    public SceneLoadEvent loadEvent;
    public GameScene targetScene;

    public void Trans()
    {
        loadEvent.LoadRequestEvent(targetScene,  true);
    }
}
