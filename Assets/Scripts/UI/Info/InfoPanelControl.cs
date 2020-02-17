using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoPanelControl : MonoBehaviour {

    private Dictionary<InfoBtType, InfoPanelBase> _infoPanels = new Dictionary<InfoBtType, InfoPanelBase>();
    private InfoPanelBase _curPanel;
    BaseUI _manager;
    public Dictionary<InfoBtType, InfoPanelBase> InfoPanels
    {
        get
        {
            return _infoPanels;
        }

        set
        {
            _infoPanels = value;
        }
    }
    public void Init(BaseUI manager)
    {
        _manager = manager;
        InfoPanelBase[] items = this.GetComponentsInChildren<InfoPanelBase>();
        foreach (InfoPanelBase item in items)
        {
            item.Init(manager);
            item.gameObject.SetActive(false);
            _infoPanels.Add(item.InfoType, item);
        }
    }
    /*切换信息面板*/
    public void ChangePanel(InfoBtType type)
    {
        if(_curPanel != null && _curPanel.InfoType == type)
        {
            return;
        }
        InfoPanelBase item = null;
        if (_infoPanels.TryGetValue(type, out item))
        {
            if (_curPanel != null)
            {
                _curPanel.OnExit();
                _curPanel.gameObject.SetActive(false);
            }
            _curPanel = item;
            _curPanel.gameObject.SetActive(true);
            _curPanel.OnEnter();
        }
    }
}
