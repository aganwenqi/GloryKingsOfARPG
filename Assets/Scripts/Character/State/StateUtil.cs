using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateUtil {

}

public enum StateType
{
	Appear,
	Idle,
	Run,
    Anim,
	Skill,
	Hit,
	Dead,

    /*附加状态*/
    Repel,
}
/*附加状态*/
public enum AdditioalStateType
{
    Speed = 1,//移动速度
    Burning,//灼伤
    Repel,//击退
}