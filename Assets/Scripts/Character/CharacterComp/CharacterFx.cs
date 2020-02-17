using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CharacterFx : CharacterCompBase {

    private List<FxBase> usedFx = new List<FxBase>();
    /*粒子数据，如果传入父对象就用这个父，不是就找角色根节点*/
	public FxBase CreateFx(FxData fxData, Transform parent = null)
	{
		FxUtilData data = new FxUtilData ();

        if(parent)
        {
            data.Parent = parent;
        }
        else
        {
            data.Parent = FxUtil.GetBindingTrans((FxBindingType)fxData.BindingType, _character);
        }
		data.Delay = fxData.Delay;
		data.Offset = Vector3.zero;
        FxBase fx = FxMananger.Instance.CreateFx(data, fxData.Name);
        usedFx.Add(fx);
        return fx;
	}
    public override void InitData()
    {
        base.InitData();
        foreach(FxBase item in usedFx)
        {
            if(!item.Equals(null))
            {
                item.InitData();
            }
        }
        usedFx.Clear();
    }
}
