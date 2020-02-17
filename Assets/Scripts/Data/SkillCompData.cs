using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;

/// <summary>
/// 自动生成的代码 HeroData.xml导出来 
/// </summary>
public class SkillCompData : DataBase {

	public int Id;		//id
	public string Desc;		//技能组件介绍
	public int SkillCompBehaviourType;		//技能行为 (1普通行为（范围检测） 2弹道技能 3buff)
	public int CheckPoint;		//检测点(1角色根节点下 2武器根节点下 3副武器节点下)
	public float Param1;		//参数1
	public float Param2;		//参数2
	public float Param3;		//参数3
	public float Param4;		//参数4
	public float EffectTime;		//结算点
	public int AttackCount;		//伤害次数
	public float AttackTime;		//伤害总计时长
	public int Fx1;		//施法特效1的id
	public int Fx2;		//施法特效2的id
	public int Fx3;		//施法特效3的id
	public int HitFx;		//受击特效
	public int HurtAdd;		//属性加成
	public int AdditionEf;		//附加效果AdditinoalEffect


	private static Dictionary<int, SkillCompData> _datas;

	public static void Load()
	{
		_datas = new Dictionary<int, SkillCompData> ();
		Object obj = Resources.Load ("Data/" + "SkillCompData");
		if (obj != null) 
		{
			XmlDocument xmlDoc = new XmlDocument ();
			xmlDoc.LoadXml (obj.ToString());

			XmlNode xmlNode = xmlDoc.SelectSingleNode ("SkillCompData");

			foreach (XmlNode item in xmlNode.ChildNodes) 
			{
				SkillCompData data = new SkillCompData ();
				data.Id = int.Parse(item.Attributes["Id"].Value);
				data.Desc = item.Attributes ["Desc"].Value;
				data.SkillCompBehaviourType = int.Parse(item.Attributes["SkillCompBehaviourType"].Value);
				data.CheckPoint = int.Parse(item.Attributes["CheckPoint"].Value);
				data.Param1 = float.Parse(item.Attributes["Param1"].Value);
				data.Param2 = float.Parse(item.Attributes["Param2"].Value);
				data.Param3 = float.Parse(item.Attributes["Param3"].Value);
				data.Param4 = float.Parse(item.Attributes["Param4"].Value);
				data.EffectTime = float.Parse(item.Attributes["EffectTime"].Value);
				data.AttackCount = int.Parse(item.Attributes["AttackCount"].Value);
				data.AttackTime = float.Parse(item.Attributes["AttackTime"].Value);
				data.Fx1 = int.Parse(item.Attributes["Fx1"].Value);
				data.Fx2 = int.Parse(item.Attributes["Fx2"].Value);
				data.Fx3 = int.Parse(item.Attributes["Fx3"].Value);
				data.HitFx = int.Parse(item.Attributes["HitFx"].Value);
				data.HurtAdd = int.Parse(item.Attributes["HurtAdd"].Value);
				data.AdditionEf = int.Parse(item.Attributes["AdditionEf"].Value);


				_datas.Add (data.Id, data);
			}
		}
	}

	public static SkillCompData FindById(int id)
	{
		if (_datas == null) 
		{
			Load ();
		}

		if (_datas.ContainsKey(id)) 
		{
			return _datas [id];
		}

		return null;
	}

	public static Dictionary<int, SkillCompData> GetDatas()
	{
		if (_datas == null) 
		{
			Load ();
		}
		return _datas;
	}
}

