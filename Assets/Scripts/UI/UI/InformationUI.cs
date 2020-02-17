using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum InfoBtType
{
   
    quit = 0,
    attribute = 1,
    quitDup = 2,//强制退出副本
    
    chapter1 = 101,
    chapter2 = 102,
    chapter3 = 103,

    settingFirst = 1000,//设置首页

    quitGame = 2000,//退出游戏
    selectHero = 2001,//选择英雄
}
public class InformationUI : BaseUI {

	public static InformationUI Instance;
    
    private Transform _showMode;//展示的对象

	void Awake()
	{
		Instance = this;
	}


	public override void Init(Character character)
	{
        base.Init(character);
        Character = character;
        

        string name = Character.HeroData.ShowModel;
        object obj = Resources.Load(Utils.CharacterPath_Shower + name);
        if(obj == null)
        {
            Debug.LogError("没有展示模型");
            return;
        }
        GameObject model = GameObject.Instantiate(obj as GameObject);
        model.transform.parent = root2.transform;
        _showMode = model.GetComponentInChildren<Animator>().transform;
    }
    /*设置角色面板所显示的属性*/
    public void SetAttributes(List<float> att)
    {
        InfoPanelBase panel = null;
       if(_infoPanelControl.InfoPanels.TryGetValue(InfoBtType.attribute, out panel))
        {
            (panel as AttributePanel).SetAttribute(att);
            //MenuPanelUI.Instance.MenuPanel.UpdataHpMp();
        }
    }
    /*打开面板*/
    public override void OpenPanel(InfoBtType type)
    {
        base.OpenPanel(type);
        _showMode.localPosition = Vector3.zero;
        _showMode.localEulerAngles = Vector3.zero;
    }
    /*切换信息面板*/
    public override void ChangePanel(InfoBtType type)
    {
        base.ChangePanel(type);
        _infoPanelControl.ChangePanel(type);
    }
}
