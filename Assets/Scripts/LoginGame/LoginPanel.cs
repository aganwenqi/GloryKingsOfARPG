using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
public class LoginPanel :MonoBehaviour
{
    public static bool Admin = false;
    private InputField usernameIF;
    private InputField passwordIF;
    private MessagePanel msgPanel;
    public UnityEvent InJoinGame;

    /*audioSource 的sid*/
    private string _sourceSid;
    private string _bgSourceSid;
    private void Awake()
    {
        usernameIF = transform.Find("UserNameLabel/UserNameInput").GetComponent<InputField>();
        passwordIF = transform.Find("PasswordLabel/PasswordInput").GetComponent<InputField>();
        msgPanel = transform.Find("MessagePanel").GetComponent<MessagePanel>();

        transform.Find("LoginButton").GetComponent<Button>().onClick.AddListener(OnLoginClick);
        _sourceSid = SerialIdManager.Instance.GetSid();
        _bgSourceSid = SerialIdManager.Instance.GetSid();
        MusicManager.Instance.AddAudioSource(_sourceSid, gameObject.AddComponent<AudioSource>());
        MusicManager.Instance.AddAudioSource(_bgSourceSid, gameObject.AddComponent<AudioSource>(), SourceType.bg);
        MusicUtil.PlayClip(_bgSourceSid, SourceClip.loginBg, true, SourceType.bg);
    }
    //登录
    private void OnLoginClick()
    {
        Clip clip = MusicUtil.GetSourceName(SourceClip.normlBt);
        string msg = "";
        do
        {
            if (string.IsNullOrEmpty(usernameIF.text))
            {
                msg += "用户名不能为空 ";
            }
            if (string.IsNullOrEmpty(passwordIF.text))
            {
                msg += "密码不能为空 ";
            }
            if (msg != "")
            {
                msgPanel.ShowMessage(msg);
                break;
            }
            if (Verify(usernameIF.text, passwordIF.text))
            {
                msg += "登陆成功";
                msgPanel.ShowMessage(msg);
                Invoke("InJoin", 0.5f);
                /*播放音效*/
                MusicUtil.PlayClip(_sourceSid, SourceClip.inJoinBt);
                return;
            }
            else
            {
                msg += "账户或密码错误 ";

            }
            msgPanel.ShowMessage(msg);
        } while (false);
        /*播放音效*/
        MusicUtil.PlayClip(_sourceSid, SourceClip.normlBt);
    }
    //验证密码
    public bool Verify(string user, string passd)
    {
        if (user == "admin" && passd == "123456")
        {
            Admin = true;
            return true;
        }
        else if(user == "user" && passd == "123456")
        {
            Admin = false;
            return true;
        }
        return false;
    }

    //进入游戏
    private void InJoin()
    {
        if (InJoinGame != null)
            InJoinGame.Invoke();
    }
}
