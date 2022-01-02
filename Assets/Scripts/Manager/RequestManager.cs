using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
public class RequestManager : BaseManager
{
    // BaseManager的构造函数就是带有GameFacade形参的。然后RequestManager的构造函数继承父类BaseManager的构造函数。
    public RequestManager(GameFacade facade) : base(facade) { }

    //通过服务器端回传的ActionCode来区分Request
    private Dictionary<ActionCode, BaseRequest> requsetDict = new Dictionary<ActionCode, BaseRequest>();

    /// <summary>
    /// 添加actionCode-request对进入字典
    /// </summary>
    /// <param name="actionCode">actionCode的键</param>
    /// <param name="request">request的值</param>
    public void AddRequest(ActionCode actionCode, BaseRequest request)
    {
        requsetDict.Add(actionCode, request);
    }
    /// <summary>
    /// 移除字典中的actionCode-request对
    /// </summary>
    /// <param name="actionCode"></param>
    public void RemoveRequest(ActionCode actionCode)
    {
        requsetDict.Remove(actionCode);
    }
    /// <summary>
    /// 处理回应
    /// </summary>
    /// <param name="actionCode">根据actionCode找Request</param>
    /// <param name="data">数据</param>
    public void HandleResponse(ActionCode actionCode, string data)
    {
        BaseRequest request = requsetDict.TryGet<ActionCode, BaseRequest>(actionCode);
        if (request == null)
        {
            Debug.LogWarning("ActionCode[" + actionCode + "]对应的Request类"); return;
        }
        request.OnResponse(data);
    }
}
