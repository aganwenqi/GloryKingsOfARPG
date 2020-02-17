using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using System.IO;

public class DataEditor : MonoBehaviour {

	[MenuItem("DataTool/自动生成cs文件")]
	public static void CreateCS()
	{
		//Debug.LogError ("点击自动生成Cs文件");

		//Debug.LogError ("Application.dataPath : " + Application.dataPath);		//工程路径   .....LearningTest/MTTest/Assets
		//Debug.LogError ("Application.persistentDataPath : " + Application.persistentDataPath);  //缓存路径

		//.....LearningTest/MTTest/Assets
		//.....LearningTest/Data


		//获取csv文件夹的路径
		string dataPath = Application.dataPath.Replace("Assets", "data/csv");
		//Debug.LogError (dataPath);

		//获取所有的csv文件的路径
		//获得所有数据放入_allDatas
		Dictionary<string, string> _allDatas = new Dictionary<string, string>();

		Dictionary<string, string> _resoucePath = new Dictionary<string, string> ();

        ArrayList csvFilePaths = new ArrayList();

        GetFiles (dataPath, csvFilePaths);

        for (int i = 0; i < csvFilePaths.Count; i++) {
            string fileName = csvFilePaths[i].ToString();
            string content = File.ReadAllText (fileName, System.Text.Encoding.Default);
			//Debug.LogError (csvFilePaths[i] + "\t\t" + "content : " + content);
			string[] strs = fileName.Split('\\');
			string key = strs [strs.Length - 1].Replace (".csv", "");

			_allDatas.Add (key, content);
			_resoucePath.Add (key, Application.dataPath + "/Scripts/Data/" + key + ".cs");
		}


		foreach (var item in _allDatas) {
			//Debug.LogError (item.Key);
			//Debug.LogError (item.Value);
			List<TableProperty> lst = TODO(item.Key, item.Value);

			string str = CreateCsString (item.Key, lst);

			//创建文件放到data文件夹里面
			//_resoucePath[item.Key]
			if (File.Exists(_resoucePath[item.Key])) {
				Debug.LogError ("存在，删除");
				File.Delete (_resoucePath [item.Key]);
			}
			StreamWriter sw = File.CreateText (_resoucePath [item.Key]);
			sw.WriteLine (str);
			sw.Close ();

		}


		AssetDatabase.Refresh ();



	}

	static List<TableProperty> TODO(string key, string value)
	{
		string[] lines = value.Split (new String[]{"\r\n"},StringSplitOptions.RemoveEmptyEntries);
		foreach (string item in lines) {
			Debug.LogError (item.ToString());	
		}

		//Id,名字,模型名,速度
		//Id,Name,ModelName,Speed
		//int,string,string,int

		string[] ZhuShis = lines [0].Split (',');
		string[] ZiDuanNames = lines [1].Split (',');
		string[] ShuXingNames = lines [2].Split (',');

		List<TableProperty> tablePropertys = new List<TableProperty> ();

		for (int i = 0; i < ZhuShis.Length; i++) {
			TableProperty tableP = new TableProperty ();
			tableP.ZhuShi = ZhuShis [i];
			tableP.ZiDuanName = ZiDuanNames [i];

			if ( ShuXingNames[i].ToLower().Equals("string")) 
			{
				tableP.DataPropertyType = DataPropertyType.StringValue;
			}
			else if ( ShuXingNames[i].ToLower().Equals("float")) 
			{
				tableP.DataPropertyType = DataPropertyType.Float;
			}
			else
			{
				tableP.DataPropertyType = DataPropertyType.Int;
			}

			tableP.Property = ShuXingNames [i].ToLower();

			tablePropertys.Add (tableP);
		}


		return tablePropertys;


	}

	public static string CreateCsString(string csName, List<TableProperty> tablePropertys)
	{
		string result = CsTemplete;


		//处理声明
		string shengming = "";
		for (int i = 0; i < tablePropertys.Count; i++) {
			string str = ShengMing;
			str = string.Format (str, tablePropertys [i].Property, tablePropertys [i].ZiDuanName, tablePropertys [i].ZhuShi);
			shengming += str;
		}

		//处理字段
		string ziduan = "";
		for (int i = 0; i < tablePropertys.Count; i++) {
			string str = "";
			if (tablePropertys [i].DataPropertyType == DataPropertyType.StringValue) {
				str = FuZhiByString;
			} else 
			{
				str = FuZhiByIntOrFloat;
			}


			str = string.Format (str, tablePropertys [i].ZiDuanName,tablePropertys [i].DataPropertyType == DataPropertyType.StringValue ? "" :  tablePropertys [i].Property);
			ziduan += str;
		}

		result = result.Replace ("$", csName);
		result = result.Replace ("%", shengming);
		result = result.Replace ("^", ziduan);
		//Debug.LogError (result);
		return result;

	}


	public static string CsTemplete = "using System.Collections;\n" +
		"using System.Collections.Generic;\n" +
		"using UnityEngine;\n" +
		"using System.Xml;\n\n" +
		"/// <summary>\n" +
		"/// 自动生成的代码 HeroData.xml导出来 \n" +
		"/// </summary>\n" +
		"public class $ : DataBase {\n\n" +
		"%\n\n" +
		"\tprivate static Dictionary<int, $> _datas;\n\n" +
		"\tpublic static void Load()\n" +
		"\t{\n" +
		"\t\t_datas = new Dictionary<int, $> ();\n" +
		"\t\tObject obj = Resources.Load (\"Data/\" + \"$\");\n" +
		"\t\tif (obj != null) \n\t\t{\n\t\t\tXmlDocument xmlDoc = new XmlDocument ();\n" +
		"\t\t\txmlDoc.LoadXml (obj.ToString());\n\n\t\t\tXmlNode xmlNode = xmlDoc.SelectSingleNode (\"$\");\n" +
		"\n\t\t\tforeach (XmlNode item in xmlNode.ChildNodes) \n" +
		"\t\t\t{\n\t\t\t\t$ data = new $ ();\n^\n\n" +
		"\t\t\t\t_datas.Add (data.Id, data);\n\t\t\t}\n\t\t}\n\t}\n\n" +
		"\tpublic static $ FindById(int id)\n\t{\n\t\tif (_datas == null) \n" +
		"\t\t{\n\t\t\tLoad ();\n\t\t}\n\n\t\tif (_datas.ContainsKey(id)) \n" +
		"\t\t{\n\t\t\treturn _datas [id];\n\t\t}\n\n\t\treturn null;\n" +
		"\t}\n" +
		"\n" +
		"\tpublic static Dictionary<int, $> GetDatas()\n\t{\n\t\tif (_datas == null) \n\t\t{\n" +
		"\t\t\tLoad ();\n\t\t}\n\t\treturn _datas;\n" +
		"\t}\n}\n";





	/// <summary>
	/// 获取文件夹里面的文件名
	/// </summary>
	/// <param name="path">Path.</param>
	public static void GetFiles(string path, ArrayList list)
	{
		//string[] files = Directory.GetFiles (path);
        //C#遍历指定文件夹中的所有文件 
        DirectoryInfo TheFolder = new DirectoryInfo(path);
        if (!TheFolder.Exists)
            return;

        //遍历文件
        foreach (FileInfo NextFile in TheFolder.GetFiles())
        {
            if (NextFile.Name == "0-0-11.grid")
                continue;
            // 获取文件完整路径
            string heatmappath = NextFile.FullName;
            string[] file = Directory.GetFiles(heatmappath);
            foreach(string item in file)
            {
                list.Add(item);
            }
        }
        //遍历文件夹
        foreach(DirectoryInfo nextFolder in TheFolder.GetDirectories())
        {
            GetFiles(nextFolder.FullName, list);
        }
	}

	public static void WriteFile(string csName, string content)
	{
		// 创建文件
		FileStream fs = new FileStream(csName + ".cs", FileMode.Create, FileAccess.ReadWrite); //可以指定盘符，也可以指定任意文件名，还可以为word等文件
		StreamWriter sw = new StreamWriter(fs); // 创建写入流
		sw.WriteLine(content); // 写入Hello World

		sw.Close(); //关闭文件
	}

	public static string ShengMing = "\tpublic {0} {1};\t\t//{2}\n";		//0属性  1字段名 2注释
	public static string FuZhiByIntOrFloat = "\t\t\t\tdata.{0} = {1}.Parse(item.Attributes[\"{0}\"].Value);\n";		//0字段名 1属性
	public static string FuZhiByString = "\t\t\t\tdata.{0}{1} = item.Attributes [\"{0}\"].Value;\n";				//0字段名
}


public class TableProperty
{
	public string ZhuShi;
	public string ZiDuanName;
	public DataPropertyType DataPropertyType;
	public string Property;

}

public enum DataPropertyType
{
	Int,
	Float,
	StringValue,
}