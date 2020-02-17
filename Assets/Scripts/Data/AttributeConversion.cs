using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;

/// <summary>
/// 自动生成的代码 HeroData.xml导出来 
/// </summary>
public class AttributeConversion : DataBase {

	public int Id;		//加成序号
	public float Power;		//力量
	public float Spell;		//法术


	private static Dictionary<int, AttributeConversion> _datas;

	public static void Load()
	{
		_datas = new Dictionary<int, AttributeConversion> ();
		Object obj = Resources.Load ("Data/" + "AttributeConversion");
		if (obj != null) 
		{
			XmlDocument xmlDoc = new XmlDocument ();
			xmlDoc.LoadXml (obj.ToString());

			XmlNode xmlNode = xmlDoc.SelectSingleNode ("AttributeConversion");

			foreach (XmlNode item in xmlNode.ChildNodes) 
			{
				AttributeConversion data = new AttributeConversion ();
				data.Id = int.Parse(item.Attributes["Id"].Value);
				data.Power = float.Parse(item.Attributes["Power"].Value);
				data.Spell = float.Parse(item.Attributes["Spell"].Value);


				_datas.Add (data.Id, data);
			}
		}
	}

	public static AttributeConversion FindById(int id)
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

	public static Dictionary<int, AttributeConversion> GetDatas()
	{
		if (_datas == null) 
		{
			Load ();
		}
		return _datas;
	}
}

