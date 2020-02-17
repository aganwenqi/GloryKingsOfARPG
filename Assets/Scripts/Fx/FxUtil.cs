using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FxUtil {


	public static Transform GetBindingTrans(FxBindingType type, Character character)
	{
		if (type == FxBindingType.None)
        {
			return null;
		}	
		else if (type == FxBindingType.CharacterRoot)
        {
			return character.transform;
		}
		else if (type == FxBindingType.WeaponRoot)
        {
            return character.Weapon.transform;
		}
        else if (type == FxBindingType.WeaponRoot2)
        {
            return character.Weapon2.transform;
        }
        else if (type == FxBindingType.HitPoint)
        {
            if(character.hitPoint)
            {
                return character.hitPoint;
            }
            else
            {
                return character.transform;
            }
        }
        return character.transform;
    }


}

public enum FxBindingType
{
	None = 0,
	CharacterRoot,
	WeaponRoot,
    WeaponRoot2,
    HitPoint,//受伤点
}
