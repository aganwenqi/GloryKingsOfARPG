using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 范围技能攻击
/// </summary>
public class SkillCompBehaviourNormlBullet : SkillCompBehaviour {

	public override void Init (SkillSyncData manager, int skillCompId)
	{
		base.Init (manager, skillCompId);
	}

	public override void Trigger ()
	{
		base.Trigger ();
	}

    //开始发射弹道
    public override void Effect ()
	{
        base.Effect();
        BulletUtilData data = new BulletUtilData();
        data.skillSyncData = _manager;
        data.scData = _skillCompData;
        data.btData = Bullet.FindById((int)_skillCompData.Param1);
        data.character = _manager.Character;
        Transform target = FxUtil.GetBindingTrans((FxBindingType)data.btData.TransformBehaviourType, _manager.Character);
        data.pos = target.transform.position;
        data.rotate = target.transform.eulerAngles;
        BulletManager.Instance.CreateBullet(data, data.btData.Name);
    }

}
