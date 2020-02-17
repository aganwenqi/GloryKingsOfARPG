using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDup : CharacterCompBase
{
    private DupController _dupController;
    /*可攻击的目标列表*/
    private List<Character> _attackTargets = new List<Character>();
    /*攻击目标枚举*/
    private AttackTarget _attackTarget;
    public override void Init(Character character)
    {
        base.Init(character);
        CharacterAI characterAI = CharacterAI.FindById(_character.HeroData.AI);
        _attackTarget = (AttackTarget)_character.HeroData.Target;
        InitData();
    }
    /*每次切换副本调用*/
    public override void InitData()
    {
        base.InitData();
        _attackTargets.Clear();
        _dupController = DupController.Instance;
        foreach (Character item in _dupController.DupCharacters)
        {
            UpdateAttackTarget(item, true);
        }
        _dupController.AddCharacter(_character);
        _dupController.AddListen(this.UpdateAttackTarget);
    }
    /*被副本监听器监听着, true副本添加了角色，false删除了角色*/
    public void UpdateAttackTarget(Character target, bool isAdd)
    {
        if (target == _character)
            return;
        bool canAttack = CharacterManager.Instance.CanAttack(_character, target);
        if (!canAttack)
            return;

        if (isAdd)
        {
            if(!_attackTargets.Contains(target))
                _attackTargets.Add(target);
        }
        else
        {
            _attackTargets.Remove(target);
        }
    }
    public override void Update(float _timing)
    {

    }
    public override void UnSpawn()
    {
        base.UnSpawn();
        _character.CharacterDup.DupController.RemoveListen(this.UpdateAttackTarget);
    }
    #region 对象属性
    public DupController DupController
    {
        get
        {
            return _dupController;
        }
        set
        {
            _dupController = value;
        }
    }

    public List<Character> AttackTargets
    {
        get
        {
            return _attackTargets;
        }

        set
        {
            _attackTargets = value;
        }
    }
    public AttackTarget AttackTarget
    {
        get
        {
            return _attackTarget;
        }

        set
        {
            _attackTarget = value;
        }
    }
    #endregion
}
