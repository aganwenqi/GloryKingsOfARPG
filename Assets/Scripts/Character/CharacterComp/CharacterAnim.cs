using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 角色动作组件
/// </summary>
public class CharacterAnim : CharacterCompBase{

	private Animator _animator;

	public override void Init (Character character)
	{
		base.Init (character);

		_animator = _character.Animator;
	}
    public override void InitData()
    {
        base.InitData();
    }
    public void PlayAnim(string animName)
	{
		_animator.Play (animName);
	}

    /*动画名，播放速度，插值变换时长，层，片源*/
    public void CrossFade(string animName, float speed = 1, float transitionDuration = 0.1f, int layer = 0, float normalizedTime = 0)
	{
        _animator.CrossFade(animName, transitionDuration, layer, normalizedTime);
        _animator.speed = speed;
    }

}
