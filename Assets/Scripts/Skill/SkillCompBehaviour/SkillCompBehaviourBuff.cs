using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 范围技能攻击
/// </summary>
public class SkillCompBehaviourBuff : SkillCompBehaviour {

    private int _num = 10;
    //维护当前加了多少buf
    private List<float> _buffValue = new List<float>();

    private float _buffCD;
    public override void Init(SkillSyncData manager, int skillCompId)
    {
        base.Init(manager, skillCompId);
        for(int i = 0; i < _num; i++)
        {
            _buffValue.Insert(i, 0);
        }
        _buffCD = 0;
    }
    public override void InitData()
    {
        base.InitData();
        _buffCD = 0;
    }
    public override void Trigger()
    {
        base.Trigger();
    }
    public override void Update(float _timing)
    {
        base.Update(_timing);

        if (_buffCD <= 0)
            return;

        _buffCD -= _timing;
        if (_buffCD <= 0)
        {
            OnEndBuff();
        }    
    }
    //开始加buff
    public override void Effect()
    {
        base.Effect();
        
        AttributesControl attControl = _manager.Character.CharacterAttribute.AttControl;
        SkillCompHurt skillData = SkillCompHurt.FindById(_skillCompData.HurtAdd);
        List<float> objAtt = attControl.GetAllAttByArrary();
        _buffCD = _skillCompData.Param2;

        _buffValue[(int)AttributeType.PhyHurt] = skillData.Phy * _skillCompData.Param1;
        _buffValue[(int)AttributeType.MagicHurt] = skillData.Magic * _skillCompData.Param1;
        _buffValue[(int)AttributeType.Power] = skillData.PowerAdd * _skillCompData.Param1;
        _buffValue[(int)AttributeType.Spell] = skillData.SpellAdd * _skillCompData.Param1;
        _buffValue[(int)AttributeType.Speed] = skillData.MSpeedAdd * _skillCompData.Param1;
        _buffValue[(int)AttributeType.Hp] = skillData.HpAdd * _skillCompData.Param1;
        _buffValue[(int)AttributeType.Mp] = skillData.MpAdd * _skillCompData.Param1;
        _buffValue[(int)AttributeType.Crit] = skillData.CritAdd * _skillCompData.Param1;
        _buffValue[(int)AttributeType.PhyDefend] = skillData.PhyDefendAdd * _skillCompData.Param1;
        _buffValue[(int)AttributeType.MagicDefend] = skillData.MagicDefendAdd * _skillCompData.Param1;
        for(int i = 0; i < _num; i++)
        {
            AttributeType type = (AttributeType)i;
            if (type == AttributeType.Hp || type == AttributeType.Mp)
            {
                continue;
            }
            objAtt[i] += _buffValue[i];
        }
        attControl.ChangeAttSignalAdd(AttributeType.Hp, _buffValue[(int)AttributeType.Hp], false);
        attControl.ChangeAttSignalAdd(AttributeType.Mp, _buffValue[(int)AttributeType.Mp], false);
        attControl.UpdataState();

        /*飘字*/
        _manager.Character.CharacterFly.PlayFlyFont((int)_buffValue[(int)AttributeType.Hp], CharacterFlyFont.FlyType.Green);
        _manager.Character.CharacterFly.PlayFlyFont((int)_buffValue[(int)AttributeType.Mp], CharacterFlyFont.FlyType.Green);
        _manager.Character.CharacterFly.PlayFlyFont((int)_buffValue[(int)AttributeType.PhyHurt], CharacterFlyFont.FlyType.Green);
        _manager.Character.CharacterFly.PlayFlyFont((int)_buffValue[(int)AttributeType.MagicHurt], CharacterFlyFont.FlyType.Green);
        _manager.Character.CharacterFly.PlayFlyFont((int)_buffValue[(int)AttributeType.Crit], CharacterFlyFont.FlyType.Green);
    }

    //buff时间结束
    private void OnEndBuff()
    {
        AttributesControl attControl = _manager.Character.CharacterAttribute.AttControl;
        List<float> objAtt = attControl.GetAllAttByArrary();
        for (int i = 0; i < _num; i++)
        {
            AttributeType type = (AttributeType)i;
            if(type == AttributeType.Hp || type == AttributeType.Mp)
            {
                continue;
            }
            objAtt[i] -= _buffValue[i];
        }
        this.OnEndFx();
        _buffCD = 0;
        attControl.UpdataState();
        if(_manager.Character.StateControl.CurState != null && _manager.Character.StateControl.CurState.StateType == StateType.Run)
        {
            (_manager.Character.StateControl.CurState as StateRun).SetSpeed(objAtt[(int)AttributeType.Speed]);
        }
    }
}
