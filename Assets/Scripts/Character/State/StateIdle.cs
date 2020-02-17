using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateIdle : StateBase {
	public override void Init(Character character)
	{
		base.Init (character);
		_stateType = StateType.Idle;
	}

	public override void OnEnter()
	{
		base.OnEnter ();

		if (_character.CharacterAttribute.AttControl.GetAttSignal(AttributeType.Hp) <= 0) {
            return;
		} else 
		{
			_character.CharacterAnim.CrossFade ("idle");
            
        }
        _character.CharacterSkill.Skilling = false;
    }

	public override void OnUpdate()
	{
		base.OnUpdate ();
	}

	public override void OnExit()
	{
		base.OnExit ();
	}

}
