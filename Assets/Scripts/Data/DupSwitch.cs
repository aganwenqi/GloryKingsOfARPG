using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;

/// <summary>
/// 自动生成的代码 HeroData.xml导出来 
/// </summary>
public class DupSwitch : DataBase {

	public int Id;		//id(每章几个副本)
	public string ChapterName;		//章节名
	public string DupIds;		//副本id


	private static Dictionary<int, DupSwitch> _datas;

	public static void Load()
	{
		_datas = new Dictionary<int, DupSwitch> ();
		Object obj = Resources.Load ("Data/" + "DupSwitch");
		if (obj != null) 
		{
			XmlDocument xmlDoc = new XmlDocument ();
			xmlDoc.LoadXml (obj.ToString());

			XmlNode xmlNode = xmlDoc.SelectSingleNode ("DupSwitch");

			foreach (XmlNode item in xmlNode.ChildNodes) 
			{
				DupSwitch data = new DupSwitch ();
				data.Id = int.Parse(item.Attributes["Id"].Value);
				data.ChapterName = item.Attributes ["ChapterName"].Value;
				data.DupIds = item.Attributes ["DupIds"].Value;


				_datas.Add (data.Id, data);
			}
		}
	}

	public static DupSwitch FindById(int id)
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

	public static Dictionary<int, DupSwitch> GetDatas()
	{
		if (_datas == null) 
		{
			Load ();
		}
		return _datas;
	}
}

