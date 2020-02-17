using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class CharacterAIControl : AIBase
{
    enum BehaviorState
    {
        idle,
        pursue,
        rotate,//原地转动状态
        attack,
    }
    private NavMeshAgent nav;

    /*当前状态*/
    BehaviorState _curState;

    /*帧间隔，用于每个多少帧刷新以下AI*/
    private int curFps;

    /*攻击间隔*/
    private float _curAttackTime;
    private bool _canNormlAttack;

    /*出生停顿间隔*/
    private float _curBeginTime;
    public override void Init(Character _manager)
    {
        base.Init(_manager);
        if(_manager._nav != null)
        {
            nav = _manager._nav;
        }
        InitData();
    }
    public override void InitData()
    {
        base.InitData();
        _curState = BehaviorState.idle;
        curFps = 0;
        if(_characterAI == null)
        {
            Debug.LogError(_character.HeroData.Name + "的_characterAI为空");
            return;
        }
        _curAttackTime = _characterAI.AttackInterval;
        _canNormlAttack = true;
        _curBeginTime = 0;
    }
    public override void Update (float _timing)
    {
        /*出生停顿间隔*/
        if (_curBeginTime < _characterAI.BgStopTime)
        {
            _curBeginTime += _timing;
            return;
        }
        nav.isStopped = true;
        SkillType skillType = _character.CharacterSkill.GetCurUseSkillType();
        /*攻击、受伤状态不执行下面操作,普通攻击除外*/
        if (nav == null || !_character.CharacterSkill.CanUseSkill() || _character.StateControl.CurState.StateType == StateType.Hit || _character.DontUse)
        {
            return;
        }
            
        float dis = -1;
        if (_curTarget == null)
        {
            List<Character> attackTargets = _character.CharacterDup.AttackTargets;
            /*计算哪个对象靠得最近*/
            if (attackTargets.Count > 0)
            {
                _curTarget = attackTargets[0];
                dis = Vector3.Distance(_curTarget.transform.position, _character.transform.position);
                for (int i = 1; i < attackTargets.Count; i++)
                {
                    if (attackTargets[i].IsDead)
                        continue;

                    float dis2 = Vector3.Distance(attackTargets[i].transform.position, _character.transform.position);
                    if (dis > dis2)
                    {
                        _curTarget = attackTargets[i];
                        dis = dis2;
                    }
                }
            }
            
        }
        else
        {
            dis = Vector3.Distance(_curTarget.transform.position, _character.transform.position);
        }
        if (_curTarget == null)
            return;

        do
        {
            if (_curTarget.IsDead || dis > _characterAI.PusueDis || dis == -1)//原地待命
            {
                _curState = BehaviorState.idle;
                _character.StateControl.ChangeState(StateType.Idle);
                break;
            }
            Quaternion ang;
            bool isLookOk = this.LookTarget(_curTarget, out ang);//面向目标是否完毕
            
            if (dis <= _characterAI.PusueDis && dis > _characterAI.StopDis)//追啊
            {
                if(nav.isStopped)
                {
                    StateBase anim = null;
                    _character.StateControl.States.TryGetValue(StateType.Anim, out anim);
                    nav.speed = _character.CharacterAttribute.AttControl.GetAttSignal(AttributeType.Speed);
                    ((StateAnim)anim).SetAnim("run");
                    _character.StateControl.ChangeState(StateType.Anim);
                }
                
                nav.isStopped = false;
                nav.destination = _curTarget.transform.position;
                _curState = BehaviorState.pursue;
                
                //AI判断是否放技能
                if(isLookOk && curFps>= _characterAI.FpsInterval)
                {
                    SkillSyncData skillSync = this.PlaySkillOfAI(dis);
                    if(skillSync != null)
                    {
                        _character.CharacterSkill.UseSkill(skillSync);
                        _curState = BehaviorState.attack;
                    }
                }
                _curAttackTime = _characterAI.AttackInterval;//追目标时把停下间隔攻击冷却事件置为可攻击
            }
            else if(!isLookOk)//旋转还没完毕
            {
                _character.transform.rotation = Quaternion.Lerp(_character.transform.rotation, ang, Time.deltaTime * 10);
                _character.StateControl.ChangeState(StateType.Anim);
                _curState = BehaviorState.pursue;
            }
            else //攻击
            {
                NearAttack(skillType, dis);
            }
        } while (false);

        if (curFps++ >= _characterAI.FpsInterval)
        {
            curFps = 0;
        }
    }
    private void NearAttack(SkillType skillType, float dis)
    {
        _curAttackTime += Time.deltaTime;
        //AI判断是否放技能
        if (curFps >= _characterAI.FpsInterval)
        {
            SkillSyncData skillSync = this.PlaySkillOfAI(dis);
            if (skillSync != null)
            {
                _character.CharacterSkill.UseSkill(skillSync);
                _curState = BehaviorState.attack;
                return;
            }
        }
        if (skillType == SkillType.None || skillType == SkillType.NormalSkill && _character.CharacterSkill.CurUsingSkillSyncData.SkillData.LinkSkillId == 0)//最后一下延时释放技能
        {
            _canNormlAttack = false;

        }
        if (_curAttackTime < _characterAI.AttackInterval && !_canNormlAttack)
        {
            //可能在跑步，所以从跑步切换状态
            if (_character.StateControl.CurState.StateType == StateType.Anim)
            {
                _curState = BehaviorState.idle;
                _character.StateControl.ChangeState(StateType.Idle);
            }
        }
        else
        {
            if (_character.CharacterSkill.UseSkill())//普通攻击
            {
                _canNormlAttack = true;
                _curState = BehaviorState.attack;
            }
            else
            {
                _curState = BehaviorState.idle;
                _character.StateControl.ChangeState(StateType.Idle);
            }
        }
        if (_curAttackTime >= _characterAI.AttackInterval)
        {
            _curAttackTime = 0;
        }
    }
}
