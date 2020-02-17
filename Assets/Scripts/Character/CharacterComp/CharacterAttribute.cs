using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AttackTarget
{
    /*攻击目标*/
    Player = 1,
    Monster,
    All,
}
/// <summary>
/// 角色的属性组件
/// </summary>
public class CharacterAttribute : CharacterCompBase {

    AttributesControl _attControl = new AttributesControl();

    public override void Init (Character character)
	{
		base.Init (character);
        _attControl.Init(character);   
    }
    public override void InitData()
    {
        base.InitData();
        _attControl.InitData();
        MenuPanelUI.Instance.MenuPanel.UpdataHpMp();//更新玩家hpmpUI
    }
    public override void Update(float _timing)
    {
        base.Update(_timing);
    }
    public AttributesControl AttControl
    {
        get
        {
            return _attControl;
        }

        set
        {
            _attControl = value;
        }
    }

    
}