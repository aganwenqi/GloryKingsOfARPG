using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FxMananger {

	private static FxMananger _instance;
	public static FxMananger Instance
	{
		get{ 
			if (_instance == null) {
				_instance = new FxMananger ();
			}
			return _instance;
		}
	}

	public PoolBase _fxPool = new PoolBase();


	public FxBase CreateFx(FxUtilData data, string name)
	{
		FxBase fx = _fxPool.Spawn (Utils.FxPath + name) as FxBase;
		fx.Init (data, _fxPool);
		fx.Trigger ();
		return fx;
	}
    public void DeleteFx(FxBase fx)
    {
        _fxPool.DeleteSpawn(fx.Path, fx);
    }
}
