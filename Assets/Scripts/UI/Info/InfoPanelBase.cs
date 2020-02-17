using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoPanelBase : MonoBehaviour {

    public InfoBtType _infoType;

    internal InfoBtType InfoType
    {
        get
        {
            return _infoType;
        }

        set
        {
            _infoType = value;
        }
    }
    BaseUI _manager;
    public BaseUI Manager
    {
        get
        {
            return _manager;
        }


    }

    public virtual void Init(BaseUI manager)
    {
        _manager = manager;
    }
    public virtual void OnEnter()
    {

    }
    public virtual void OnExit()
    {

    }
}
