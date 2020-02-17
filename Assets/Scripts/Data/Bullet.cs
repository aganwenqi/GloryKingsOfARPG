using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;

/// <summary>
/// 自动生成的代码 HeroData.xml导出来 
/// </summary>
public class Bullet : DataBase {

	public int Id;		//id
	public string Desc;		//技能组件介绍
	public string Name;		//模型名
	public int TransformBehaviourType;		//绑点类型(1角色根节点下 2武器根节点下 3副武器节点下)
	public float Param1;		//参数1
	public float Param2;		//参数2
	public float Param3;		//参数3
	public float Param4;		//参数4
	public int Fx1;		//施法特效1的id
	public int Fx2;		//施法特效2的id
	public int Fx3;		//施法特效3的id
	public int HitFx;		//受击特效
	public int LinkSkillId;		//链接弹道
	public int LSTrigger;		//链接弹道触发时间
	public int TriggleBt;		//结束并触发下一个弹道
	public int HurtAdd;		//伤害加成组件SkillCompHurt
	public int AdditionEf;		//附加效果AdditinoalEffect
	public int OverObstacle;		//是否穿过障碍
	public int MusicComps;		//音效组件


	private static Dictionary<int, Bullet> _datas;

	public static void Load()
	{
		_datas = new Dictionary<int, Bullet> ();
		Object obj = Resources.Load ("Data/" + "Bullet");
		if (obj != null) 
		{
			XmlDocument xmlDoc = new XmlDocument ();
			xmlDoc.LoadXml (obj.ToString());

			XmlNode xmlNode = xmlDoc.SelectSingleNode ("Bullet");

			foreach (XmlNode item in xmlNode.ChildNodes) 
			{
				Bullet data = new Bullet ();
				data.Id = int.Parse(item.Attributes["Id"].Value);
				data.Desc = item.Attributes ["Desc"].Value;
				data.Name = item.Attributes ["Name"].Value;
				data.TransformBehaviourType = int.Parse(item.Attributes["TransformBehaviourType"].Value);
				data.Param1 = float.Parse(item.Attributes["Param1"].Value);
				data.Param2 = float.Parse(item.Attributes["Param2"].Value);
				data.Param3 = float.Parse(item.Attributes["Param3"].Value);
				data.Param4 = float.Parse(item.Attributes["Param4"].Value);
				data.Fx1 = int.Parse(item.Attributes["Fx1"].Value);
				data.Fx2 = int.Parse(item.Attributes["Fx2"].Value);
				data.Fx3 = int.Parse(item.Attributes["Fx3"].Value);
				data.HitFx = int.Parse(item.Attributes["HitFx"].Value);
				data.LinkSkillId = int.Parse(item.Attributes["LinkSkillId"].Value);
				data.LSTrigger = int.Parse(item.Attributes["LSTrigger"].Value);
				data.TriggleBt = int.Parse(item.Attributes["TriggleBt"].Value);
				data.HurtAdd = int.Parse(item.Attributes["HurtAdd"].Value);
				data.AdditionEf = int.Parse(item.Attributes["AdditionEf"].Value);
				data.OverObstacle = int.Parse(item.Attributes["OverObstacle"].Value);
				data.MusicComps = int.Parse(item.Attributes["MusicComps"].Value);


				_datas.Add (data.Id, data);
			}
		}
	}

	public static Bullet FindById(int id)
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

	public static Dictionary<int, Bullet> GetDatas()
	{
		if (_datas == null) 
		{
			Load ();
		}
		return _datas;
	}
}

