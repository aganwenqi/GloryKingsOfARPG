using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseUI : MonoBehaviour {
    public InfoPanelControl _infoPanelControl; //信息面板
    public SWInfoPanel swInfoPanel;//控制按钮面板
    public GameObject root1;
    public GameObject root2;

    private bool isEnter = true;

    private Character _character;

    public virtual void Init(Character character = null)
    {
        _character = character;
        if (_infoPanelControl)
            _infoPanelControl.Init(this);
        if(swInfoPanel)
            swInfoPanel.Init(this);
    }
    public virtual void InitData()
    { }
    /*打开面板*/
    public virtual void OpenPanel(InfoBtType type)
    {
        if (!root1.activeSelf)
        {
            root1.SetActive(true);
            root2.SetActive(true);
        }
        ChangePanel(type);
    }
    /*切换信息面板*/
    public virtual void ChangePanel(InfoBtType type)
    {
        if (_infoPanelControl)
            _infoPanelControl.ChangePanel(type);
    }
    public virtual void OnEnter()
    {
        isEnter = true;
        root1.SetActive(true);
        root2.SetActive(true);
    }
    public virtual void OnQuit()
    {
        isEnter = false;
        root1.SetActive(false);
        root2.SetActive(false);
    }
    #region 对象属性
    public bool IsEnter
    {
        get
        {
            return isEnter;
        }

        set
        {
            isEnter = value;
        }
    }

    public Character Character
    {
        get
        {
            return _character;
        }

        set
        {
            _character = value;
        }
    }
    #endregion
}
