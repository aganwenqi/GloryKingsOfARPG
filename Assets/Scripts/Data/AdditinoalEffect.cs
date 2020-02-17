using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;

/// <summary>
/// 自动生成的代码 HeroData.xml导出来 
/// </summary>
public class AdditinoalEffect : DataBase {

	public int Id;		//id
	public string Name;		//效果名
	public int AdditioalStateType;		//效果类型
	public float Hurt;		//伤害
	public int HurtType;		//伤害类型(1固定 2乘玩家属性）
	public float TimeLen;		//总时长
	public int Count;		//触发次数
	public int Fx;		//特效


	private static Dictionary<int, AdditinoalEffect> _datas;

	public static void Load()
	{
		_datas = new Dictionary<int, AdditinoalEffect> ();
		Object obj = Resources.Load ("Data/" + "AdditinoalEffect");
		if (obj != null) 
		{
			XmlDocument xmlDoc = new XmlDocument ();
			xmlDoc.LoadXml (obj.ToString());

			XmlNode xmlNode = xmlDoc.SelectSingleNode ("AdditinoalEffect");

			foreach (XmlNode item in xmlNode.ChildNodes) 
			{
				AdditinoalEffect data = new AdditinoalEffect ();
				data.Id = int.Parse(item.Attributes["Id"].Value);
				data.Name = item.Attributes ["Name"].Value;
				data.AdditioalStateType = int.Parse(item.Attributes["AdditioalStateType"].Value);
				data.Hurt = float.Parse(item.Attributes["Hurt"].Value);
				data.HurtType = int.Parse(item.Attributes["HurtType"].Value);
				data.TimeLen = float.Parse(item.Attributes["TimeLen"].Value);
				data.Count = int.Parse(item.Attributes["Count"].Value);
				data.Fx = int.Parse(item.Attributes["Fx"].Value);


				_datas.Add (data.Id, data);
			}
		}
	}

	public static AdditinoalEffect FindById(int id)
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

	public static Dictionary<int, AdditinoalEffect> GetDatas()
	{
		if (_datas == null) 
		{
			Load ();
		}
		return _datas;
	}
}

