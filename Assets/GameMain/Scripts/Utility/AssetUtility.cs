public static class AssetUtility
{
    //加载预制体路径
    public static string GetEntityAsset(string assetName)
    {
        return $"Assets/GameMain/Entities/{assetName}.prefab";
    }
    
    //加载场景路径
    public static string GetSceneAsset(string assetName)
    {
        return $"Assets/GameMain/Scenes/{assetName}.unity";
    }
}