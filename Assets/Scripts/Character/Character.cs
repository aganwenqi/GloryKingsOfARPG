
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
//int 5             float  0.583f             string  "sdfsdf"

/// <summary>
/// 角色基类 包括英雄，怪物等等等
/// </summary>
public class Character : MonoBehaviour, IpoolObj {
    public DissolveContro []_dissolve;

	public Animator Animator;
    public Transform weapon;//武器节点
    public Transform weapon2;//负武器节点
    public Transform hitPoint;//受伤特效点
    public NavMeshAgent _nav;
    private HeroData _heroData;
    private bool _isDead;

    private bool _DontUse;//不可使用其他操作

    /// <summary>
    /// 属性model类池
    /// </summary>
    PoolBaseObj<Dictionary<AttributeType, float>> _attributePool = new PoolBaseObj<Dictionary<AttributeType, float>>();
    /// <summary>
    /// 动作组件 
    /// </summary>
    private CharacterAnim _characterAnim = new CharacterAnim();
    /// <summary>
	/// 技能组件 
	/// </summary>
	private CharacterSkill _characterSkill = new CharacterSkill();
    /// <summary>
	/// 特效组件 
	/// </summary>
	private CharacterFx _characterFx = new CharacterFx();
    /// <summary>
	/// 飘字组件 
	/// </summary>
	private CharacterFlyFont _characterFly = new CharacterFlyFont();
    /// <summary>
	/// 玩家属性
	/// </summary>
	private CharacterAttribute _characterAttribute = new CharacterAttribute();
    /// <summary>
	/// 区分角色类型的组件控制器
	/// </summary>
	private CharacterTypeBase _characterTypeInstance;
    /// <summary>
	/// 状态控制器
	/// </summary>
	private StateControl _stateControl = new StateControl();
    /// <summary>
	/// 附加状态控制器
	/// </summary>
	private AdditioalStateControl _addStateControl = new AdditioalStateControl();
    /// <summary>
	/// 玩家副本
	/// </summary>
	private CharacterDup _characterDup = new CharacterDup();
    /// <summary>
    /// 行为AI
    /// </summary>
    private CharacterAIControl _aiControl = new CharacterAIControl();
    /// <summary>
    /// 音效管理
    /// </summary>
    private CharacterSound _characterSound = new CharacterSound();

    //是否开启AI
    private bool _openAI;

    #region 当前属性
    public CharacterUtilData CharacterUtilData;
	#endregion

	public virtual void Init(CharacterUtilData data)
	{
		CharacterUtilData = data;
		_heroData = HeroData.FindById (CharacterUtilData.heroId);
		_characterAnim.Init (this);
        _characterAttribute.Init (this);
		_characterSkill.Init (this);
		_characterFx.Init (this);
        _characterFly.Init(this);

        _characterTypeInstance = CharacterUtil.GetCharaterTypeInstance (CharacterUtilData.characterType);
		_characterTypeInstance.Init (this);

        _stateControl.Init (this);
        _addStateControl.Init(this);
        _aiControl.Init(this);
        _characterSound.Init(this);
        _openAI = CharacterUtil.IsOpenAI(data.characterType);

        _characterDup.Init(this);
    }
    //切换场景需要的初始化
    public virtual void InitData(Vector3 position, Quaternion rotate)
    {
        _isDead = false;
        if (!this.gameObject.activeSelf)
            this.gameObject.SetActive(true);

        _characterSkill.InitData();
        _characterFx.InitData();
        _characterFly.InitData();
        _characterAttribute.InitData();
        _characterAnim.InitData();
        _characterDup.InitData();
        _stateControl.InitData();
        _addStateControl.InitData();
        _aiControl.InitData();
        _characterSound.InitData();
        foreach (var item in _dissolve)
        {
            item.InitNorml();
        }
        SetCollider(true);
        if (this._nav)
        {
            if (!_openAI)
            {
                this._nav.enabled = false;
            }
            else
            {
                this._nav.enabled = true;
            }
        }
        this.transform.position = position;
        this.transform.rotation = rotate;
        _DontUse = false;
    }
    /// <summary>
	/// 获得输出伤害
    /// 1技能，2目标属性，3攻击类型
	/// </summary>
    public HurtResult PrintHurt(DataBase skillData, AttributesControl dst, SkillCompBehaviourType type)
    {
        return HurtUtils.HurtCalculation(this.CharacterAttribute.AttControl, dst, skillData, type);
    }
	/// <summary>
	/// 被攻击
	/// </summary>
	public void Hit(HurtResult hurt,DataBase data , SkillSyncData skill,SkillCompBehaviourType type)
	{
        if (_isDead)
            return;
        StateBase hit = null;
        if (CharacterUtilData.characterType == CharacterType.Player && skill.SkillData.SkillType == (int)SkillType.NormalSkill)
        {
            //是玩家，被普通攻击击中不打断攻击
            _stateControl.States.TryGetValue(StateType.Hit, out hit);
        }
        else
        {
            if (!_DontUse)
            {
                OnEndState();//打断,普通攻击不打断
                _stateControl.ChangeState(StateType.Hit);
                hit = _stateControl.CurState;
            }
        }
        if (hit != null)
        {
            //Debug.LogError("hitttttttttttttttttttttttt");
            (hit as StateHit).ChangeHit(hurt, data, type);
        } 
    }
    /// <summary>
	/// 直接伤害，不改变状态
	/// </summary>
	public void Hit(HurtResult hurt)
    {
        if (_isDead)
            return;
        StateBase hit;
        _stateControl.States.TryGetValue(StateType.Hit, out hit);
        if (hit != null)
        {
            //Debug.LogError("直接伤害httttttt");
            (hit as StateHit).ChangeHit(hurt);
        }
    }
    /*结束状态*/
    public void OnEndState()
    {
        if (_characterSkill.CurUsingSkillSyncData != null)//打断,普通攻击不打断
        {
            _characterSkill.OnEndSkill();
        }
        _stateControl.ChangeState(StateType.Idle);
    }
    /// <summary>
    /// 死亡
    /// </summary>
    public virtual void Dead()
    {
        _isDead = true;
        for(int i = 0; i < _dissolve.Length; i++)
        {
            if(i == 0)
            {
                _dissolve[i].Play(() => {
                    CharacterManager.Instance.RemoveCharacter(this);
                });
            }
            else
            {
                _dissolve[i].Play(null);
            }
        }
        
        SetCollider(false);
    }
    public void SetCollider(bool isCollider)
    {
        Rigidbody rig = GetComponent<Rigidbody>();
        Collider col = GetComponent<Collider>();
        if (rig != null)
        {
            rig.isKinematic = !isCollider;
        }
        if (col != null)
        {
            col.enabled = isCollider;
        }
        if (_nav)
        {
            _nav.enabled = isCollider;
        }
    }
    private void FixedUpdate()
    {
        _stateControl.FixedUpdate(Time.deltaTime);
    }
    public virtual void Update () {
        if (_isDead)
            return;
        float _timing = Time.deltaTime;
        if (_openAI)
            _aiControl.Update(Time.deltaTime);

        _characterAnim.Update (_timing);
        _characterAttribute.Update (_timing);
		_characterSkill.Update (_timing);
        _characterSound.Update(_timing);
        _characterFx.Update (_timing);
        _characterFly.Update(_timing);
        _stateControl.Update (_timing);
        _addStateControl.Update(_timing);
        _characterDup.Update(_timing);
    }

    public virtual void LateUpdate()
	{
	}
    private void OnDestroy()
    {
        CharacterManager.Instance.DeleteCharacter(this);
    }
    #region 对象池对象的接口

    private string _path;

	public string Path {
		get {
			return  _path;
		}
		set
		{ 
			_path = value;
		}
	}

    public void OnSpawn()
	{
		this.gameObject.SetActive (true);
	}

	public void OnUnSpawn()
	{

		this.gameObject.SetActive (false);
        _characterAnim.UnSpawn();
        _characterAttribute.UnSpawn();
        _characterSkill.UnSpawn();
        _characterFx.UnSpawn();
        _characterFly.UnSpawn();
        _characterDup.UnSpawn();
        _characterTypeInstance.UnSpawn();
    }

    #endregion

    #region 所有组件属性
    public PoolBaseObj<Dictionary<AttributeType, float>> AttributePool
    {
        get
        {
            return _attributePool;
        }
    }

    public CharacterAnim CharacterAnim
    {
        get
        {
            return _characterAnim;
        }
    }

    public CharacterSkill CharacterSkill
    {
        get
        {
            return _characterSkill;
        }
    }

    public CharacterFx CharacterFx
    {
        get
        {
            return _characterFx;
        }
    }

    public CharacterAttribute CharacterAttribute
    {
        get
        {
            return _characterAttribute;
        }

        set
        {
            _characterAttribute = value;
        }
    }

    public CharacterTypeBase CharacterTypeInstance
    {
        get
        {
            return _characterTypeInstance;
        }
    }

    public StateControl StateControl
    {
        get
        {
            return _stateControl;
        }
    }



    public HeroData HeroData
    {
        get
        {
            return _heroData;
        }
    }


    public bool IsDead
    {
        get
        {
            return _isDead;
        }
    }

    public Transform Weapon
    {
        get
        {
            return weapon;
        }

        set
        {
            weapon = value;
        }
    }

    public CharacterDup CharacterDup
    {
        get
        {
            return _characterDup;
        }

        set
        {
            _characterDup = value;
        }
    }
    public CharacterAIControl AiControl
    {
        get
        {
            return _aiControl;
        }

        set
        {
            _aiControl = value;
        }
    }

    public bool OpenAI
    {
        get
        {
            return _openAI;
        }

        set
        {
            _openAI = value;
        }
    }

    public CharacterFlyFont CharacterFly
    {
        get
        {
            return _characterFly;
        }

        set
        {
            _characterFly = value;
        }
    }

    public Transform Weapon2
    {
        get
        {
            return weapon2;
        }

        set
        {
            weapon2 = value;
        }
    }

    public AdditioalStateControl AddStateControl
    {
        get
        {
            return _addStateControl;
        }

        set
        {
            _addStateControl = value;
        }
    }

    public CharacterSound CharacterSound
    {
        get
        {
            return _characterSound;
        }

        set
        {
            _characterSound = value;
        }
    }

    public bool DontUse
    {
        get
        {
            return _DontUse;
        }

        set
        {
            _DontUse = value;
        }
    }
    #endregion
}

/// <summary>
/// 角色实时数据 
/// </summary>
public class CharacterUtilData
{
	public string sid;
	public int heroId;
    public string modelPath;
	public CharacterType characterType;
}
