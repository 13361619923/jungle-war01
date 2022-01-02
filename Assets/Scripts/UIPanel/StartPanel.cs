using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class StartPanel : BasePanel
{
    Button loginButton;
    Animator btnAnimator;

    public override void OnEnter()//OnEnter先于Start被调用
    {
        if (loginButton == null)
            loginButton = transform.Find("LoginButton").GetComponent<Button>();
        if (btnAnimator == null)
            btnAnimator = loginButton.GetComponent<Animator>();

        //dotween按钮动画播放完了之后，再启动按钮的动画状态机
        loginButton.transform.DOScale(1, 0.3f).OnComplete(() => btnAnimator.enabled = true);
    }
    void Start()
    {
        loginButton = transform.Find("LoginButton").GetComponent<Button>();
        loginButton.onClick.AddListener(OnLoginClick);
        btnAnimator = loginButton.GetComponent<Animator>();
    }
    public override void OnPause()
    {
        //当有更新的面板加载出来后，此面板按钮的动画状态机关闭
        btnAnimator.enabled = false;
        //此面板按钮的动画状态机关闭后，再播放dotween动画
        loginButton.transform.DOScale(0, 0.4f).OnComplete(() => this.gameObject.SetActive(false));
    }
    public override void OnResume()
    {
        this.gameObject.SetActive(true);
        //因为OnPause()关闭了按钮的动画状态机，所以在dotween按钮动画播放完了之后，需要重新打开
        loginButton.transform.DOScale(1, 0.3f).OnComplete(() => btnAnimator.enabled = true);
    }

    //以下方法不会被调用，startPanel没有调用PopPanel()方法的入口
    public override void OnExit()
    {
    }

    /// <summary>
    /// 登录按钮的回调函数
    /// </summary>
    private void OnLoginClick()
    {
        uiMng.PushPanel(UIPanelType.Login);         //加载登录面板
    }
}
