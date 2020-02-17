using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;

/// <summary>
/// 自动生成的代码 HeroData.xml导出来 
/// </summary>
public class SkillCompHurt : DataBase {

	public int Id;		//id
	public string Desc;		//伤害组件介绍
	public float Phy;		//物理攻击加成
	public float Magic;		//法术攻击
	public float PowerAdd;		//力量加成
	public float SpellAdd;		//法术加成
	public float MSpeedAdd;		//移动速度加成
	public float HpAdd;		//HP加成
	public float MpAdd;		//MP加成
	public float CritAdd;		//暴击加成
	public float PhyDefendAdd;		//物理抗性加成
	public float MagicDefendAdd;		//法术抗性加成


	private static Dictionary<int, SkillCompHurt> _datas;

	public static void Load()
	{
		_datas = new Dictionary<int, SkillCompHurt> ();
		Object obj = Resources.Load ("Data/" + "SkillCompHurt");
		if (obj != null) 
		{
			XmlDocument xmlDoc = new XmlDocument ();
			xmlDoc.LoadXml (obj.ToString());

			XmlNode xmlNode = xmlDoc.SelectSingleNode ("SkillCompHurt");

			foreach (XmlNode item in xmlNode.ChildNodes) 
			{
				SkillCompHurt data = new SkillCompHurt ();
				data.Id = int.Parse(item.Attributes["Id"].Value);
				data.Desc = item.Attributes ["Desc"].Value;
				data.Phy = float.Parse(item.Attributes["Phy"].Value);
				data.Magic = float.Parse(item.Attributes["Magic"].Value);
				data.PowerAdd = float.Parse(item.Attributes["PowerAdd"].Value);
				data.SpellAdd = float.Parse(item.Attributes["SpellAdd"].Value);
				data.MSpeedAdd = float.Parse(item.Attributes["MSpeedAdd"].Value);
				data.HpAdd = float.Parse(item.Attributes["HpAdd"].Value);
				data.MpAdd = float.Parse(item.Attributes["MpAdd"].Value);
				data.CritAdd = float.Parse(item.Attributes["CritAdd"].Value);
				data.PhyDefendAdd = float.Parse(item.Attributes["PhyDefendAdd"].Value);
				data.MagicDefendAdd = float.Parse(item.Attributes["MagicDefendAdd"].Value);


				_datas.Add (data.Id, data);
			}
		}
	}

	public static SkillCompHurt FindById(int id)
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

	public static Dictionary<int, SkillCompHurt> GetDatas()
	{
		if (_datas == null) 
		{
			Load ();
		}
		return _datas;
	}
}

