using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;

/// <summary>
/// 自动生成的代码 HeroData.xml导出来 
/// </summary>
public class BulletClip : DataBase {

	public int Id;		//id
	public string Name;		//名字
	public string SkillClip;		//技能释放音效
	public float DelaySkill;		//延时播放


	private static Dictionary<int, BulletClip> _datas;

	public static void Load()
	{
		_datas = new Dictionary<int, BulletClip> ();
		Object obj = Resources.Load ("Data/" + "BulletClip");
		if (obj != null) 
		{
			XmlDocument xmlDoc = new XmlDocument ();
			xmlDoc.LoadXml (obj.ToString());

			XmlNode xmlNode = xmlDoc.SelectSingleNode ("BulletClip");

			foreach (XmlNode item in xmlNode.ChildNodes) 
			{
				BulletClip data = new BulletClip ();
				data.Id = int.Parse(item.Attributes["Id"].Value);
				data.Name = item.Attributes ["Name"].Value;
				data.SkillClip = item.Attributes ["SkillClip"].Value;
				data.DelaySkill = float.Parse(item.Attributes["DelaySkill"].Value);


				_datas.Add (data.Id, data);
			}
		}
	}

	public static BulletClip FindById(int id)
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

	public static Dictionary<int, BulletClip> GetDatas()
	{
		if (_datas == null) 
		{
			Load ();
		}
		return _datas;
	}
}

