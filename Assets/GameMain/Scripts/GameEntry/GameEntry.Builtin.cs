using UnityEngine;
using UnityGameFramework.Runtime;

/// <summary>
/// 游戏入口 - 框架原生组件
/// </summary>
public partial class GameEntry : MonoBehaviour
{
    /// <summary>
    /// 获取游戏基础组件。
    /// </summary>
    public static BaseComponent Base { get; private set; }

    /// <summary>
    /// 获取调试组件。
    /// </summary>
    public static DebuggerComponent Debugger { get; private set; }

    /// <summary>
    /// 获取配置组件。
    /// </summary>
    public static ConfigComponent Config { get; private set; }

    /// <summary>
    /// 获取实体组件。
    /// </summary>
    public static EntityComponent Entity { get; private set; }

    /// <summary>
    /// 获取事件组件。
    /// </summary>
    public static EventComponent Event { get; private set; }

    /// <summary>
    /// 获取有限状态机组件。
    /// </summary>
    public static FsmComponent Fsm { get; private set; }

    /// <summary>
    /// 获取对象池组件。
    /// </summary>
    public static ObjectPoolComponent ObjectPool { get; private set; }

    /// <summary>
    /// 获取流程组件。
    /// </summary>
    public static ProcedureComponent Procedure { get; private set; }

    /// <summary>
    /// 获取场景组件。
    /// </summary>
    public static SceneComponent Scene { get; private set; }

    /// <summary>
    /// 获取数据表组件。
    /// </summary>
    public static DataTableComponent DataTable { get; private set; }

    private static void InitBuiltinComponents()
    {
        Base = UnityGameFramework.Runtime.GameEntry.GetComponent<BaseComponent>();
        Debugger = UnityGameFramework.Runtime.GameEntry.GetComponent<DebuggerComponent>();
        Config = UnityGameFramework.Runtime.GameEntry.GetComponent<ConfigComponent>();
        Entity = UnityGameFramework.Runtime.GameEntry.GetComponent<EntityComponent>();
        Event = UnityGameFramework.Runtime.GameEntry.GetComponent<EventComponent>();
        Fsm = UnityGameFramework.Runtime.GameEntry.GetComponent<FsmComponent>();
        ObjectPool = UnityGameFramework.Runtime.GameEntry.GetComponent<ObjectPoolComponent>();
        Procedure = UnityGameFramework.Runtime.GameEntry.GetComponent<ProcedureComponent>();
        Scene = UnityGameFramework.Runtime.GameEntry.GetComponent<SceneComponent>();
        DataTable = UnityGameFramework.Runtime.GameEntry.GetComponent<DataTableComponent>();
    }
}