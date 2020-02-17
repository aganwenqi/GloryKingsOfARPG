using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttributesControl
{
    private Character _character;
    /*玩家属性*/
    private Attributes _attributes = new Attributes();

    public void Init(Character character)
    {
        this._character = character;
        _attributes.Init(character);
        BaseAttribute att = BaseAttribute.FindById(character.HeroData.Affx);
        Dictionary<AttributeType, float> attr = _character.AttributePool.Spawn();
        attr.Add(AttributeType.Speed, att.Speed);
        attr.Add(AttributeType.PhyHurt, att.PhyHurt);
        attr.Add(AttributeType.MagicHurt, att.MagicHurt);
        attr.Add(AttributeType.PhyDefend, att.PhyDefend);
        attr.Add(AttributeType.MagicDefend, att.MagicDefend);
        attr.Add(AttributeType.Power, att.Power);
        attr.Add(AttributeType.Spell, att.Spell);
        attr.Add(AttributeType.Crit, att.Crit);
        attr.Add(AttributeType.Hp, att.Hp);
        attr.Add(AttributeType.Mp, att.Mp);

        ChangeAttSignalAdd(AttributeType.MaxHp, att.Hp, false);
        ChangeAttSignalAdd(AttributeType.MaxMp, att.Mp, false);
        ChangeAttAdd(attr);
    }
    /*属性加,单个属性,最后一个属性是否更新ui信息*/
    public float ChangeAttSignalAdd(AttributeType type, float value, bool updataUi = true)
    {
        return _attributes.ChangeAttSignalAdd(type, value, updataUi);
    }
    /*属性加,会自动回收对象*/
    public void ChangeAttAdd(Dictionary<AttributeType, float> attr)
    {
        _attributes.ChangeAttAdd(attr);
    }
    /*属性乘,会自动回收对象*/
    public void ChangeAttMul(Dictionary<AttributeType, float> attr)
    {
        _attributes.ChangeAttAdd(attr);
    }
    /*获得属性,返回的对象需要自己回收到内存池，_Character.AttributePool*/
    public Dictionary<AttributeType, float> GetAtt(List<AttributeType> type)
    {
        return _attributes.GetAtt(type);
    }
    /*获得单个属性*/
    public float GetAttSignal(AttributeType type)
    {
        return _attributes.GetAttSignal(type);
    }
    /*获得属性原件*/
    public List<float> GetAllAttByArrary()
    {
        return _attributes.Attr;
    }
    public void UpdataState(bool updataUi = true)
    {
        _attributes.UpdataState(updataUi);
    }
    /*初始化玩家基础属性*/
    public void InitData()
    {
        BaseAttribute att = BaseAttribute.FindById(_character.HeroData.Affx);
        List<float> attr = GetAllAttByArrary();
        attr[(int)AttributeType.Speed] = att.Speed;
        attr[(int)AttributeType.PhyHurt] = att.PhyHurt;
        attr[(int)AttributeType.MagicHurt] = att.MagicHurt;
        attr[(int)AttributeType.PhyDefend] = att.PhyDefend;
        attr[(int)AttributeType.MagicDefend] = att.MagicDefend;
        attr[(int)AttributeType.Power] = att.Power;
        attr[(int)AttributeType.Spell] = att.Spell;
        attr[(int)AttributeType.Crit] = att.Crit;
        attr[(int)AttributeType.Hp] = att.Hp;
        attr[(int)AttributeType.Mp] = att.Mp;
        UpdataState();
    }
}
public enum AttributeType
{
    Speed = 0,
    PhyHurt,
    MagicHurt,
    PhyDefend,
    MagicDefend,
    Power,
    Spell,
    Crit,
    Hp,
    Mp,

    MaxHp,
    MaxMp,
    Lenght,
}
/*记录玩家属性信息*/
public class Attributes
{
    private List<float> _attr = new List<float>((int)AttributeType.Lenght);
    private Character _character;
    public List<float> Attr
    {
        get
        {
            return _attr;
        }

        set
        {
            _attr = value;
        }
    }
    public void UpdataState(bool updataUi = true)
    {
        if (_character.HeroData.characterType == (int)CharacterType.Player && updataUi)
            InformationUI.Instance.SetAttributes(_attr);

        StateBase state;
        if(_character.StateControl != null && _character.StateControl.States != null && _character.StateControl.States.TryGetValue(StateType.Run, out state))
        {
            ((StateRun)state).Speed = _attr[(int)AttributeType.Speed];
        }
    }
    public void Init(Character character)
    {
        _character = character;
        for(int i = 0; i < (int)AttributeType.Lenght; i++)
        {
            _attr.Insert(i, 0);
        }
    }
    public float ChangeAttSignalAdd(AttributeType type, float value, bool updataUi = true)
    {
        int curType = (int)type;
        _attr[curType] += value;
        if (type == AttributeType.Hp)
        {
            if (_attr[curType] > _attr[(int)AttributeType.MaxHp])
                _attr[curType] = _attr[(int)AttributeType.MaxHp];
        }
        if (type == AttributeType.Mp)
        {
            if (_attr[curType] > _attr[(int)AttributeType.MaxMp])
                _attr[curType] = _attr[(int)AttributeType.MaxMp];
        }
        CheckCorrectHpMp();
        UpdataState(updataUi);
        return _attr[(int)type];
    }
    public void ChangeAttAdd(Dictionary<AttributeType, float> attr, bool updataUi = true)
    {

        foreach(var item in attr)
        {
            int curType = (int)item.Key;
            _attr[curType] += item.Value;
            if (item.Key == AttributeType.Hp)
            {
                if (_attr[curType] > _attr[(int)AttributeType.MaxHp])
                    _attr[curType] = _attr[(int)AttributeType.MaxHp];
            }
            if (item.Key == AttributeType.Mp)
            {
                if (_attr[curType] > _attr[(int)AttributeType.MaxMp])
                    _attr[curType] = _attr[(int)AttributeType.MaxMp];
            }
            
        }
        CheckCorrectHpMp();
        UpdataState(updataUi);
        _character.AttributePool.UnSpawn(attr);
    }
    public void ChangeAttMul(Dictionary<AttributeType, float> attr, bool updataUi = true)
    {
        foreach (var item in attr)
        {
            int curType = (int)item.Key;
            _attr[curType] *= item.Value;
            if (item.Key == AttributeType.Hp)
            {
                if (_attr[curType] > _attr[(int)AttributeType.MaxHp])
                    _attr[curType] = _attr[(int)AttributeType.MaxHp];
            }
            if (item.Key == AttributeType.Mp)
            {
                if (_attr[curType] > _attr[(int)AttributeType.MaxMp])
                    _attr[curType] = _attr[(int)AttributeType.MaxMp];
            }
        }
        CheckCorrectHpMp();
        UpdataState(updataUi);

        _character.AttributePool.UnSpawn(attr);
    }
    public Dictionary<AttributeType, float> GetAtt(List<AttributeType> type)
    {
        Dictionary<AttributeType, float> att = _character.AttributePool.Spawn();
        foreach (AttributeType t in type)
        {
            float value = _attr[(int)t];
            att.Add(t, value);
        }
        return att;
    }
    public float GetAttSignal(AttributeType type)
    {
        return _attr[(int)type];
    }

    private void CheckCorrectHpMp()
    {
        if(_attr[(int)AttributeType.Hp] < 0)
        {
            _attr[(int)AttributeType.Hp] = 0;
        }
        if (_attr[(int)AttributeType.Mp] < 0)
        {
            _attr[(int)AttributeType.Mp] = 0;
        }
    }
}