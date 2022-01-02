using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessagePanel : BasePanel
{
    private Text text;
    private float showTime = 1;         //显示时间为1s
    string message = null;

    void Update()
    {
        if (message != null)
        {
            ShowMessage(message);
            message = null;
        }
    }
    public override void OnEnter()
    {
        base.OnEnter();
        text = GetComponent<Text>();
        text.enabled = false;
        uiMng.InjectMsgPanel(this);     //当MessagePanel被创建出来的时候，把自身注入给UIManager

    }
    public void ShowMessage(string msg)
    {
        text.CrossFadeAlpha(1, 0.1f, true);         //注意：是通过控制CanvasRenderer。alpha为1才能显示出信息
        text.enabled = true;                        //将内容显示
        text.text = msg;                            //设置要显示的信息
        Invoke("Hide", showTime);                   //添加计时器，1s之后开始调用Hide()
    }
    public void ShowMessageSync(string msg)
    {
        message = msg;
    }
    private void Hide()
    {
        text.CrossFadeAlpha(0, 1, true);    //注意：是通过控制CanvasRenderer的内容。alpha值1s后变成0，true是忽略timeScale
    }
}
