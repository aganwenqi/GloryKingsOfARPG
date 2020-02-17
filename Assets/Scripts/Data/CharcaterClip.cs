using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;

/// <summary>
/// 自动生成的代码 HeroData.xml导出来 
/// </summary>
public class CharcaterClip : DataBase {

	public int Id;		//id
	public string Name;		//名字
	public string HeroClip;		//英雄音效
	public float DelayHero;		//延时播放
	public string SkillClip;		//技能释放音效
	public float DelaySkill;		//延时播放
	public string HitClip;		//击中音效
	public float DelayHit;		//延时播放


	private static Dictionary<int, CharcaterClip> _datas;

	public static void Load()
	{
		_datas = new Dictionary<int, CharcaterClip> ();
		Object obj = Resources.Load ("Data/" + "CharcaterClip");
		if (obj != null) 
		{
			XmlDocument xmlDoc = new XmlDocument ();
			xmlDoc.LoadXml (obj.ToString());

			XmlNode xmlNode = xmlDoc.SelectSingleNode ("CharcaterClip");

			foreach (XmlNode item in xmlNode.ChildNodes) 
			{
				CharcaterClip data = new CharcaterClip ();
				data.Id = int.Parse(item.Attributes["Id"].Value);
				data.Name = item.Attributes ["Name"].Value;
				data.HeroClip = item.Attributes ["HeroClip"].Value;
				data.DelayHero = float.Parse(item.Attributes["DelayHero"].Value);
				data.SkillClip = item.Attributes ["SkillClip"].Value;
				data.DelaySkill = float.Parse(item.Attributes["DelaySkill"].Value);
				data.HitClip = item.Attributes ["HitClip"].Value;
				data.DelayHit = float.Parse(item.Attributes["DelayHit"].Value);


				_datas.Add (data.Id, data);
			}
		}
	}

	public static CharcaterClip FindById(int id)
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

	public static Dictionary<int, CharcaterClip> GetDatas()
	{
		if (_datas == null) 
		{
			Load ();
		}
		return _datas;
	}
}

