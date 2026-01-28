using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

[ExecuteInEditMode]
public class SceneReferenceChecker : MonoBehaviour
{
    public GameScene gameSceneToCheck;

    void OnEnable()
    {
        if (gameSceneToCheck == null) return;

        CheckReference(gameSceneToCheck);
    }

    public static void CheckReference(GameScene gameScene)
    {
        if (gameScene == null)
        {
            Debug.LogError("GameScene 为空");
            return;
        }

        Debug.Log($"=== 检查 GameScene: {gameScene.name} ===");

        if (gameScene.sceneReference == null)
        {
            Debug.LogError("sceneReference 为空！");
            return;
        }

        Debug.Log($"AssetGUID: {gameScene.sceneReference.AssetGUID}");
        Debug.Log($"RuntimeKey: {gameScene.sceneReference.RuntimeKey}");

#if UNITY_EDITOR
        // 获取实际资产
        string assetPath = AssetDatabase.GUIDToAssetPath(gameScene.sceneReference.AssetGUID);
        if (!string.IsNullOrEmpty(assetPath))
        {
            Debug.Log($"资产路径: {assetPath}");

            Object asset = AssetDatabase.LoadMainAssetAtPath(assetPath);
            if (asset != null)
            {
                Debug.Log($"资产类型: {asset.GetType()}");
                Debug.Log($"资产名称: {asset.name}");

                // 检查是否是场景
                if (AssetDatabase.GetMainAssetTypeAtPath(assetPath) == typeof(SceneAsset))
                {
                    Debug.Log("? 正确：引用的是场景文件 (.unity)");
                }
                else
                {
                    Debug.LogError($"? 错误：引用的不是场景文件！");
                    Debug.LogError($"文件扩展名: {System.IO.Path.GetExtension(assetPath)}");

                    // 列出该路径下的所有资产
                    Object[] allAssets = AssetDatabase.LoadAllAssetsAtPath(assetPath);
                    foreach (var a in allAssets)
                    {
                        Debug.Log($"   - {a.name} ({a.GetType()})");
                    }
                }
            }
        }
        else
        {
            Debug.LogError($"找不到 GUID 对应的资产: {gameScene.sceneReference.AssetGUID}");
            Debug.Log("可能是 Addressables 中的资产，需要在运行时检查");
        }
#endif

        Debug.Log($"=== 检查结束 ===");
    }
}