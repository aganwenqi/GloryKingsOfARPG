using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;

/// <summary>
/// 自动生成的代码 HeroData.xml导出来 
/// </summary>
public class HeroData : DataBase {

	public int Id;		//id
	public string Name;		//名字
	public string ModelName;		//模型名
	public string ShowModel;		//展示模型名
	public string Icon;		//图标
	public string AllSkills;		//拥有技能
	public int characterType;		//角色类型(1玩家 2怪物 3npc)
	public int Target;		//攻击对象(1玩家 2怪物 3所有)
	public int Affx;		//词缀套
	public int AI;		//AI表
	public int SkillWeight;		//技能权重表
	public int Sounds;		//基础音效


	private static Dictionary<int, HeroData> _datas;

	public static void Load()
	{
		_datas = new Dictionary<int, HeroData> ();
		Object obj = Resources.Load ("Data/" + "HeroData");
		if (obj != null) 
		{
			XmlDocument xmlDoc = new XmlDocument ();
			xmlDoc.LoadXml (obj.ToString());

			XmlNode xmlNode = xmlDoc.SelectSingleNode ("HeroData");

			foreach (XmlNode item in xmlNode.ChildNodes) 
			{
				HeroData data = new HeroData ();
				data.Id = int.Parse(item.Attributes["Id"].Value);
				data.Name = item.Attributes ["Name"].Value;
				data.ModelName = item.Attributes ["ModelName"].Value;
				data.ShowModel = item.Attributes ["ShowModel"].Value;
				data.Icon = item.Attributes ["Icon"].Value;
				data.AllSkills = item.Attributes ["AllSkills"].Value;
				data.characterType = int.Parse(item.Attributes["characterType"].Value);
				data.Target = int.Parse(item.Attributes["Target"].Value);
				data.Affx = int.Parse(item.Attributes["Affx"].Value);
				data.AI = int.Parse(item.Attributes["AI"].Value);
				data.SkillWeight = int.Parse(item.Attributes["SkillWeight"].Value);
				data.Sounds = int.Parse(item.Attributes["Sounds"].Value);


				_datas.Add (data.Id, data);
			}
		}
	}

	public static HeroData FindById(int id)
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

	public static Dictionary<int, HeroData> GetDatas()
	{
		if (_datas == null) 
		{
			Load ();
		}
		return _datas;
	}
}

