using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdditioalStateRepel : AdditioalStateBase
{
    private bool _using;
    private AdditinoalEffect _cfg;
    private Vector3 _dir;
    public override void Init(Character character)
    {
        base.Init(character);
        _canAdd = false;
        _using = false;
        _cfg = null;
    }
    public override void Update(float timing)
    {
        base.Update(timing);
        if (_using)//玩家向后退
        {
            _character.transform.position += _dir * timing;
        }
    }
    public override void OnEventEnter(TimerEventData events)
    {
        base.OnEventEnter(events);
        _character.OnEndState();
        _character.StateControl.ChangeState(StateType.Repel);
        AdditinoalModel model;
        if (!_curStateCfg.TryGetValue(events.sid, out model))
        {
            return;
        }
        _cfg = model._curStateCfg;
        Vector3 noOffect = new Vector3(model._dst.transform.position.x, _character.transform.position.y , model._dst.transform.position.z);
        _dir = noOffect - _character.transform.position;
        _dir = _dir.normalized * _cfg.Hurt * 2;
        _using = true;
    }

    public override void OnEventEnd(TimerEventData events)
    {
        _using = false;
        _character.OnEndState();
        base.OnEventEnd(events);
    }
}
