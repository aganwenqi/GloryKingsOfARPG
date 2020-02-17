using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateHit : StateBase {

    private float _beHurtTime = 1;
	public override void Init(Character character)
	{
		base.Init (character);
		_stateType = StateType.Hit;
        BaseAttribute baseAtt = BaseAttribute.FindById(character.HeroData.Affx);
        if (baseAtt != null)
            _beHurtTime = baseAtt.BeHurtTime;
    }

	public override void OnEnter()
	{
		base.OnEnter ();
        _character.CharacterAnim.CrossFade("hit", 1, 0.1f);
        _character.CharacterSkill.Skilling = true;
        CharcaterOtherClip baseClip = _character.CharacterSound.BaseClip;
        if (baseClip != null)
        {
            _character.CharacterSound.PlayHeroSound(baseClip.HitClip, baseClip.DelayHit);
        }
    }

	public override void OnUpdate()
	{
		base.OnUpdate ();

		if (_timing >= _beHurtTime) {
			_character.StateControl.ChangeState (StateType.Idle);
		}
	}

	public override void OnExit()
	{
		base.OnExit ();
        _character.CharacterSkill.Skilling = false;
    }
    /*伤害角色*/
	public void ChangeHit(HurtResult hurt, DataBase data, SkillCompBehaviourType type)
	{
        int hitFx = 0;
        if(type == SkillCompBehaviourType.Normal)
        {
            hitFx = ((SkillCompData)data).HitFx;
        }
        else if(type == SkillCompBehaviourType.Bullet)
        {
            hitFx = ((Bullet)data).HitFx;
        }
        FxData fxData = FxData.FindById(hitFx);
        _character.CharacterFx.CreateFx(fxData);

        AttributesControl attControl = _character.CharacterAttribute.AttControl;
        float curHp = attControl.ChangeAttSignalAdd(AttributeType.Hp, hurt.hurt);
		if (curHp <= 0) {
            _character.StateControl.ChangeState(StateType.Idle);
            _character.StateControl.ChangeState(StateType.Dead);
        }
        /*飘字*/
        _character.CharacterFly.PlayFlyFont((int)hurt.hurt, hurt.hurt <= 0 ? CharacterFlyFont.FlyType.Red : CharacterFlyFont.FlyType.Green, hurt.isCrit);
        //更新显示
        //Debug.LogError(_character.CharacterUtilData.sid + "当前血量剩下 " + curHp);
    }
    /*直接伤害角色*/
    public void ChangeHit(HurtResult hurt)
    {
        AttributesControl attControl = _character.CharacterAttribute.AttControl;
        float curHp = attControl.ChangeAttSignalAdd(AttributeType.Hp, hurt.hurt);
        if (curHp <= 0)
        {
            _character.StateControl.ChangeState(StateType.Idle);
            _character.StateControl.ChangeState(StateType.Dead);
        }
        /*飘字*/
        _character.CharacterFly.PlayFlyFont((int)hurt.hurt, hurt.hurt < 0 ? CharacterFlyFont.FlyType.Red : CharacterFlyFont.FlyType.Green, hurt.isCrit);
        //更新显示
        Debug.LogError(_character.CharacterUtilData.sid + "当前血量剩下 " + curHp);
    }
}
