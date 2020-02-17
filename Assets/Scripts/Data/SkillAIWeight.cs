using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;

/// <summary>
/// 自动生成的代码 HeroData.xml导出来 
/// </summary>
public class SkillAIWeight : DataBase {

	public int Id;		//id(HeroData对应)
	public string MiaoShu;		//猫叔
	public int Skill0ID;		//技能0ID
	public int Skill0;		//技能0
	public int Skill0Random;		//技能0释放概率（普攻）
	public int Skill1ID;		//技能1ID
	public int Skill1;		//技能1
	public int Skill1Random;		//技能1释放概率
	public int Skill2ID;		//技能2ID
	public int Skill2;		//技能2
	public int Skill2Random;		//技能2释放概率
	public int Skill3ID;		//技能3ID
	public int Skill3;		//技能3
	public int Skill3Random;		//技能3释放概率


	private static Dictionary<int, SkillAIWeight> _datas;

	public static void Load()
	{
		_datas = new Dictionary<int, SkillAIWeight> ();
		Object obj = Resources.Load ("Data/" + "SkillAIWeight");
		if (obj != null) 
		{
			XmlDocument xmlDoc = new XmlDocument ();
			xmlDoc.LoadXml (obj.ToString());

			XmlNode xmlNode = xmlDoc.SelectSingleNode ("SkillAIWeight");

			foreach (XmlNode item in xmlNode.ChildNodes) 
			{
				SkillAIWeight data = new SkillAIWeight ();
				data.Id = int.Parse(item.Attributes["Id"].Value);
				data.MiaoShu = item.Attributes ["MiaoShu"].Value;
				data.Skill0ID = int.Parse(item.Attributes["Skill0ID"].Value);
				data.Skill0 = int.Parse(item.Attributes["Skill0"].Value);
				data.Skill0Random = int.Parse(item.Attributes["Skill0Random"].Value);
				data.Skill1ID = int.Parse(item.Attributes["Skill1ID"].Value);
				data.Skill1 = int.Parse(item.Attributes["Skill1"].Value);
				data.Skill1Random = int.Parse(item.Attributes["Skill1Random"].Value);
				data.Skill2ID = int.Parse(item.Attributes["Skill2ID"].Value);
				data.Skill2 = int.Parse(item.Attributes["Skill2"].Value);
				data.Skill2Random = int.Parse(item.Attributes["Skill2Random"].Value);
				data.Skill3ID = int.Parse(item.Attributes["Skill3ID"].Value);
				data.Skill3 = int.Parse(item.Attributes["Skill3"].Value);
				data.Skill3Random = int.Parse(item.Attributes["Skill3Random"].Value);


				_datas.Add (data.Id, data);
			}
		}
	}

	public static SkillAIWeight FindById(int id)
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

	public static Dictionary<int, SkillAIWeight> GetDatas()
	{
		if (_datas == null) 
		{
			Load ();
		}
		return _datas;
	}
}

