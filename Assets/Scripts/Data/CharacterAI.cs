using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;

/// <summary>
/// 自动生成的代码 HeroData.xml导出来 
/// </summary>
public class CharacterAI : DataBase {

	public int Id;		//怪物id
	public string Name;		//名字
	public int behaviorType;		//行为方式(1原地，2巡逻)
	public float PusueDis;		//追击距离(米)
	public float StopDis;		//停止距离
	public float AttackDis;		//可攻击距离
	public float AttackAngle;		//可攻击角度差
	public int FpsInterval;		//AI计算帧间隔
	public int AttackInterval;		//攻击间隔(秒)
	public float BgStopTime;		//出生停留间隔


	private static Dictionary<int, CharacterAI> _datas;

	public static void Load()
	{
		_datas = new Dictionary<int, CharacterAI> ();
		Object obj = Resources.Load ("Data/" + "CharacterAI");
		if (obj != null) 
		{
			XmlDocument xmlDoc = new XmlDocument ();
			xmlDoc.LoadXml (obj.ToString());

			XmlNode xmlNode = xmlDoc.SelectSingleNode ("CharacterAI");

			foreach (XmlNode item in xmlNode.ChildNodes) 
			{
				CharacterAI data = new CharacterAI ();
				data.Id = int.Parse(item.Attributes["Id"].Value);
				data.Name = item.Attributes ["Name"].Value;
				data.behaviorType = int.Parse(item.Attributes["behaviorType"].Value);
				data.PusueDis = float.Parse(item.Attributes["PusueDis"].Value);
				data.StopDis = float.Parse(item.Attributes["StopDis"].Value);
				data.AttackDis = float.Parse(item.Attributes["AttackDis"].Value);
				data.AttackAngle = float.Parse(item.Attributes["AttackAngle"].Value);
				data.FpsInterval = int.Parse(item.Attributes["FpsInterval"].Value);
				data.AttackInterval = int.Parse(item.Attributes["AttackInterval"].Value);
				data.BgStopTime = float.Parse(item.Attributes["BgStopTime"].Value);


				_datas.Add (data.Id, data);
			}
		}
	}

	public static CharacterAI FindById(int id)
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

	public static Dictionary<int, CharacterAI> GetDatas()
	{
		if (_datas == null) 
		{
			Load ();
		}
		return _datas;
	}
}

