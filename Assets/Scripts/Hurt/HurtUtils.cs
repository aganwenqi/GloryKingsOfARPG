using UnityEngine;

public class HurtUtils{

    /*伤害计算*/
    /// <summary>
    /* 暴击（isCrit）= rand(0, 100) > crit;
       暴击伤害加成 critMul = isCrit ? 2 : 1;
       总伤害 =（ (物理攻击加成* 玩家物理攻击 + 力量 * 力量加成 *换算表力量) *技能组件加成 - 物理抗性）* critMul
              +（ (法术攻击加成* 玩家物理攻击 + 法术 * 法术加成 * 换算表法术) *技能组件加成 - 法术抗性）* critMul*/
    /// </summary>
    /// <param name="scr"></param>
    /// <param name="dst"></param>
    /// <param name="skillData"></param>
    /// <param name="type"></param>
    /// <returns></returns>
    public static HurtResult HurtCalculation(AttributesControl scr, AttributesControl dst, DataBase skillData, SkillCompBehaviourType type)
    {
        HurtResult hurt = new HurtResult();
        if (skillData == null)
        {
            Debug.LogError("没有这个技能组建，无法计算伤害");
            return hurt;
        }
        SkillCompHurt skillHurt = null;
        float comAdd = 0;//技能组件加成
        if (type == SkillCompBehaviourType.Normal)
        {
            SkillCompData sc = skillData as SkillCompData;
            skillHurt = SkillCompHurt.FindById(sc.HurtAdd);
            comAdd = sc.Param1;
        }
        else if (type == SkillCompBehaviourType.Bullet)
        {
            Bullet bt = skillData as Bullet;
            skillHurt = SkillCompHurt.FindById(bt.HurtAdd);
            comAdd = bt.Param1;
        }
        if (skillHurt == null)
        {
            Debug.LogError("获得伤害换算配置失败");
            return hurt;
        }
        AttributeConversion cal = AttributeConversion.FindById(1);//换算表
        hurt.isCrit = IsCrit(scr.GetAttSignal(AttributeType.Crit));
        float phyAdd = skillHurt.Phy;
        float phy = scr.GetAttSignal(AttributeType.PhyHurt);
        float power = scr.GetAttSignal(AttributeType.Power);
        float powerAdd = skillHurt.PowerAdd;
        float powerCal = cal.Power;
        float phyDefend = dst.GetAttSignal(AttributeType.PhyDefend);

        int critMul = hurt.isCrit ? 2 : 1;

        float magicAdd = skillHurt.Magic;
        float magic = scr.GetAttSignal(AttributeType.MagicHurt);
        float spell = scr.GetAttSignal(AttributeType.Spell);
        float spellAdd = skillHurt.SpellAdd;
        float spellCal = cal.Spell;
        float magicDefend = dst.GetAttSignal(AttributeType.MagicDefend);

        float PhyResult = ((phyAdd * phy + power * powerAdd * powerCal) * comAdd - phyDefend) * critMul;
        float magicResult = ((magicAdd * magic + spell * spellAdd * spellCal) * comAdd - magicDefend) * critMul;
        if (PhyResult < 0)
            PhyResult = 0;
        if (magicResult < 0)
            magicResult = 0;

        hurt.hurt = -(PhyResult + magicResult);
        return hurt;
    }
	
    /*是否暴击计算*/
    public static bool IsCrit(float crit)
    {
        if(crit > 99.9999)
        {
            return true;
        }
        else if(crit < 0.0001)
        {
            return false;
        }
        int value = Random.Range(0,100);
        if(value < crit)
        {
            return true;
        }
        return false;
    }
}
public struct HurtResult
{
    public float hurt;
    public bool isCrit;
}