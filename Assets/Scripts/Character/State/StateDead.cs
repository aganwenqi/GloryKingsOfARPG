using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateDead : StateBase {
    float _endTime;
	public override void Init(Character character)
	{
		base.Init (character);
		_stateType = StateType.Dead;

    }

	public override void OnEnter()
	{
		base.OnEnter ();
		_character.CharacterAnim.CrossFade ("dead");
        CharacterManager.Instance.AllCharacters.Remove(_character);
        DupController.Instance.RemoveCharacter(_character);
        _character.Dead();
        CharcaterOtherClip baseClip = _character.CharacterSound.BaseClip;
        if (baseClip != null)
        {
            _character.CharacterSound.PlayHeroSound(baseClip.DeadClip, baseClip.DelayDead);
        }
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
