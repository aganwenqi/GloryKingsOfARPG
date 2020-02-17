using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DupResultUI : BaseUI {
    static public DupResultUI _instance;
    static public DupResultUI Instance
    {
        get
        {
            return _instance;
        }
    }

    public Image Bg;
    public Image BgText;
    void Awake()
	{
        _instance = this;
	}

	public override void Init(Character character)
	{
        base.Init(character);
        
    }
    public override void OnEnter()
    {
        base.OnEnter();
        Sprite bg = null;
        Sprite bgText = null;
        if(Character.IsDead)
        {
            bg = Resources.Load<Sprite>("Picture/DupResult/fail");
            bgText = Resources.Load<Sprite>("Picture/DupResult/failText");
            BgText.color = new Color(100, 167, 182, 133);
        }
        else
        {
            bg = Resources.Load<Sprite>("Picture/DupResult/cross");
            bgText = Resources.Load<Sprite>("Picture/DupResult/crossText");
            BgText.color = new Color(99, 255, 162, 133);
        }
        Bg.sprite = bg;
        BgText.sprite = bgText;
    }
    /*退出按下后回到主场景*/
    public override void OnQuit()
    {
        base.OnQuit();
        if (this.Character != null && DupManager.Instance.CurDupId != 1000)
        {
            SceneController.ChangeScene(1000);
        }
    }
}
