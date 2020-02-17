using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;

/// <summary>
/// 自动生成的代码 HeroData.xml导出来 
/// </summary>
public class CharcaterOtherClip : DataBase {

	public int Id;		//id
	public string Name;		//名字
	public string HitClip;		//受击音效
	public float DelayHit;		//延时播放
	public string DeadClip;		//死亡音效
	public float DelayDead;		//延时播放
	public string Speak1;		//台词音效一
	public float DelaySp1;		//延时播放


	private static Dictionary<int, CharcaterOtherClip> _datas;

	public static void Load()
	{
		_datas = new Dictionary<int, CharcaterOtherClip> ();
		Object obj = Resources.Load ("Data/" + "CharcaterOtherClip");
		if (obj != null) 
		{
			XmlDocument xmlDoc = new XmlDocument ();
			xmlDoc.LoadXml (obj.ToString());

			XmlNode xmlNode = xmlDoc.SelectSingleNode ("CharcaterOtherClip");

			foreach (XmlNode item in xmlNode.ChildNodes) 
			{
				CharcaterOtherClip data = new CharcaterOtherClip ();
				data.Id = int.Parse(item.Attributes["Id"].Value);
				data.Name = item.Attributes ["Name"].Value;
				data.HitClip = item.Attributes ["HitClip"].Value;
				data.DelayHit = float.Parse(item.Attributes["DelayHit"].Value);
				data.DeadClip = item.Attributes ["DeadClip"].Value;
				data.DelayDead = float.Parse(item.Attributes["DelayDead"].Value);
				data.Speak1 = item.Attributes ["Speak1"].Value;
				data.DelaySp1 = float.Parse(item.Attributes["DelaySp1"].Value);


				_datas.Add (data.Id, data);
			}
		}
	}

	public static CharcaterOtherClip FindById(int id)
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

	public static Dictionary<int, CharcaterOtherClip> GetDatas()
	{
		if (_datas == null) 
		{
			Load ();
		}
		return _datas;
	}
}

