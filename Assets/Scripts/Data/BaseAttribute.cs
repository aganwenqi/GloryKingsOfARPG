using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;

/// <summary>
/// 自动生成的代码 HeroData.xml导出来 
/// </summary>
public class BaseAttribute : DataBase {

	public int Id;		//id
	public string Describe;		//描述
	public float Speed;		//移动速度
	public float PhyHurt;		//物理攻击
	public float MagicHurt;		//法术攻击
	public float PhyDefend;		//物理抗性
	public float MagicDefend;		//魔法抗性
	public float Power;		//力量
	public float Spell;		//法术
	public float Crit;		//暴击
	public float Hp;		//HP
	public float Mp;		//MP
	public float BeHurtTime;		//受伤动作时长


	private static Dictionary<int, BaseAttribute> _datas;

	public static void Load()
	{
		_datas = new Dictionary<int, BaseAttribute> ();
		Object obj = Resources.Load ("Data/" + "BaseAttribute");
		if (obj != null) 
		{
			XmlDocument xmlDoc = new XmlDocument ();
			xmlDoc.LoadXml (obj.ToString());

			XmlNode xmlNode = xmlDoc.SelectSingleNode ("BaseAttribute");

			foreach (XmlNode item in xmlNode.ChildNodes) 
			{
				BaseAttribute data = new BaseAttribute ();
				data.Id = int.Parse(item.Attributes["Id"].Value);
				data.Describe = item.Attributes ["Describe"].Value;
				data.Speed = float.Parse(item.Attributes["Speed"].Value);
				data.PhyHurt = float.Parse(item.Attributes["PhyHurt"].Value);
				data.MagicHurt = float.Parse(item.Attributes["MagicHurt"].Value);
				data.PhyDefend = float.Parse(item.Attributes["PhyDefend"].Value);
				data.MagicDefend = float.Parse(item.Attributes["MagicDefend"].Value);
				data.Power = float.Parse(item.Attributes["Power"].Value);
				data.Spell = float.Parse(item.Attributes["Spell"].Value);
				data.Crit = float.Parse(item.Attributes["Crit"].Value);
				data.Hp = float.Parse(item.Attributes["Hp"].Value);
				data.Mp = float.Parse(item.Attributes["Mp"].Value);
				data.BeHurtTime = float.Parse(item.Attributes["BeHurtTime"].Value);


				_datas.Add (data.Id, data);
			}
		}
	}

	public static BaseAttribute FindById(int id)
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

	public static Dictionary<int, BaseAttribute> GetDatas()
	{
		if (_datas == null) 
		{
			Load ();
		}
		return _datas;
	}
}

