using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 附加状态控制器组件
/// </summary>
public class AdditioalStateControl
{

    protected Character _character;
    /*所有状态表*/
    private Dictionary<AdditioalStateType, AdditioalStateBase> _states = new Dictionary<AdditioalStateType, AdditioalStateBase>();
    /*当前触发的定时器*/
    private Dictionary<string, TimerEventBase> _timer = new Dictionary<string, TimerEventBase>();

    /*model对象池*/
    private PoolBaseObj<AdditinoalModel> _modelPool = new PoolBaseObj<AdditinoalModel>();

    
    public void Init(Character character)
    {
        _character = character;

        AdditioalStateSpeed speed = new AdditioalStateSpeed();
        speed.Init(_character);
        _states.Add(AdditioalStateType.Speed, speed);

        AdditioalStateBurning burning = new AdditioalStateBurning();
        burning.Init(_character);
        _states.Add(AdditioalStateType.Burning, burning);

        AdditioalStateRepel repel = new AdditioalStateRepel();
        repel.Init(_character);
        _states.Add(AdditioalStateType.Repel, repel);
    }
    public void InitData()
    {
        _timer.Clear();
        RemoveAllTimer();
        foreach (var item in _states)
        {
            item.Value.InitData();
        }
    }
    public void Update(float _timing)
    {
        foreach (var item in _states)
        {
            item.Value.Update(_timing);
        }
    }
    /*添加状态,状态id,谁给我加状态*/
    public void AddState(int id, Character dst)
    {
        if(_character.IsDead)
        {
            return;
        }
        AdditinoalEffect effect = AdditinoalEffect.FindById(id);
        AdditioalStateType types = (AdditioalStateType)effect.AdditioalStateType;
        AdditioalStateBase stateBase = null;
        if(_states.TryGetValue(types, out stateBase))
        {
            /*触发一个定时器*/
            TimerEventBase timerEvent = TimerManager.Instance.AddTimerEvent(stateBase.OnEvent, effect.TimeLen, effect.Count);
            AdditinoalModel model = _modelPool.Spawn();
            model._curStateCfg = effect;
            model._dst = dst;
            stateBase.AddState((timerEvent as TimerEvent).Sid, model);
            _timer.Add((timerEvent as TimerEvent).Sid, timerEvent);
        }
    }

    /*删除一个定时器状态*/
    public void RemoveTimer(string sid)
    {
        _timer.Remove(sid);
    }
    /*删除所有定时器状态*/
    public void RemoveAllTimer()
    {
        _timer.Clear();
    }

    #region 对象属性
    public PoolBaseObj<AdditinoalModel> ModelPool
    {
        get
        {
            return _modelPool;
        }

        set
        {
            _modelPool = value;
        }
    }
    #endregion
}
