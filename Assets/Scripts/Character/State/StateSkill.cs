using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateSkill : StateBase {

	public override void Init(Character character)
	{
		base.Init (character);
		_stateType = StateType.Skill;
	}

	public override void OnEnter()
	{
		base.OnEnter ();
        SkillSyncData CurUsingSkillSyncData = _character.CharacterSkill.CurUsingSkillSyncData;

        _character.CharacterSkill.Skilling = true;
		_character.CharacterAnim.CrossFade (CurUsingSkillSyncData.SkillData.AnimName);
        /*播放音效*/
        _character.CharacterSound.PlayHeroSound(CurUsingSkillSyncData.SkillClip.HeroClip, CurUsingSkillSyncData.SkillClip.DelayHero);
        _character.CharacterSound.PlaySkillSound(CurUsingSkillSyncData.SkillClip.SkillClip, CurUsingSkillSyncData.SkillClip.DelaySkill);
    }

	public override void OnUpdate()
	{
		base.OnUpdate ();

		if (_timing >= _character.CharacterSkill.CurUsingSkillSyncData.SkillData.AnimLength) {
			_character.CharacterSkill.OnEndSkill ();

		}

	}

	public override void OnExit()
	{
		base.OnExit ();
        SkillSyncData CurUsingSkillSyncData = _character.CharacterSkill.CurUsingSkillSyncData;
        _character.CharacterSound.StopSkillSound(CurUsingSkillSyncData.SkillClip.SkillClip);
    }

}
