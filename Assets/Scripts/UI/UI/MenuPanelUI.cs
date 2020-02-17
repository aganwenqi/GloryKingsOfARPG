using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MenuPanelUI : BaseUI {
    static public MenuPanelUI _instance;
    public MenuPanel MenuPanel;
    static public MenuPanelUI Instance
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
        MenuPanel.Init(this);
    }
}
