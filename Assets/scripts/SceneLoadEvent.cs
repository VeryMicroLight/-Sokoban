using System.Drawing.Printing;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Events/SceneLoadEvent")]
public class SceneLoadEvent : ScriptableObject
{
    public UnityAction<GameScene, bool> LoadRequestEvent;
    public void RaiseLoadRequestEvent(GameScene location, bool fadeScreen)
    {
        if (LoadRequestEvent == null)
        {
            return;
        }
        LoadRequestEvent?.Invoke(location, fadeScreen);
    }
}
