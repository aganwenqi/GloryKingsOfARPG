using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;


/// <summary>
/// 工具类 
/// </summary>
public class Utils
{
	public static string PlayerPath = "Prefab/Character/Player/";
    public static string MonsterPath = "Prefab/Character/Monster/";
    public static string CharacterPath_Shower = "Prefab/Shower/";//展示模型路径
    public static string FxPath = "Prefab/Fx/";
    public static string BulletPath = "Prefab/Bullet/";  //弹道

    public static string MusicPath = "Music/";//音乐文件路径
    public static string GetHeroPath(CharacterType type)
    {
        if (type == CharacterType.Player)
            return PlayerPath;
        else if (type == CharacterType.Monster)
            return MonsterPath;

        return null;
    }
}