using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdditioalStateBurning : AdditioalStateBase
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
            HurtResult hurt = new HurtResult();
            hurt.hurt = cfg.Hurt;
            hurt.isCrit = false;
            _character.Hit(hurt);
        }
        else if(cfg.HurtType == (int)AdditinoalHurtType.player)//乘玩家属性
        {
            //TODO
        }
    }
    public override void OnEventEnd(TimerEventData events)
    {
        base.OnEventEnd(events);
    }
}
