using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateRepel : StateBase {

	public override void Init(Character character)
	{
		base.Init (character);
        _stateType = StateType.Repel;
    }

	public override void OnEnter()
	{
		base.OnEnter ();
        _character.CharacterAnim.CrossFade("repel");
        _character.CharacterSkill.Skilling = true;
        _character.DontUse = true;
        /*
        CharcaterOtherClip baseClip = _character.CharacterSound.BaseClip;
        if (baseClip != null)
        {
            _character.CharacterSound.PlayHeroSound(baseClip.HitClip, baseClip.DelayHit);
        }*/
    }

	public override void OnUpdate()
	{
		base.OnUpdate ();

	}

	public override void OnExit()
	{
		base.OnExit ();
        _character.CharacterSkill.Skilling = false;
        _character.DontUse = false;
    }
}
