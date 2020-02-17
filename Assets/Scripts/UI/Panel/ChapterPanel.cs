using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ChapterPanel : InfoPanelBase
{
    /*加载的副本按钮*/
    public List<Transform> dupBtPoint = new List<Transform>();
    private Dictionary<int, DupBtnItem> _dupBtnItems = new Dictionary<int, DupBtnItem>();

    /*章节名字*/
    public Text charpterName;

    private DupSwitch _dupSwitch;
    public override void Init(BaseUI manager)
    {
        base.Init(manager);
        _dupBtnItems.Clear();

        //TODO章节按钮加载
        _dupSwitch = DupSwitch.FindById((int)_infoType - 100);
        string[] dupId = _dupSwitch.DupIds.Split('、');
        for(int i = 0; i < dupId.Length; i++)
        {
            if (i > dupBtPoint.Count - 1)
                break;
            int id = int.Parse(dupId[i]);
            DupBtnItem dupBtnItem = CreateItem(int.Parse(dupId[i]), dupBtPoint[i]);
            _dupBtnItems.Add(id, dupBtnItem);
        }

    }
    public override void OnEnter()
    {
        base.OnEnter();
        if (charpterName != null)
            charpterName.text = _dupSwitch.ChapterName;
    }
    public override void OnExit()
    {
        base.OnExit();
    }
    public DupBtnItem CreateItem(int id, Transform parent)
    {
        DupBtnItem item = ((GameObject)Instantiate((Resources.Load("Prefab/UI/Dup/" + "dupBtItem") as GameObject))).GetComponent<DupBtnItem>();
        item.transform.SetParent(parent);
        (item.transform as RectTransform).localPosition = Vector3.zero;
        item.transform.localScale = Vector3.one;
        item.transform.localEulerAngles = Vector3.zero;
        item.Init(this, Dup.FindById(id));
        return item;
    }
}
