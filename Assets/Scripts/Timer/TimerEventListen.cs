using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerEventListen
{
    public TimerEventBase _timerEvent;
    /*这个方法需要注册到定时器里的*/
    public virtual void OnEvent(TimerEventData events)
    {
        switch (events.type)
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
    }

    public virtual void OnEventStay(TimerEventData events)
    {
    }
    public virtual void OnEventEnd(TimerEventData events)
    {

    }
}
