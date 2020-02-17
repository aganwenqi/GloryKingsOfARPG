using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public enum TimerEventType
{
    Enter,
    Stay,
    End,
}
public struct TimerEventData
{
    public TimerEventType type;
    public int count;
    public string sid;
}
public interface TimerEventBase
{
    /*回调函数，时长，触发次数*/
    void Init(TimerManager manager, UnityAction<TimerEventData> actionFunc, float timeLenght, int eventCount, string sid);
    void OnEventEnter();
    void OnEventStay(int count);
    void OnEventEnd();
    void Updata(float timing);
}
