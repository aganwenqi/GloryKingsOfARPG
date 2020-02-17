using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 范围技能攻击
/// </summary>
public class SkillCompBehaviourNormal : SkillCompBehaviour {

	private float _range;


	private float _angleLimit = 0;

	private float _posOffSet = 0;
	private Vector3 _offsetPos;
	public override void Init (SkillSyncData manager, int skillCompId)
	{
		base.Init (manager, skillCompId);
        _range = _skillCompData.Param2;
		_angleLimit = _skillCompData.Param3;
		_posOffSet = _skillCompData.Param4;

		//_manager.Character.transform.position


	}
    public override void InitData()
    {
        base.InitData();
    }
    public override void Trigger ()
	{
		base.Trigger ();
	}

    /*开始计算伤害*/
	public override void Effect ()
	{
		base.Effect ();
        if(_skillCompData.AttackCount <= 1)//只打一次
        {
            HurtTarget();
            return;
        }

        /*要打n次, 触发一个定时器*/
        _timerEvent = TimerManager.Instance.AddTimerEvent(OnEvent, _skillCompData.AttackTime, _skillCompData.AttackCount); 
    }
    public override void OnEventStay(TimerEventData events)
    {
        base.OnEventStay(events);
        HurtTarget();
    }
    /*伤害角色*/
    private void HurtTarget()
    {
        bool result = false;//是否有打到人
        Transform scr = this.GetCheckPoint();
        _offsetPos = scr.position + (-scr.forward * _posOffSet);
        //Debug.DrawLine (_offsetPos, _manager.Character.transform.position, Color.blue, 2);

        //获得副本敌方英雄
        List<Character> oppos = _manager.Character.CharacterDup.AttackTargets;

        for (int i = 0; i < oppos.Count; i++)
        {
            float dis = Vector3.Distance(oppos[i].transform.position, scr.position);
            //Debug.Log("攻击距离：" + dis + " 设定距离：" + _range);
            if (dis <= _range)
            {
                if (_passRangeLimit(scr.forward, _offsetPos, oppos[i]))
                {
                    SkillCompBehaviourType type = (SkillCompBehaviourType)_skillCompData.SkillCompBehaviourType;
                    HurtResult hurt = _manager.Character.PrintHurt(_skillCompData, oppos[i].CharacterAttribute.AttControl, type);
                    oppos[i].Hit(hurt, _skillCompData, _manager, type);
                    if (_skillCompData.AdditionEf != 0)
                    {
                        oppos[i].AddStateControl.AddState(_skillCompData.AdditionEf, _manager.Character);
                    }
                    result = true;
                }

            }
        }
        if(result)
        {
            //打到了播放音乐
            _manager.Character.CharacterSound.PlaySkillSound(_manager.SkillClip.HitClip, _manager.SkillClip.DelayHit);
        }
    }
	Vector3 _temp1;
	Vector3 _temp2;
	/// <summary>
	/// 角度计算
	/// </summary>
	private bool _passRangeLimit(Vector3 v, Vector3 src, Character dst)
	{
		if (_angleLimit == 0) {
			return true;
		}

		_temp1 = Vector3.Normalize (v);
		_temp2 = Vector3.Normalize (dst.transform.position - src);

		float result = Mathf.Acos (Vector3.Dot (_temp1, _temp2) / (_temp1.magnitude * _temp2.magnitude));
		result = (180 / Mathf.PI) * result;

		if (result * 2 <= _angleLimit) {
			return true;
		}

		return false;
	}

}
