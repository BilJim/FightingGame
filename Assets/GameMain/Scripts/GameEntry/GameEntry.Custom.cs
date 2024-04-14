using UnityEngine;

/// <summary>
/// 游戏入口 - 自定义组件
/// </summary>
public partial class GameEntry : MonoBehaviour
{
    public static InputControlComponent InputControl { get; private set; }

    private static void InitCustomComponents()
    {
        InputControl = UnityGameFramework.Runtime.GameEntry.GetComponent<InputControlComponent>();
    }
}