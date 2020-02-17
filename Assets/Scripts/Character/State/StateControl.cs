using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 状态控制器组件
/// </summary>
public class StateControl {

	protected Character _character;

    private Dictionary<StateType, StateBase> _states;

    protected StateBase _curState;
	public StateBase CurState
	{
		get
		{
			return _curState;
		}
	}

    public void Init(Character character)
	{
		_character = character;
		_states = new Dictionary<StateType, StateBase> ();
        
		StateIdle idle = new StateIdle();
		idle.Init (_character);
		_states.Add (StateType.Idle, idle);
        StateRun run = new StateRun();
		run.Init (_character);
		_states.Add (StateType.Run, run);

        StateAnim anim = new StateAnim();
        anim.Init(_character);
        _states.Add(StateType.Anim, anim);

        StateSkill skill = new StateSkill();
		skill.Init (_character);
		_states.Add (StateType.Skill, skill);

		StateHit hit = new StateHit();
		hit.Init (_character);
		_states.Add (StateType.Hit, hit);

		StateDead dead = new StateDead();
		dead.Init (_character);
		_states.Add (StateType.Dead, dead);

        /*附加状态调用*/
        StateRepel repel = new StateRepel();
        repel.Init(_character);
        _states.Add(StateType.Repel, repel);
        //角色生成时候的初始状态
        ChangeState (StateType.Idle);

	}

    public void InitData()
    {
        foreach(var item in _states)
        {
            item.Value.InitData();
        }
        ChangeState(StateType.Idle, true);
    }
	/// <summary>
	/// 核心借口 切换当前的状态 ,force是否强制切换
	/// </summary>
	public void ChangeState(StateType stateType, bool force = false)
	{

		if (force != true && _curState != null && _curState.StateType == stateType) {
			return;
		}

		if (_curState != null) {
			_curState.OnExit ();
		}

		if (_states.ContainsKey(stateType)) {
			_curState = _states [stateType];

			_curState.OnEnter ();
		}

	}
    public  void FixedUpdate(float _timing)
    {
        if (_curState != null)
        {
            _curState.OnFixedUpdate();
        }
    }
	/// <summary>
	/// 需要外部调用
	/// </summary>
	public void Update(float _timing)
	{
		if (_curState != null) {
			_curState.OnUpdate ();
		}
	}
    public Dictionary<StateType, StateBase> States
    {
        get
        {
            return _states;
        }

        set
        {
            _states = value;
        }
    }
}
