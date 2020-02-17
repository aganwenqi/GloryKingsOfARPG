using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
/*定时器*/
public class TimerManager : MonoBehaviour {
    public static TimerManager Instance;
    private Dictionary<string, TimerEventBase> _allTimerEvent = new Dictionary<string, TimerEventBase>();
    private List<TimerEventBase> _sycTimerEvent = new List<TimerEventBase>();//解决foreach无法删除增加问题

    private PoolBaseObj<TimerEvent> _timerEventPool = new PoolBaseObj<TimerEvent>();
    private void Awake()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update () {
        _sycTimerEvent.Clear();
        foreach (var item in _allTimerEvent)
        {
            _sycTimerEvent.Add(item.Value);
        }
        for(int i = 0; i < _sycTimerEvent.Count; i++)
        {
            _sycTimerEvent[i].Updata(Time.deltaTime);
        }
    }
    /// <summary>
    /// 添加定时事件,回调函数，时长，触发次数
    /// </summary>
    public TimerEventBase AddTimerEvent(UnityAction<TimerEventData> actionFunc, float timeLenght, int eventCount)
    {
        string sid = SerialIdManager.Instance.GetSid();
        TimerEventBase item = _timerEventPool.Spawn();
        item.Init(this, actionFunc, timeLenght, eventCount, sid);
        _allTimerEvent.Add(sid, item);
        return item;
    }
    /// <summary>
    /// 删除定时器
    /// </summary>
    public bool RemoveTimerEvent(string sid)
    {
        TimerEventBase item = null;
        bool result = _allTimerEvent.TryGetValue(sid, out item);
        if(result)
        {
            _allTimerEvent.Remove(sid);
            _timerEventPool.UnSpawn((TimerEvent)item);
        }
        return result;
    }
    public bool RemoveTimerEvent(TimerEventBase item)
    {
        _timerEventPool.UnSpawn((TimerEvent)item);
        return _allTimerEvent.Remove((item as TimerEvent).Sid);
    }
    #region 对象属性
    public Dictionary<string, TimerEventBase> AllTimerEvent
    {
        get
        {
            return _allTimerEvent;
        }

        set
        {
            _allTimerEvent = value;
        }
    }
    #endregion
}
