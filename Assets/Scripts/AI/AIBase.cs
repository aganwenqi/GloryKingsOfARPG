
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AIBase : CharacterCompBase
{


    /*AI行为触发配置*/
    protected CharacterAI _characterAI;

    /*当前跟踪的目标*/
    protected Character _curTarget;

    /*攻击目标枚举*/
    protected AttackTarget _attackTarget;

    /*...........以下避免GC预缓存...............*/
    private Vector3 _dirToTarget = new Vector3();

    private List<SkillSyncData> _canUseSkill = new List<SkillSyncData>();
    private int[] _weights = new int[8];//预计最多有8个技能
    private int[] _randoms = new int[8];//释放概率
    public override void Init(Character _manager)
    {
        base.Init(_manager);
        _curTarget = null;
        
        _characterAI = CharacterAI.FindById(_character.HeroData.AI);
        _attackTarget = (AttackTarget)_character.HeroData.Target;
    }
    public override void InitData()
    {
        base.InitData();
    }
    public override void Update(float _timing) {
        base.Update(_timing);
    }

    

    /*SkillAIWeight权重表计算释放技能*/
    protected SkillSyncData PlaySkillOfAI(float dis)
    {
        Dictionary<int, SkillSyncData> skillSync = _character.CharacterSkill.SkillSyncData;
        if (skillSync == null && skillSync.Count <= 0)
            return null;

        /*先计算哪些技能达到释放距离*/
        _canUseSkill.Clear();
        int count = 0;
        int num = 0;
        SkillAIWeight skillAI = SkillAIWeight.FindById(_character.HeroData.SkillWeight);
        if(skillAI == null)
        {
            //Debug.LogError("没有权重:" );
            return null;
        }
        UnityAction<int, int, int> curFunc = (int id, int weight, int random) => 
        {
            SkillSyncData skill = null;
            if(skillSync.TryGetValue(id, out skill))
            {
                if (skill.SkillData.ReleaseDis >= dis && skill.CdTiming == 0)
                {
                    
                    _canUseSkill.Add(skill);
                    _randoms[count] = random;/*触发概率*/
                    _weights[count] = weight;/*权重分布*/
                    _weights[count] += num;
                    num += weight;
                    count++;
                }
            }
        };
        if (skillAI.Skill0ID != 0)
            curFunc(skillAI.Skill0ID, skillAI.Skill0, skillAI.Skill0Random);
        if(skillAI.Skill1ID != 0)
            curFunc(skillAI.Skill1ID, skillAI.Skill1, skillAI.Skill1Random);

        if (skillAI.Skill2ID != 0)
            curFunc(skillAI.Skill2ID, skillAI.Skill2, skillAI.Skill2Random);

        if (skillAI.Skill3ID != 0)
            curFunc(skillAI.Skill3ID, skillAI.Skill3, skillAI.Skill3Random);

        /*计算释放哪个技能*/
        SkillSyncData useSkill = null;
        int rand = Random.Range(0, num);
        int left = 0;
        for(int i = 0; i < _canUseSkill.Count; i++)
        {
            if(left <= rand && rand < _weights[i])
            {
                if(_randoms[i] >= Random.Range(0, 100)) //根据概率看是否可以放这个技能
                {
                    useSkill = _canUseSkill[i];
                }
                break;
            }
            left = _weights[i];
        }
        return useSkill;
    }
    /*看着目标,返回true正中目标*/
    protected bool LookTarget(Character dst, out Quaternion ang)
    {
        ang = Quaternion.identity;
        if (_curTarget == null)
            return false;
        _dirToTarget.Set(_character.transform.position.x, dst.transform.position.y, _character.transform.position.z);
        _dirToTarget = dst.transform.position - _dirToTarget;
        ang = Quaternion.LookRotation(_dirToTarget);
        if (Quaternion.Angle(_character.transform.rotation, ang) > _characterAI.AttackAngle) //瞄准玩家角度
        {
            //_character.transform.rotation = Quaternion.Lerp(_character.transform.rotation, ang, Time.deltaTime * 10);
            return false;
        }
        return true;
    }
    #region 属性链接
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
