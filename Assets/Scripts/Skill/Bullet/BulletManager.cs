using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager {

	private static BulletManager _instance;
	public static BulletManager Instance
	{
		get{ 
			if (_instance == null) {
				_instance = new BulletManager();
			}
			return _instance;
		}
	}

	public PoolBase _btPool = new PoolBase();


	public BulletBase CreateBullet(BulletUtilData data, string name)
	{
        BulletBase bt = _btPool.Spawn (Utils.BulletPath + name) as BulletBase;
        bt.Init (data, _btPool);
        bt.Trigger ();
		return bt;
	}

}
