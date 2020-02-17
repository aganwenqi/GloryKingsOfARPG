using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillBtnItem : MonoBehaviour {

	public List<Image> Imgs;

	public Image Mask;

	public Image Bac;

	protected SkillSyncData _data;
	protected bool _useBac;
	protected SkillPanel _manager;

	private KeyCode _keyCode;

	public void Init(SkillPanel manager, SkillSyncData data, bool useBac = true, KeyCode keyCode = KeyCode.None)
	{
		_manager = manager;
		_data = data;
		_useBac = useBac;
		_keyCode = keyCode;
		UpdateUI ();
	}

	public void UpdateUI()
	{
		Sprite sprite = null;

		if (string.IsNullOrEmpty (_data.SkillData.Icon)) {
			sprite = Resources.Load<Sprite> ("Picture/SkillIcon/" + _data.SkillData.Icon);
		} 
		else 
		{
			sprite = Resources.Load<Sprite> ("Picture/SkillIcon/" + _data.SkillData.Icon);
		}

		for (int i = 0; i < Imgs.Count; i++) {
			Imgs [i].sprite = sprite;
			Imgs [i].SetNativeSize ();
		}

		Bac.gameObject.SetActive (_useBac);
	}

	public void Update()
	{
		Mask.fillAmount = (1 - _data.CdTiming / _data.SkillData.Cd);

		if (Input.GetKeyDown(_keyCode)) {
			Invoke_Skill ();
		}
	}

	public void Invoke_Skill()
	{
        _manager.Manager.Character.CharacterSkill.UseSkill(_data);
    }
}
