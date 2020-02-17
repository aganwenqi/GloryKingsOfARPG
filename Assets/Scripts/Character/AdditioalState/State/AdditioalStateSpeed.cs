using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdditioalStateSpeed : AdditioalStateBase
{
    public override void Init(Character character)
    {
        base.Init(character);
    }
    public override void OnEventEnter(TimerEventData events)
    {
        base.OnEventEnter(events);
    }

    public override void OnEventStay(TimerEventData events)
    {
        base.OnEventStay(events);
        AdditinoalModel model;
        if(!_curStateCfg.TryGetValue(events.sid, out model))
        {
            return;
        }
        AdditinoalEffect cfg = model._curStateCfg;
        Character dst = model._dst;
        if(cfg.HurtType == (int)AdditinoalHurtType.fix)//固定伤害
        {
            _character.CharacterAttribute.AttControl.ChangeAttSignalAdd(AttributeType.Speed, cfg.Hurt);
        }
        else if(cfg.HurtType == (int)AdditinoalHurtType.player)//乘玩家属性
        {
            //TODO
        }
    }
    public override void OnEventEnd(TimerEventData events)
    {
        AdditinoalModel model;
        if (!_curStateCfg.TryGetValue(events.sid, out model))
        {
            return;
        }
        AdditinoalEffect cfg = model._curStateCfg;
        Character dst = model._dst;
        if (cfg.HurtType == (int)AdditinoalHurtType.fix)//固定伤害
        {
            _character.CharacterAttribute.AttControl.ChangeAttSignalAdd(AttributeType.Speed, -cfg.Hurt);
        }
        else if (cfg.HurtType == (int)AdditinoalHurtType.player)//乘玩家属性
        {
            //TODO
        }

        base.OnEventEnd(events);
    }
}
