using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*这里只负责打怪和移动*/
public class BulletNorml : BulletBase {

    private string _overTag = "Bullet";
    public override void Init(BulletUtilData data, PoolBase pool)
	{
        base.Init(data, pool);
    }
    public override void InitData()
    {
        base.InitData();
    }
    public override void Update()
    {
        base.Update();
        transform.Translate(Vector3.forward * Data.btData.Param2 * Time.deltaTime);
    }
    /*有敌人进来*/
    private void OnTriggerEnter(Collider other)
    {
        
        Character scr = Data.character;
        Character dst = other.GetComponent<Character>();

        if (scr == dst || other.transform == transform)//不能打自己
        {
            return;
        }
        BulletNorml bt = other.GetComponent<BulletNorml>();
        if (bt != null && bt.Data.character == scr)//不是自己的子弹
        {
            return;
        }

        bool result = AttackDst(scr, dst, other);//是否打到
        if (Data.btData.OverObstacle == 1)//穿过
        {
            if (Data.btData.TriggleBt != 0)//-1打中直接消失，0没有链接弹道，其他有触发下一个弹道需要消失自己
            {
                if(result)
                {
                    OnTrigerNextBullet(Data.btData.TriggleBt);
                    _liveTimeEnd();
                }
            }
        }
        else
        {
            if (Data.btData.TriggleBt != 0)//-1打中直接消失，0没有链接弹道，其他有触发下一个弹道需要消失自己
            {
                if(dst && !CharacterManager.Instance.IsAttackTarget(scr, dst))
                {
                    return;//不攻击其他目标
                }
                OnTrigerNextBullet(Data.btData.TriggleBt);
                _liveTimeEnd();
            }
        }

        if (result)
        {
            //打到了播放音乐
            MusicManager.Instance.Play(_sourceSid, Utils.MusicPath + Data.skillSyncData.SkillClip.HitClip, Data.skillSyncData.SkillClip.DelayHit, false, SourceType.norml);
        }

    }
    private bool AttackDst(Character scr, Character dst, Collider other)
    {
        if (!CharacterManager.Instance.CanAttack(scr, dst))
        {
            return false;
        }

        if (OverHurt.Contains(other))
        {
            /*已经打过它了*/
            return false;
        }
        /*伤害计算*/
        OverHurt.Add(other);
        SkillCompBehaviourType type = (SkillCompBehaviourType)Data.scData.SkillCompBehaviourType;
        HurtResult hurt = scr.PrintHurt(Data.btData, dst.CharacterAttribute.AttControl, type);
        dst.Hit(hurt, Data.btData, Data.skillSyncData, type);

        /*附加效果*/
        AdditioalState(dst);
        return true;
    }
}

