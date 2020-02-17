using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

/// <summary>
/// 角色飘字
/// </summary>
public class CharacterFlyFont : CharacterCompBase {
    public enum FlyType
    {
        None,
        Red,
        Green
    };
    private ShootTextProController shootTextProController;
    private StringBuilder _flyValue = new StringBuilder();

    /*ui刷新间隔*/
    private float uiUpdataTime = 0;
    //预防GC
    private string _add = "+";
    private string _dec = "-";
    public override void Init (Character character)
	{
		base.Init (character);
        shootTextProController = _character.GetComponent<ShootTextProController>();
        uiUpdataTime = 0;
    }
    public override void InitData()
    {
        base.InitData();
    }
    /*附带暴击飘字*/
    public void PlayFlyFont(int value, FlyType type, int crit)
    {
        if(crit == 0)
        {
            PlayFlyFont(value, type, false);
        }
        else
        {
            PlayFlyFont(value, type, true);
        }
        
    }
    /*附带暴击飘字*/
    public void PlayFlyFont(int value, FlyType type, bool crit)
    {
        PlayFlyFont(value, type);
        if(crit)
        {
            PlayFlyFont("crit", type);
        }
    }

    /*飘字*/
    public void PlayFlyFont(int value, FlyType type)
    {
        if (_character.CharacterUtilData.characterType == CharacterType.Monster)
        {
            //设置怪物头上血条
            AttributesControl attControl = _character.CharacterAttribute.AttControl;
            float hp = attControl.GetAttSignal(AttributeType.Hp);
            float maxHp = attControl.GetAttSignal(AttributeType.MaxHp);
            (_character as Monster).HpBar.RefreshHpGauge(hp / maxHp);
        }
        else if(_character.CharacterUtilData.characterType == CharacterType.Player)
        {
            MenuPanelUI.Instance.MenuPanel.UpdataHpMp();//更新玩家hp
        }
        
        _flyValue.Remove(0, _flyValue.Length);
        if (type == FlyType.Red)
        {
            shootTextProController.DelayMoveTime = 0.4f;
            shootTextProController.textAnimationType = TextAnimationType.Burst;
        }
        else if(type == FlyType.Green)
        {
            shootTextProController.DelayMoveTime = 0.0f;
            shootTextProController.textAnimationType = TextAnimationType.Normal;
            _flyValue.Append(_add);
        }
        _flyValue.Append(value);
        shootTextProController.CreatShootText(_flyValue.ToString(), _character.transform);
    }
    public void PlayFlyFont(string value, FlyType type)
    {
        if (type == FlyType.Red)
        {
            shootTextProController.DelayMoveTime = 0.4f;
            shootTextProController.textAnimationType = TextAnimationType.Burst;
        }
        else if (type == FlyType.Green)
        {
            shootTextProController.DelayMoveTime = 0.0f;
            shootTextProController.textAnimationType = TextAnimationType.Normal;
        }
        shootTextProController.CreatShootText(value, _character.transform);
    }
    public override void Update(float _timing)
    {
        base.Update(_timing);
        uiUpdataTime += _timing;
        if(uiUpdataTime > 0.1)
        {
            uiUpdataTime = 0;
            //MenuPanelUI.Instance.MenuPanel.UpdataHpMp();
        } 
    }
}