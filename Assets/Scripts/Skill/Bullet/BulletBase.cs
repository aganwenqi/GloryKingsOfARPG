using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBase : MonoBehaviour, IpoolObj {

	public Transform Root;
    
	private float _timing;
	// 施法特效数据们 
	private List<FxData> _fxDatas = new List<FxData>();
    //加载好的特效
    private List<FxBase> _fxBases = new List<FxBase>();
    
    private bool _using;

    //被打过的怪物不打它
    private ArrayList _overHurt = new ArrayList();

    /*audioSource 的sid*/
    protected string _sourceSid;
    /*音效配置*/
    private BulletClip _bulletClip;

    /*.......是否触发过...........*/
    private bool _playerFx;//触发粒子
    private bool _linkBt;//触发链接弹道
    public ArrayList OverHurt
    {
        get
        {
            return _overHurt;
        }
    }
    private PoolBase _pool;
	public PoolBase Pool
	{
		get
        { 
			return _pool;
		}
	}
    private BulletUtilData _data;
    public BulletUtilData Data
    {
        get
        {
            return _data;
        }
    }
    private void Awake()
    {
        Collider collider = this.GetComponent<Collider>();
        if (collider != null)
        {
            collider.isTrigger = true;
        }
    }
    /*装在导弹粒子*/
	public virtual void Init(BulletUtilData data, PoolBase pool)
	{
		_pool = pool;
		_data = data;
        InitData();
        if (_fxDatas.Count <= 0)
        {
            if (data.btData.Fx1 != 0)
            {
                _fxDatas.Add(FxData.FindById(data.btData.Fx1));
            }
            if (data.btData.Fx2 != 0)
            {
                _fxDatas.Add(FxData.FindById(data.btData.Fx2));
            }
            if (data.btData.Fx3 != 0)
            {
                _fxDatas.Add(FxData.FindById(data.btData.Fx3));
            }
        }
    }
    public virtual void InitData()
    {
        _timing = 0;
        _using = false;
        _playerFx = false;
        _linkBt = false;
        _fxBases.Clear();
        _fxDatas.Clear();
        _overHurt.Clear();
        _sourceSid = SerialIdManager.Instance.GetSid();
        MusicManager.Instance.AddAudioSource(_sourceSid, this.GetComponent<AudioSource>());
        _bulletClip = BulletClip.FindById(_data.btData.MusicComps);
    }
	public void Trigger()
	{
		this.gameObject.SetActive (true);
        this.transform.position = _data.pos;
        this.transform.eulerAngles = _data.rotate;
		_timing = 0;
		_using = true;
        _playerFx = false;
        _linkBt = false;
        Root.gameObject.SetActive (false);
        for (int i = 0; i < _fxDatas.Count; i++)
        {
            FxData fxData = _fxDatas[i];
            FxBase fxBase = _data.character.CharacterFx.CreateFx(fxData, Root);
            _fxBases.Add(fxBase);
        }
        if(_bulletClip != null)
        {
            /*播放弹道音效*/
            MusicManager.Instance.Play(_sourceSid, Utils.MusicPath + _bulletClip.SkillClip, _bulletClip.DelaySkill);
        }
    }


	public virtual void Update()
	{
		if (_using) {
			_timing += Time.deltaTime;

			if (_timing >= _data.btData.Param4 && !_playerFx) {
				_playFx ();
			}

			if (_timing >= _data.btData.Param3) {
				_liveTimeEnd ();
			}
            if(_timing >= _data.btData.LSTrigger && !_linkBt)
            {
                OnTrrigerLinkBullet();
            }
		}
	}

	private void _playFx()
	{
        _playerFx = true;

        Root.gameObject.SetActive (true);
	}


	protected void _liveTimeEnd()
	{
		Root.gameObject.SetActive (false);
        foreach(var item in _fxBases)
        {
            item._liveTimeEnd();
        }
        
        _pool.UnSpawn (Path, this);

		_using = false;
        MusicManager.Instance.RemoveAudioSource(_sourceSid);
    }
    /*触发链接弹道*/
    protected void OnTrrigerLinkBullet()
    {
        _linkBt = true;
        if(_data.btData.LinkSkillId != 0)
        {
            OnTrigerNextBullet(_data.btData.LinkSkillId);
        }
    }
    /*触发下一个弹道*/
    public virtual void OnTrigerNextBullet(int bulletId)
    {
        Bullet bt = Bullet.FindById(bulletId);
        if(bt == null)
        {
            return;
        }
        BulletUtilData data = new BulletUtilData();
        data.skillSyncData = Data.skillSyncData;
        data.scData = Data.scData;
        data.btData = bt;
        data.character = Data.character;
        data.pos = transform.position;
        data.rotate = transform.eulerAngles;
        BulletManager.Instance.CreateBullet(data, data.btData.Name);
    }
    /*是否附加给对象效果*/
    public void AdditioalState(Character dst)
    {
        if(_data.btData.AdditionEf != 0)
        {
            dst.AddStateControl.AddState(_data.btData.AdditionEf, Data.character);
        }
    }
	#region 对象池对象的接口

	private string _path;

	public string Path {
		get {
			return _path;
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
	}

	#endregion
}
public class BulletUtilData
{
    public SkillSyncData skillSyncData;
    public Bullet btData;
    public SkillCompData scData;
    public Character character;
    public Vector3 pos;
    public Vector3 rotate;
}

