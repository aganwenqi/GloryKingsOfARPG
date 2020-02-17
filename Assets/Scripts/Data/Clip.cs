using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;

/// <summary>
/// 自动生成的代码 HeroData.xml导出来 
/// </summary>
public class Clip : DataBase {

	public int Id;		//id
	public string Name;		//名字
	public string ClipName;		//音效
	public float Delay;		//延时播放


	private static Dictionary<int, Clip> _datas;

	public static void Load()
	{
		_datas = new Dictionary<int, Clip> ();
		Object obj = Resources.Load ("Data/" + "Clip");
		if (obj != null) 
		{
			XmlDocument xmlDoc = new XmlDocument ();
			xmlDoc.LoadXml (obj.ToString());

			XmlNode xmlNode = xmlDoc.SelectSingleNode ("Clip");

			foreach (XmlNode item in xmlNode.ChildNodes) 
			{
				Clip data = new Clip ();
				data.Id = int.Parse(item.Attributes["Id"].Value);
				data.Name = item.Attributes ["Name"].Value;
				data.ClipName = item.Attributes ["ClipName"].Value;
				data.Delay = float.Parse(item.Attributes["Delay"].Value);


				_datas.Add (data.Id, data);
			}
		}
	}

	public static Clip FindById(int id)
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

	public static Dictionary<int, Clip> GetDatas()
	{
		if (_datas == null) 
		{
			Load ();
		}
		return _datas;
	}
}

