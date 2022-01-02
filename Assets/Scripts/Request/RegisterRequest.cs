using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegisterRequest : BaseRequest {

    private RegisterPanel reqisterPanel;

    public override void Awake()
    {
        reqisterPanel = GetComponent<RegisterPanel>();
        requestCode = RequestCode.User;
        actionCode = ActionCode.Register;
        base.Awake();// 父类的Awake方法把面板实例添加到了RequestManager的字典中
    }
    public override void OnResponse(string data)
    {
        ReturnCode returnCode = (ReturnCode)int.Parse(data);
        reqisterPanel.OnRegisterResponse(returnCode);
    }
}
