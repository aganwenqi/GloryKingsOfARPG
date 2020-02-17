using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class StateAnim : StateBase {
    private string _animName;
    public override void Init(Character character)
	{
		base.Init (character);
		_stateType = StateType.Anim;
        _animName = "run";
    }
	public override void OnEnter()
	{
		base.OnEnter ();
		_character.CharacterAnim.CrossFade (_animName);
	}

	public override void OnExit()
	{
		base.OnExit ();
	}

    public void SetAnim(string animName)
	{
        _animName = animName;
    }

}
