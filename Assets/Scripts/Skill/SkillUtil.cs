using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillUtil {

	public static SkillCompBehaviour CreateSkillCompBehaviour(SkillCompBehaviourType type)
	{
		SkillCompBehaviour result = null;
		if (type == SkillCompBehaviourType.Normal) {//正常范围攻击技能
			result = new SkillCompBehaviourNormal ();
		}
		else if (type == SkillCompBehaviourType.Bullet) {
            result = new SkillCompBehaviourNormlBullet();
        }
        else if(type == SkillCompBehaviourType.Buff){
            result = new SkillCompBehaviourBuff();
        }
		return result;
	}

}

/// <summary>
/// 技能组件的行为类型
/// </summary>
public enum SkillCompBehaviourType
{
	None = 0,
	Normal = 1,//正常范围攻击
	Bullet = 2,//弹道
    Buff = 3,
}
