using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
/*使用针对性对象池*/
public class TimerEvent : TimerEventBase
{
    private string _sid;
    private TimerManager _manager;
    private UnityAction<TimerEventData> _actionFunc;
    private int _eventCount;
    private int _curCount;

    private float _roundTime;//触发时间间隔
    private float _curTime;

    private bool isEnter;
    public void Init(TimerManager manager, UnityAction<TimerEventData> actionFunc, float timeLenght, int eventCount, string sid)
    {
        _manager = manager;
        _sid = sid;
        _actionFunc = actionFunc;
        _eventCount = eventCount;
        _curTime = 0;
        _curCount = 0;
        isEnter = false;
        if (eventCount <= 1)
        {
            _eventCount = 1;
            _roundTime = timeLenght;
        }
        else
        {
            _roundTime = timeLenght / eventCount;
        }     
    }
    public void OnEventEnter()
    {
        TimerEventData result = new TimerEventData();
        result.count = 0;
        result.type = TimerEventType.Enter;
        result.sid = Sid;
        if (_actionFunc != null)
            _actionFunc.Invoke(result);
    }

    public void OnEventStay(int count)
    {
        TimerEventData result = new TimerEventData();
        result.count = count;
        result.type = TimerEventType.Stay;
        result.sid = Sid;
        if (_actionFunc != null)
            _actionFunc.Invoke(result);
    }
    public void OnEventEnd()
    {
        TimerEventData result = new TimerEventData();
        result.count = _curCount;
        result.type = TimerEventType.End;
        result.sid = Sid;
        if (_actionFunc != null)
            _actionFunc.Invoke(result);
        _manager.RemoveTimerEvent(this);
    }
    public void Updata(float timing)
    {
        _curTime += timing;
        if (!isEnter)
        {
            OnEventEnter();
            OnEventStay(++_curCount);
            isEnter = true;
        }
        if(_curTime > _roundTime)
        {
            _curCount++;
            if (_curCount > _eventCount)
            {
                OnEventEnd();
                return;
            }
            _curTime = 0;
            OnEventStay(_curCount);
        }
    }
    #region 对象属性
    public string Sid
    {
        get
        {
            return _sid;
        }

        set
        {
            _sid = value;
        }
    }
    #endregion
}
