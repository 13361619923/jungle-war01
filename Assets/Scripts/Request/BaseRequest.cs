using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

//request是需要挂载在游戏物体上的
public class BaseRequest : MonoBehaviour
{
    protected RequestCode requestCode = RequestCode.None;
    protected ActionCode actionCode = ActionCode.None;
    protected GameFacade facade;

    /// <summary>
    /// 在子类中重写，重新指定自己的RequestCode和ActionCode
    /// </summary>
    public virtual void Awake()
    {
        GameFacade.Instance.AddRequest(actionCode, this);
        facade = GameFacade.Instance;
    }

    /// <summary>
    /// 父类方法减少代码量
    /// </summary>
    /// <param name="data"></param>
    protected void SendRequest(string data)
    {
        facade.SendRequest(requestCode, actionCode, data);
    }

    /// <summary>
    /// 在子类中重写的回应服务器端回传数据的虚方法
    /// </summary>
    /// <param name="data">服务器端回传数据</param>
    public virtual void OnResponse(string data) { }

    /// <summary>
    /// 用于销毁游戏物体的虚方法
    /// </summary>
    public virtual void OnDestroy()
    {
        GameFacade.Instance.RemoveRequest(actionCode);
    }
}
