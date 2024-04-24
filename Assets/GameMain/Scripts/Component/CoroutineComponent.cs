using System.Collections;
using UnityEngine;
using UnityGameFramework.Runtime;

/// <summary>
/// 协程控制组件 - 专门用于协程控制
/// </summary>
public class CoroutineComponent : GameFrameworkComponent
{
    IEnumerator WaitForSeconds(int seconds)
    {
        yield return new WaitForSeconds(seconds);
    }
}