using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 技能控制组件
/// </summary>
public class CharacterSkill : CharacterCompBase {

	private List<int> _allSkillIds = new List<int>();

	private Dictionary<int, SkillSyncData> _skillSyncDatas = new Dictionary<int, SkillSyncData> ();
	
	public SkillSyncData CurUsingSkillSyncData;//当前释放着的技能

    private SkillSyncData _normlSkillSyncData;//普通攻击

    private int _nextSkillId;//下一个要释放的技能

    private bool _skilling;
  
	public override void Init (Character character)
	{
		base.Init (character);
		Skilling = false;
        _normlSkillSyncData = null;
        _allSkillIds.Clear ();

		string[] skillsStr = character.HeroData.AllSkills.Split ('|');
		if (skillsStr != null) {
			for (int i = 0; i < skillsStr.Length; i++) {
				_allSkillIds.Add(int.Parse (skillsStr [i]));
			}
		}

		_skillSyncDatas.Clear ();

		for (int i = 0; i < _allSkillIds.Count; i++) {
			SkillSyncData data = new SkillSyncData ();
			data.Init (_character, _allSkillIds [i]);
			_skillSyncDatas.Add (_allSkillIds[i], data);
			if(_normlSkillSyncData == null && data.SkillData.SkillType == (int)SkillType.NormalSkill)
            {
                _normlSkillSyncData = data;
            }
		}

	}
    public override void InitData()
    {
        base.InitData();
        CurUsingSkillSyncData = null;
        foreach(var item in _skillSyncDatas)
        {
            item.Value.InitData();
        }
    }
    public void OnEndSkill()
	{
		_character.CharacterSkill.Skilling = false;
		_character.StateControl.ChangeState (StateType.Idle);

		CurUsingSkillSyncData.OnEnd ();
		CurUsingSkillSyncData = null;

		//TriggerSkill (CurUsingSkillSyncData.SkillData.LinkSkillId);

	}


	public override void Update (float _timing)
	{
		base.Update (_timing);

		foreach (var item in _skillSyncDatas) {
			item.Value.Update (_timing);
		}

	}

    /*使用技能,默认使用普通攻击*/
    public bool UseSkill(SkillSyncData useSkill = null)
    {
        if (!CanUseSkill())
        {
            return false;
        }

        if(useSkill == null)
        {
            useSkill = _normlSkillSyncData;
        }
        if (useSkill.SkillData.SkillType == (int)SkillType.NormalSkill)//普通攻击
        {
            if (!Skilling || CurUsingSkillSyncData == null)
            {
                _nextSkillId = GetFirstNormalSkillId();
            }
            else
            {
                _nextSkillId = CurUsingSkillSyncData.SkillData.LinkSkillId;
            }
            _normlSkillSyncData = _skillSyncDatas[_nextSkillId];
        }
        else //正常技能
        {
            if (Skilling)
            {
                return false;
            }
            else
            {
                _nextSkillId = useSkill.SkillData.Id;
            }
        }
        return TriggerSkill(_nextSkillId);
    }
	public bool TriggerSkill(int skillId)
	{
		if (skillId == 0) {
			return false;
		}

		if (!_skillSyncDatas.ContainsKey(skillId)) {
			Debug.LogError ("无法触发技能，因为不包含技能id : " + skillId);
			return false;
		}


		//1.cd是否结束
		if (_skillSyncDatas[skillId].CdTiming > 0) {
			//Debug.LogError ("技能" + skillId.ToString() + "还处于cd状态");
			return false;
		}

		//3.是否一定要待机状态才能播放技能
		if (Skilling) {

			if (CurUsingSkillSyncData != null && CurUsingSkillSyncData.SkillData.LinkSkillId != 0) 
			{
                OnEndSkill();
            } 
			else 
			{
				Debug.LogError ("无法触发技能，还在使用其他技能,需结束其他技能");
				return false;
			}

		}

		CurUsingSkillSyncData = _skillSyncDatas [skillId];
		CurUsingSkillSyncData.TriggerSkill ();

		_character.StateControl.ChangeState (StateType.Idle);
		_character.StateControl.ChangeState (StateType.Skill);

		return true;
	}

	public int GetFirstNormalSkillId()
	{
		foreach (var item in _skillSyncDatas) {
			if (item.Value.SkillData.SkillType == (int)SkillType.NormalSkill) {
				return item.Key;
			}
		}
		return 0;
	}

	public int GetLastNormalSkillId()
	{
		List<int> allNormalSkilIds = new List<int> ();
		foreach (var item in _skillSyncDatas) {
			if (item.Value.SkillData.SkillType == (int)SkillType.NormalSkill) {
				allNormalSkilIds.Add (item.Key);
			}
		}

		if (allNormalSkilIds.Count == 0) {
			return 0;
		}

		return allNormalSkilIds [allNormalSkilIds.Count - 1];

	}
    /*判断是否可以放下一个技能*/
    public bool CanUseSkill()
    {
        if (CurUsingSkillSyncData == null)
            return true;

        if (_character.IsDead || _character.StateControl.CurState.StateType == StateType.Hit || 
            CurUsingSkillSyncData.CurTime < CurUsingSkillSyncData.SkillData.LinkSkillTriggerTime || CurUsingSkillSyncData.SkillData.LinkSkillId == 0)
        {
            //Debug.LogError("无法触发技能，死亡、受击、释放链接技能时间不到、没有链接为0");
            return false;
        }
        if (!_skillSyncDatas.ContainsKey(CurUsingSkillSyncData.SkillData.LinkSkillId))
        {
            //Debug.LogError("找不到链接技能情况");
            return false;
        }
        return true;
    }
    /*当前使用的技能类型*/
    public SkillType GetCurUseSkillType()
    {
        if (CurUsingSkillSyncData != null)
        {
            return (SkillType)CurUsingSkillSyncData.SkillData.SkillType;
        }
        return SkillType.None;
    }
    #region 对象属性
    public Dictionary<int, SkillSyncData> SkillSyncData
    {
        get
        {
            return _skillSyncDatas;
        }
    }
    public bool Skilling
    {
        get
        {
            return _skilling;
        }
        set
        {
            _skilling = value;
        }
    }
    #endregion
}

/// <summary>
/// 技能动态数据 
/// </summary>
public class SkillSyncData
{
	private SkillData _skillData;

    /*技能相关音效配置*/
    private CharcaterClip _skillClip;
    public SkillData SkillData
	{
		get
		{
			return _skillData;
		}
	}


	private float _cdTiming;
    #region 对象属性
    /// <summary>
    /// 冷却倒计时
    /// </summary>
    public float CdTiming
    {
        get
        {
            return _cdTiming;
        }
    }

    private float _curTime;
    public float CurTime
    {
        get
        {
            return _curTime;
        }
    }
    public Character Character
    {
        get
        {
            return _character;
        }
    }

    public CharcaterClip SkillClip
    {
        get
        {
            return _skillClip;
        }

        set
        {
            _skillClip = value;
        }
    }
    #endregion


    private Dictionary<int, SkillCompBehaviour> _skillComps = new Dictionary<int, SkillCompBehaviour>();//普通技能

    private Dictionary<int, SkillCompBehaviour> _skillBuffComps = new Dictionary<int, SkillCompBehaviour>();//buff技能

    private Character _character;
	

    public void Init(Character character, int skillId)
	{
		_character = character;
		_skillData = SkillData.FindById (skillId);	
		_cdTiming = 0;
        _curTime = 0;
		_sklling = false;
		_skillComps.Clear ();
        _skillBuffComps.Clear();

        string[] skillCompsStr = _skillData.SkillComps.Split('|');
		for (int i = 0; i < skillCompsStr.Length; i++) {
			int skillCompId = int.Parse (skillCompsStr [i]);
            SkillCompBehaviourType type = (SkillCompBehaviourType)SkillCompData.FindById(skillCompId).SkillCompBehaviourType;

            SkillCompBehaviour skillComp = SkillUtil.CreateSkillCompBehaviour (type) ;
			skillComp.Init (this, skillCompId);
            if(type == SkillCompBehaviourType.Buff)
            {
                _skillBuffComps.Add(skillCompId, skillComp);
            }
            else
            {
                _skillComps.Add(skillCompId, skillComp);
            }
			

		}

        _skillClip = CharcaterClip.FindById(_skillData.MusicComps);

    }
    public void InitData()
    {
        _cdTiming = 0;
        _curTime = 0;
        _sklling = false;
        foreach(var item in _skillComps)
        {
            item.Value.InitData();
        }
        foreach (var item in _skillBuffComps)
        {
            item.Value.InitData();
        }
    }
	public void Update(float _timing)
	{
		
		if (_cdTiming > 0) {
			_cdTiming -= _timing;
			if (_cdTiming < 0) {
				_cdTiming = 0;
			}
            foreach (var item in _skillBuffComps)
            {
                item.Value.Update(_timing);
            }
        }
        

        if (!_sklling) {
			return;
		}

        _curTime += Time.deltaTime;

		foreach (var item in _skillComps) {
			item.Value.Update (_timing);
		}


	}

	private bool _sklling;

	public void TriggerSkill()
	{
		_cdTiming = _skillData.Cd;
        _curTime = 0;
		_sklling = true;
		foreach (var item in _skillComps) {
			item.Value.Trigger ();
		}
        foreach (var item in _skillBuffComps)
        {
            item.Value.Trigger();
        }
    }

	public void OnEnd()
	{
        _curTime = 0;
		_sklling = false;


		foreach (var item in _skillComps) {
			item.Value.OnEndSkill ();
		}
        foreach (var item in _skillBuffComps)
        {
            item.Value.OnEndSkill();
        }
    }
}

public enum SkillType
{
	None = 0,
	NormalSkill = 1,
	Skill = 2,
	UltalSkill = 3,
}