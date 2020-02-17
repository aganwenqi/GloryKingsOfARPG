using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FxBase : MonoBehaviour, IpoolObj {

	public Transform Root;
	public float LifeTime = 0;
	private FxUtilData _data;

	private float _timing;

	private bool _using;

	private PoolBase _pool;
	public PoolBase Pool
	{
		get{ 
			return _pool;
		}
	}

	public FxBase()
	{
			
	}


	public void Init(FxUtilData data, PoolBase pool)
	{
		_pool = pool;
		_data = data;
		_timing = 0;
		_using = false;
	}
    public void InitData()
    {
        _liveTimeEnd();
    }
	public void Trigger()
	{
		if (_data == null) {
			_data = new FxUtilData ();
		}
		this.gameObject.SetActive (true);
		this.transform.parent = _data.Parent;
		this.transform.localPosition = _data.Offset;
        this.transform.localScale = Vector3.one;
        this.transform.localEulerAngles = Vector3.zero;
		_timing = 0;
		_using = true;
		Root.gameObject.SetActive (false);

	}


	public void Update()
	{
		if (_using) {
			_timing += Time.deltaTime;

			if (_timing >= _data.Delay) {
				_playFx ();
			}

			if (_timing >= LifeTime) {
				_liveTimeEnd ();
			}

		}
	}

	private void _playFx()
	{
		Root.gameObject.SetActive (true);
	}


	public void _liveTimeEnd()
	{
		Root.gameObject.SetActive (false);

		_pool.UnSpawn (Path, this);

		_using = false;
	}
    private void OnDestroy()
    {
        FxMananger.Instance.DeleteFx(this);
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

    public FxUtilData Data
    {
        get
        {
            return _data;
        }

        set
        {
            _data = value;
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


public class FxUtilData
{
	public Transform Parent;
	public float Delay;
	public Vector3 Offset;
}
