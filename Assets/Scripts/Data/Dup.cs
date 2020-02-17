using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;

/// <summary>
/// 自动生成的代码 HeroData.xml导出来 
/// </summary>
public class Dup : DataBase {

	public int Id;		//id
	public string SceneName;		//场景名
	public int PointNum;		//怪物点个数
	public string PointValue;		//每点怪物id:数量
	public int Award;		//通关奖励
	public string SwitchUI;		//选择关卡显示ui
	public string scene;		//切换场景名
	public int BgMusic;		//背景音乐(链接Clip表)


	private static Dictionary<int, Dup> _datas;

	public static void Load()
	{
		_datas = new Dictionary<int, Dup> ();
		Object obj = Resources.Load ("Data/" + "Dup");
		if (obj != null) 
		{
			XmlDocument xmlDoc = new XmlDocument ();
			xmlDoc.LoadXml (obj.ToString());

			XmlNode xmlNode = xmlDoc.SelectSingleNode ("Dup");

			foreach (XmlNode item in xmlNode.ChildNodes) 
			{
				Dup data = new Dup ();
				data.Id = int.Parse(item.Attributes["Id"].Value);
				data.SceneName = item.Attributes ["SceneName"].Value;
				data.PointNum = int.Parse(item.Attributes["PointNum"].Value);
				data.PointValue = item.Attributes ["PointValue"].Value;
				data.Award = int.Parse(item.Attributes["Award"].Value);
				data.SwitchUI = item.Attributes ["SwitchUI"].Value;
				data.scene = item.Attributes ["scene"].Value;
				data.BgMusic = int.Parse(item.Attributes["BgMusic"].Value);


				_datas.Add (data.Id, data);
			}
		}
	}

	public static Dup FindById(int id)
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

	public static Dictionary<int, Dup> GetDatas()
	{
		if (_datas == null) 
		{
			Load ();
		}
		return _datas;
	}
}

