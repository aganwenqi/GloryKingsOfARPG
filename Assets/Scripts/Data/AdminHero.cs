using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;

/// <summary>
/// 自动生成的代码 HeroData.xml导出来 
/// </summary>
public class AdminHero : DataBase {

	public int Id;		//id
	public string Name;		//名字


	private static Dictionary<int, AdminHero> _datas;

	public static void Load()
	{
		_datas = new Dictionary<int, AdminHero> ();
		Object obj = Resources.Load ("Data/" + "AdminHero");
		if (obj != null) 
		{
			XmlDocument xmlDoc = new XmlDocument ();
			xmlDoc.LoadXml (obj.ToString());

			XmlNode xmlNode = xmlDoc.SelectSingleNode ("AdminHero");

			foreach (XmlNode item in xmlNode.ChildNodes) 
			{
				AdminHero data = new AdminHero ();
				data.Id = int.Parse(item.Attributes["Id"].Value);
				data.Name = item.Attributes ["Name"].Value;


				_datas.Add (data.Id, data);
			}
		}
	}

	public static AdminHero FindById(int id)
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

	public static Dictionary<int, AdminHero> GetDatas()
	{
		if (_datas == null) 
		{
			Load ();
		}
		return _datas;
	}
}

