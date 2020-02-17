using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class BattleAxisPanel : MonoBehaviour {

	public AxisBtn AxisBtn;

	private BattleUI _manager;
	public BattleUI Manager
	{
		get{
			return _manager;
		}
	}

	public void Init(BattleUI manager)
	{
		_manager = manager;
		AxisBtn.Init (this);
	}


}
