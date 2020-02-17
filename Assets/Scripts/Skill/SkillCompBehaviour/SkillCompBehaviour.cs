using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillCompBehaviour : TimerEventListen
{

	protected SkillSyncData _manager;

	protected SkillCompData _skillCompData;

	/// <summary>
	/// 施法特效数据们 
	/// </summary>
	private List<FxData> _fxDatas = new List<FxData>();
    protected List<FxBase> _fxBases = new List<FxBase>();

    public virtual void Init(SkillSyncData manager, int skillCompId)
	{
		_manager = manager;
		_skillCompData = SkillCompData.FindById (skillCompId);
		_effect = false;

		if (_skillCompData.Fx1 != 0) {
			_fxDatas.Add(FxData.FindById (_skillCompData.Fx1));
		}
		if (_skillCompData.Fx2 != 0) {
			_fxDatas.Add(FxData.FindById (_skillCompData.Fx2));
		}
		if (_skillCompData.Fx3 != 0) {
			_fxDatas.Add(FxData.FindById (_skillCompData.Fx3));
		}

	}
    public virtual void InitData()
    {
        _effect = false;
        if(_timerEvent != null)
        {
            _timerEvent.OnEventEnd();
            _timerEvent = null;
        }
        _fxBases.Clear();
    }

	/// <summary>
	/// 触发点，在技能开始的时候触发 
	/// </summary>
	public virtual void Trigger()
	{
        //Debug.LogError ("技能开始=技能组件开始 : " + _manager.Timing);
        _effect = false;
        _fxBases.Clear();

        for (int i = 0; i < _fxDatas.Count; i++) {

			FxData fxData = _fxDatas [i];

            _fxBases.Add(_manager.Character.CharacterFx.CreateFx (fxData));
		}
	}

	private bool _effect;
	/// <summary>
	/// 结算点 
	/// </summary>
	public virtual void Effect()
	{
		_effect = true;
		//Debug.LogError ("技能组件结算 : " + _manager.Timing);
	}

	public virtual void Update(float _timing)
	{
		if (!_effect) {
			if (_manager.CurTime > _skillCompData.EffectTime) {
				Effect ();
			}
		}
	}

	public void OnEndSkill()
	{
		_effect = true;
        if(_skillCompData.SkillCompBehaviourType == (int)SkillCompBehaviourType.Buff)
        {
            return;
        }
        OnEndFx();

    }

    public void OnEndFx()
    {
        foreach(var item in _fxBases)
        {
            item._liveTimeEnd();
        }
    }

    /*获得哪里计算打击点*/
    public Transform GetCheckPoint()
    {
        return FxUtil.GetBindingTrans((FxBindingType)_skillCompData.CheckPoint, _manager.Character);
    }
}
