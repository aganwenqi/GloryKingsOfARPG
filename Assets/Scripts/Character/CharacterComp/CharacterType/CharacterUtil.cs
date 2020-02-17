using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 角色公用方法
/// </summary>
public class CharacterUtil {

	/// <summary>
	/// 获取角色类型组件工厂
	/// </summary>
	public static CharacterTypeBase GetCharaterTypeInstance(CharacterType characterType)
	{
		CharacterTypeBase characterTypeBase = null;
		if (characterType == CharacterType.Monster) {
			characterTypeBase = new CharacterTypeMonster ();
		}
		else if(characterType == CharacterType.Player)
		{
			characterTypeBase = new CharacterTypeRole ();
		}

		return characterTypeBase;
	}
    /// <summary>
	/// 是否开启AI,默认玩家不开，怪物开
	/// </summary>
    public static bool IsOpenAI(CharacterType characterType)
    {
        if (characterType == CharacterType.Monster)
            return true;
        else if (characterType == CharacterType.Player)
            return false;

        return false;
    }
}

/// <summary>
/// 角色类型 
/// </summary>
public enum CharacterType
{
    None = 0,
    Player = 1,
    Monster = 2,
	Partner,
}
