using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillPanel : MonoBehaviour {

	private BattleUI _manager;
	public BattleUI Manager
	{
		get{ 
			return _manager;
		}
	}


	public RectTransform NormalSkillRoot;
	public List<RectTransform> SkillsRoot;
	private List<KeyCode> _skillKeyCodes = new List<KeyCode>();

	private List<SkillBtnItem> _items = new List<SkillBtnItem>();

	public void Init(BattleUI manager)
	{
		_manager = manager;

		for (int i = 0; i < _items.Count; i++) {
			Destroy (_items [i].gameObject);
		}
		_items.Clear ();

		_skillKeyCodes.Add (KeyCode.J);
		_skillKeyCodes.Add (KeyCode.K);
		_skillKeyCodes.Add (KeyCode.L);
		_skillKeyCodes.Add (KeyCode.M);

		//获取普攻动态数据
		SkillSyncData normalData = null;
		foreach (var item in _manager.Character.CharacterSkill.SkillSyncData) {

			if (item.Value.SkillData.SkillType == (int)SkillType.NormalSkill) {
				normalData = item.Value;
				break;
			}
		}

		if (normalData != null) {
			CreateItem (normalData, NormalSkillRoot, true, KeyCode.Space);
		}

		List<SkillSyncData> skillDatas = new List<SkillSyncData> ();
		foreach (var item in _manager.Character.CharacterSkill.SkillSyncData) {

			if (item.Value.SkillData.SkillType != (int)SkillType.NormalSkill) {
				skillDatas.Add(item.Value);
			}
		}

		for (int i = 0; i < skillDatas.Count; i++) {
			if (SkillsRoot.Count >= i + 1) {
				CreateItem (skillDatas[i], SkillsRoot[i], true, _skillKeyCodes[i]);
			}
		}
	}


	public void CreateItem(SkillSyncData data, RectTransform parent, bool useBac = true, KeyCode keyCode = KeyCode.None)
	{
		SkillBtnItem skillBtnItem = ((GameObject)Instantiate ((Resources.Load ("Prefab/UI/Item/" + "SkillBtnItem") as GameObject))).GetComponent<SkillBtnItem> ();
		skillBtnItem.transform.SetParent (parent);
		(skillBtnItem.transform as RectTransform).anchoredPosition3D = Vector3.zero;
		skillBtnItem.transform.localScale = Vector3.one;
		skillBtnItem.transform.localEulerAngles = Vector3.zero;

		skillBtnItem.Init (this, data, useBac, keyCode);
	}



}
