using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;

/// <summary>
/// 自动生成的代码 HeroData.xml导出来 
/// </summary>
public class SkillData : DataBase {

	public int Id;		//id
	public string Desc;		//技能介绍
	public string AnimName;		//动作名
	public float AnimLength;		//动作长度
	public float ReleaseDis;		//释放距离长度
	public int SkillType;		//技能类型(1普攻 2正常技能 3大招)
	public string Icon;		//技能图标名
	public float Cd;		//冷却时间
	public int LinkSkillId;		//链接技能
	public float LinkSkillTriggerTime;		//触发链接技能时间节点
	public string SkillComps;		//技能组件
	public int MusicComps;		//音效组件


	private static Dictionary<int, SkillData> _datas;

	public static void Load()
	{
		_datas = new Dictionary<int, SkillData> ();
		Object obj = Resources.Load ("Data/" + "SkillData");
		if (obj != null) 
		{
			XmlDocument xmlDoc = new XmlDocument ();
			xmlDoc.LoadXml (obj.ToString());

			XmlNode xmlNode = xmlDoc.SelectSingleNode ("SkillData");

			foreach (XmlNode item in xmlNode.ChildNodes) 
			{
				SkillData data = new SkillData ();
				data.Id = int.Parse(item.Attributes["Id"].Value);
				data.Desc = item.Attributes ["Desc"].Value;
				data.AnimName = item.Attributes ["AnimName"].Value;
				data.AnimLength = float.Parse(item.Attributes["AnimLength"].Value);
				data.ReleaseDis = float.Parse(item.Attributes["ReleaseDis"].Value);
				data.SkillType = int.Parse(item.Attributes["SkillType"].Value);
				data.Icon = item.Attributes ["Icon"].Value;
				data.Cd = float.Parse(item.Attributes["Cd"].Value);
				data.LinkSkillId = int.Parse(item.Attributes["LinkSkillId"].Value);
				data.LinkSkillTriggerTime = float.Parse(item.Attributes["LinkSkillTriggerTime"].Value);
				data.SkillComps = item.Attributes ["SkillComps"].Value;
				data.MusicComps = int.Parse(item.Attributes["MusicComps"].Value);


				_datas.Add (data.Id, data);
			}
		}
	}

	public static SkillData FindById(int id)
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

	public static Dictionary<int, SkillData> GetDatas()
	{
		if (_datas == null) 
		{
			Load ();
		}
		return _datas;
	}
}

