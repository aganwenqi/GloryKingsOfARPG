using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdditioalStateBase
{
    protected Character _character;

    protected AdditioalStateType _stateType;

    /*是否可叠加*/
    protected bool _canAdd = true;

    /*当前定时器状态下的所有sid对应的配置*/
    protected Dictionary<string, AdditinoalModel> _curStateCfg = new Dictionary<string, AdditinoalModel>();

    public AdditioalStateType AddStateType
    {
        get
        {
            return _stateType;
        }
    }

    public virtual void Init(Character character)
    {
        _character = character;
    }
    public virtual void InitData()
    {
        _curStateCfg.Clear();
    }
    
    /*有状态来了要调用这个*/
    public virtual void AddState(string sid, AdditinoalModel model)
    {
        Clear();
        _curStateCfg.Add(sid, model);
    }
    public virtual void Update(float timing)
    {

    }
    /*这个方法需要注册到定时器里的*/
    public virtual void OnEvent(TimerEventData events)
    {
        switch(events.type)
        {
            case TimerEventType.Enter:
                OnEventEnter(events);
                break;
            case TimerEventType.Stay:
                OnEventStay(events);
                break;
            case TimerEventType.End:
                OnEventEnd(events);
                break;
        }
    }

    public virtual void OnEventEnter(TimerEventData events)
    {
        /*触发附加粒子*/
        AdditinoalModel model;
        if (!_curStateCfg.TryGetValue(events.sid, out model) || model._curStateCfg.Fx == 0)
        {
            return;
        }
        FxData fx = FxData.FindById(model._curStateCfg.Fx);

        model._fxBase = _character.CharacterFx.CreateFx(fx);
    }

    public virtual void OnEventStay(TimerEventData events)
    {
    }
    public virtual void OnEventEnd(TimerEventData events)
    {
        /*触发附加粒子结束*/
        AdditinoalModel model = null;
        if (_curStateCfg.TryGetValue(events.sid, out model) && model._fxBase)
        {
            model._fxBase._liveTimeEnd();
        }
        _character.AddStateControl.RemoveTimer(events.sid);
        _character.AddStateControl.ModelPool.UnSpawn(model);
        _curStateCfg.Remove(events.sid);

    }
    private void Clear()
    {
        if(_canAdd == false)
        {
            foreach(var item in _curStateCfg)
            {
                if (item.Value._fxBase)
                {
                    item.Value._fxBase._liveTimeEnd();
                }
                _character.AddStateControl.RemoveTimer(item.Key);
                _character.AddStateControl.ModelPool.UnSpawn(item.Value);
            }
            _curStateCfg.Clear();
        }
    }
}
