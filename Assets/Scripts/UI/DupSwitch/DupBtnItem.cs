using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DupBtnItem : MonoBehaviour {

	public Image _UI;
    public Text _DupName;
	protected Dup _data;
	protected ChapterPanel _manager;

	public void Init(ChapterPanel manager, Dup data)
	{
		_manager = manager;
		_data = data;
        _DupName.text = _data.SceneName;
        UpdateUI ();
	}

	public void UpdateUI()
	{
		Sprite sprite = null;

        sprite = Resources.Load<Sprite>("Picture/DupSwitch/Chapter/" + _data.SwitchUI);
        _UI.sprite = sprite;

    }

	public void Invoke_UI()
	{
        //切换场景
        if(DupManager.Instance.CurDupId != _data.Id)
        {
            SceneController.ChangeScene(_data.Id);
            List<BaseUI> showUI = new List<BaseUI>();
            showUI.Add(BattleUI.Instance);
            showUI.Add(MenuPanelUI.Instance);
            UICanvasManager.Instance.OnEnterUI(showUI);
        }
    }
}
