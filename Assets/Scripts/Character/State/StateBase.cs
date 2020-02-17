using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 状态基类
/// </summary>
public class StateBase {

	protected Character _character;


	protected StateType _stateType;
	public StateType StateType
	{
		get
		{
			return _stateType;
		}
	}

	protected float _timing;

	public virtual void Init(Character character)
	{
		_character = character;
	}
    public virtual void InitData()
    {
        _timing = 0;
    }
	public virtual void OnEnter()
	{
		//Debug.LogError ("进入状态 : " + _stateType);
		_timing = 0;
	}
    public virtual void OnFixedUpdate()
    {

    }
	public virtual void OnUpdate()
	{
		_timing += Time.deltaTime;
	}

	public virtual void OnExit()
	{
		//Debug.LogError ("退出状态 : " + _stateType);
	}


}
