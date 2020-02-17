using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DupSwitchUI : BaseUI {
    static public DupSwitchUI _instance;
    static public DupSwitchUI Instance
    {
        get
        {
            return _instance;
        }
    }
    void Awake()
	{
        _instance = this;
	}

	public override void Init(Character character)
	{
        base.Init(character);
        
    }
    public override void OnEnter()
    {
        base.OnEnter();
        this.Character.enabled = false;
        _infoPanelControl.ChangePanel(InfoBtType.chapter1);
    }
    public override void OnQuit()
    {
        this.Character.enabled = true;
        base.OnQuit();
    }
}
