using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SettingUI : BaseUI {
    static public SettingUI _instance;
    static public SettingUI Instance
    {
        get
        {
            return _instance;
        }
    }

    public Button quitDup;//退出副本按钮
    void Awake()
	{
        _instance = this;
	}

	public override void Init(Character character)
	{
        base.Init(character);
        
    }
    public override void InitData()
    {
        base.InitData();
        quitDup.gameObject.SetActive(DupManager.Instance.CurDupId == 1000 ? false : true);
    }
    public override void OnEnter()
    {
        base.OnEnter();
        this.Character.enabled = false;
        //_infoPanelControl.ChangePanel(InfoBtType.chapter1);
    }
    public override void OnQuit()
    {
        this.Character.enabled = true;
        base.OnQuit();
    }
}
