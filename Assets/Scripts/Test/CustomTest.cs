using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomTest : MonoBehaviour {
	PoolBase _pool = new PoolBase();
	// Use this for initialization
	void Start () {
		//HeroData.FindById (1001);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.O)) {
			FxBase fx = _pool.Spawn (Utils.FxPath + "mt_skill1") as FxBase;
			fx.Init (new FxUtilData (), _pool);
			fx.Trigger ();
		}
	}
}
