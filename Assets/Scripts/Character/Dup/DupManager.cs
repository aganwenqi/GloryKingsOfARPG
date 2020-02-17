using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DupManager
{
    private static DupManager _instance = null;

    /*存储解析副本表数据字典*/
    public PoolBaseObj<Dictionary<int, int>> DicIIPool = new PoolBaseObj<Dictionary<int, int>>();

    /*当前副本配置*/
    private int _curDupId = -1;

    /*第一次进入主城*/
    public bool FristIn = true;

    /*..................切换场景必须设置....................*/
    public void SetDupId(int dupId)
    {
        _curDupId = dupId;
    }
    #region 对象属性
    public static DupManager Instance
    {
        get
        {
            if (_instance == null)
                _instance = new DupManager();
            return _instance;
        }
    }
    public int CurDupId
    {
        get
        {
            return _curDupId;
        }

        set
        {
            _curDupId = value;
        }
    }
    #endregion
}
