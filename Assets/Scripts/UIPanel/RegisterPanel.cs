using Common;
using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RegisterPanel : BasePanel
{

    private Button closeButton;
    private Button RegisterButton;
    private InputField usernameIF;
    private InputField passwordIF;
    private InputField rePasswordIF;
    //private RegisterRequest registerRequest;

    //由于UIFramework设计的原因，OnEnter是先于Start执行的
    public override void OnEnter()
    {
        gameObject.SetActive(true);
        transform.localScale = Vector3.zero;
        transform.DOScale(1, 0.4f);
        transform.localPosition = new Vector3(1000, 0, 0);
        transform.DOLocalMoveX(0, 0.4f);
        if (closeButton == null)
            closeButton = transform.Find("CloseButton").GetComponent<Button>();
    }

    internal void OnRegisterResponse(ReturnCode returnCode)
    {
        if (returnCode == ReturnCode.Success)
        {
            // TODO
            //uiMng.PushPanelSync(UIPanelType.RoomList);//这个如果不在主线程中调用会报错，Start Awake都属于主线程。
            uiMng.ShowMessageSync("注册成功");
        }
        else
        {
            uiMng.ShowMessageSync("用户名或者密码重复，注册失败");
        }
    }

    public override void OnExit()
    {
        gameObject.SetActive(false);
    }

    void Start()
    {
        //registerRequest = GetComponent<RegisterRequest>();

        usernameIF = transform.Find("UsernameLabel/UsernameInput").GetComponent<InputField>();
        passwordIF = transform.Find("PasswordLabel/PasswordInput").GetComponent<InputField>();
        rePasswordIF = transform.Find("RePasswordLabel/RePasswordInput").GetComponent<InputField>();
        closeButton = transform.Find("CloseButton").GetComponent<Button>();

        closeButton.onClick.AddListener(OnCloseClick);
        transform.Find("RegisterButton").GetComponent<Button>().onClick.AddListener(OnRegisterClick);

    }

    private void OnRegisterClick()
    {
        //根据InputField的字符串，向服务器端发送注册的请求
        string msg = "";
        if (string.IsNullOrEmpty(usernameIF.text))
        {
            msg += "用户名不能为空\n";
        }
        if (string.IsNullOrEmpty(passwordIF.text))
        {
            msg += "密码不能为空\n";
        }
        if (rePasswordIF.text != passwordIF.text)
        {
            msg += "重复密码不一致";
        }
        if (msg != "")
        {
            uiMng.ShowMessage(msg); return;
        }
        // registerRequest.SendRequest(usernameIF.text, passwordIF.text);//用户名和密码已经发送到服务器端，进行校验
    }

    private void OnCloseClick()
    {
        transform.localScale = Vector3.one;
        transform.localPosition = new Vector3(0, 0, 0);
        transform.DOScale(0, 0.1f);
        transform.DOLocalMoveX(1000, 0.1f).OnComplete(() => uiMng.PopPanel());
    }
}
