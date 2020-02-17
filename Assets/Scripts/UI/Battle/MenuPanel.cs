using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MenuPanel : MonoBehaviour {
    enum menuType
    {
        Info = 0,//角色信息
        Setting = 1,///设置
    }
	private MenuPanelUI _manager;
    /*头像*/
    public Image Icon;
    /*名字*/
    public Text Name;
    /*血条*/
    public Slider HpSlider;
    public Text HpText;
    /*蓝条*/
    public Slider MpSlider;
    public Text MpText;
	public MenuPanelUI Manager
	{
		get{ 
			return _manager;
		}
	}

	public void Init(MenuPanelUI manager)
	{
		_manager = manager;
        Name.text = _manager.Character.HeroData.Name;
        UpdataUI();
    }
    private void UpdataUI()
    {
        Sprite sprite = null;
        sprite = Resources.Load<Sprite>("Picture/HeroIcon/" + _manager.Character.HeroData.Icon);
        Icon.sprite = sprite;
    }
    /*更新血条和蓝条*/
    public void UpdataHpMp()
    {
        if (_manager.Character.CharacterUtilData.characterType != CharacterType.Player)
        {
            return;
        }
        AttributesControl attControl = _manager.Character.CharacterAttribute.AttControl;
        int hp = (int)attControl.GetAttSignal(AttributeType.Hp);
        int mp = (int)attControl.GetAttSignal(AttributeType.Mp);
        float maxHp = attControl.GetAttSignal(AttributeType.MaxHp);
        float maxMp = attControl.GetAttSignal(AttributeType.MaxMp);
        HpText.text = hp.ToString();
        MpText.text = mp.ToString();
        HpSlider.maxValue = maxHp;
        MpSlider.maxValue = maxMp;
        HpSlider.value = hp;
        MpSlider.value = mp;

        
    }
    /*有菜单按钮按下*/
    public void Invoke_Menu(int type)
    {
        menuType t = (menuType)type;
        switch(t)
        {
            case menuType.Info:
                UICanvasManager.Instance.OnEnterUI(InformationUI.Instance);
                InformationUI.Instance.OpenPanel(InfoBtType.attribute);
                MusicUtil.PlayClip(UICanvasManager.Instance.SourceSid, SourceClip.openBt);
                break;
            case menuType.Setting:
                UICanvasManager.Instance.OnEnterUI(SettingUI.Instance);
                SettingUI.Instance.OpenPanel(InfoBtType.settingFirst);
                MusicUtil.PlayClip(UICanvasManager.Instance.SourceSid, SourceClip.openBt);
                break;
        }
    }
}
