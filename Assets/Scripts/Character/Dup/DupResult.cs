using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DupResult
{
    DupController _manager;
    /*出来结果后多久触发结果*/
    private float _maxTime = 2;
    private float _curTime = 0;
    private bool _isAction;
    public void Init(DupController manager)
    {
        this._manager = manager;
        _isAction = false;
    }

    /*玩家死亡*/
    public void PlayerGetOver(Character player)
    {
        _isAction = true;
        _curTime = 0;
    }
    /*通关*/
    public void CrossDup()
    {
        _isAction = true;
        _curTime = 0;
        _manager.Player.StateControl.ChangeState(StateType.Idle);  
    }
    public void Updata()
    {
        if(_isAction)
        {
            _curTime += Time.deltaTime;
            if(_curTime >= _maxTime)
            {
                _isAction = false;
                UICanvasManager.Instance.OnEnterUI(DupResultUI.Instance);
            }
        }
    }
}
