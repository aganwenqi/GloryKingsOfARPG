
using System.Collections.Generic;
using UnityEngine;

public class Monster : Character {

    private MonsterHpBar _hpBar;

    private void Awake()
    {
        _hpBar = this.GetComponentInChildren<MonsterHpBar>();
    }
    public override void Init(CharacterUtilData data)
	{
        base.Init(data);
        _hpBar.Init(this);
    }
    // Update is called once per frame
    public override void Update () {
        if (IsDead)
            return;
        

        base.Update();
    }
    public override void InitData(Vector3 position, Quaternion rotate)
    {
        base.InitData(position, rotate);
        _hpBar.InitData();
    }
    public override void LateUpdate()
	{
        if (IsDead)
            return;
	}
    public override void Dead()
    {
        base.Dead();
        _hpBar.Dead();
    }
    #region 对象属性
    public MonsterHpBar HpBar
    {
        get
        {
            return _hpBar;
        }

        set
        {
            _hpBar = value;
        }
    }
    #endregion
}
