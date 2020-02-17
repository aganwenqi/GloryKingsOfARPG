using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateRun : StateBase {
    private float _speed;

    /*初始速度*/
    private float _initSpeed;
    /*换算后的速度*/
    private float _finalSpeed;
    /*玩家的属性*/
    private AttributesControl _attControl;
    public float Speed
    {
        get
        {
            return _speed;
        }

        set
        {
            _speed = value;
        }
    }
    public override void Init(Character character)
	{
		base.Init (character);
		_stateType = StateType.Run;
        _attControl = _character.CharacterAttribute.AttControl;
        _speed = _attControl.GetAttSignal(AttributeType.Speed);
        _initSpeed = _speed;
        _finalSpeed = 1;
    }
	public override void OnEnter()
	{
		base.OnEnter ();
        _speed = _attControl.GetAttSignal(AttributeType.Speed);
        _finalSpeed = _speed / _initSpeed;
        _character.CharacterAnim.CrossFade ("run", _finalSpeed); 
    }
    public override void OnFixedUpdate()
    {
        base.OnFixedUpdate();
        _character.transform.Translate(_dir * Time.deltaTime * _speed, Space.World);

        _character.transform.LookAt(_dir + _character.transform.position);

        if (_speed == 0)
        {
            _character.StateControl.ChangeState(StateType.Idle);
        }
    }
    public override void OnUpdate()
	{

	}

	public override void OnExit()
	{
		base.OnExit ();
	}

   

    Vector3 _dir;

    public void SetDir(Vector3 dir, float speed = 0)
	{
		_dir = dir;
		_speed += speed;
       
    }
    public void SetSpeed(float speed = 0)
    {
        _speed = speed;
        _finalSpeed = _speed / _initSpeed;
        _character.Animator.speed = _finalSpeed;
    }

}
