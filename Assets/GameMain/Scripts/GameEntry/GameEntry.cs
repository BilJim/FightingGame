using UnityEngine;

/// <summary>
/// 游戏入口
/// </summary>
public partial class GameEntry : MonoBehaviour
{
    private void Start()
    {
        //初始化基础组件
        InitBuiltinComponents();
        //初始化自定义组件
        InitCustomComponents();
    }
}