using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 角色功能组件基类 
/// </summary>
public class CharacterCompBase {


	protected Character _character;
	public virtual void Init(Character character)
	{
		_character = character;

	}
    public virtual void InitData()
    {

    }
	public virtual void Update(float _timing)
	{
		
	}

	public virtual void LateUpdate()
	{
		
	}
    public virtual void UnSpawn()
    {

    }
}
