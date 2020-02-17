using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICanvasManager : MonoBehaviour {
    public static UICanvasManager Instance;
    private Character _character;
    public List<BaseUI> baseUI;
    public Camera UICamera2D;
    public Camera UICamera3D;

    /*UI按钮的audioSource*/
    private string _sourceSid;

    void Awake()
    {
        Instance = this;
    }
    public void Init(Character character)
    {
        this._character = character;
        /*
        BattleUI.Instance.Init(character);
        InformationUI.Instance.Init(character);
        DupSwitchUI.Instance.Init(character);
        DupResultUI.Instance.Init(character);
        MenuPanelUI.Instance.Init(character);
        SettingUI.Instance.Init(character);*/
        foreach(BaseUI item in baseUI)
        {
            item.Init(character);
        }
        _sourceSid = SerialIdManager.Instance.GetSid();
        gameObject.AddComponent<AudioSource>();
    }
    public void InitData()
    {
        MusicManager.Instance.AddAudioSource(_sourceSid, this.GetComponent<AudioSource>());
        foreach (BaseUI item in baseUI)
        {
            item.InitData();
        }
    }
    //显示多个组ui
    public void OnEnterUI(List<BaseUI> ui)
    {
        foreach (BaseUI item in baseUI)
        {
            if (item.IsEnter)
            {
                item.OnQuit();
            }
        }

        foreach (BaseUI item in ui)
        {
            if(baseUI.Contains(item))
            {
                item.OnEnter();
            }
        }
    }
    //显示一个组ui
    public void OnEnterUI(BaseUI ui)
    {
        foreach (BaseUI item in baseUI)
        {
            if (item == ui)
            {
                if (!item.IsEnter)
                    item.OnEnter();
            }
            else if(item.IsEnter)
            {
                item.OnQuit();
            }
        }
    }
    #region 对象属性
    public string SourceSid
    {
        get
        {
            return _sourceSid;
        }

        set
        {
            _sourceSid = value;
        }
    }
    #endregion
}
