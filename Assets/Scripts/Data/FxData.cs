using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;

/// <summary>
/// 自动生成的代码 HeroData.xml导出来 
/// </summary>
public class FxData : DataBase {

	public int Id;		//id
	public string Desc;		//备注
	public string Name;		//特效名
	public int BindingType;		//绑点类型(1角色根节点下 2武器根节点下 3副武器节点下 4受伤点)
	public float Delay;		//延迟播放时间


	private static Dictionary<int, FxData> _datas;

	public static void Load()
	{
		_datas = new Dictionary<int, FxData> ();
		Object obj = Resources.Load ("Data/" + "FxData");
		if (obj != null) 
		{
			XmlDocument xmlDoc = new XmlDocument ();
			xmlDoc.LoadXml (obj.ToString());

			XmlNode xmlNode = xmlDoc.SelectSingleNode ("FxData");

			foreach (XmlNode item in xmlNode.ChildNodes) 
			{
				FxData data = new FxData ();
				data.Id = int.Parse(item.Attributes["Id"].Value);
				data.Desc = item.Attributes ["Desc"].Value;
				data.Name = item.Attributes ["Name"].Value;
				data.BindingType = int.Parse(item.Attributes["BindingType"].Value);
				data.Delay = float.Parse(item.Attributes["Delay"].Value);


				_datas.Add (data.Id, data);
			}
		}
	}

	public static FxData FindById(int id)
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

	public static Dictionary<int, FxData> GetDatas()
	{
		if (_datas == null) 
		{
			Load ();
		}
		return _datas;
	}
}

