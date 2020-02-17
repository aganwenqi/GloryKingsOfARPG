using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MusicState
{
    Stop = 0,
    Play,
    Pause
}
public enum SoundTypes
{
    skill = 0,
    hero,
}
public enum SourceClip
{
    normlBt = 1001,//普通按钮
    selectBt = 1002,//选择英雄按钮
    beginBt = 1003,//开始游戏按钮
    openBt = 1004,//打开按钮
    quitBt = 1005,//退出按钮
    inJoinBt = 1006,//进入游戏按钮

    loginBg = 2001,//登陆背景音乐
    selectHeroBg = 2002,//选择英雄背景音乐
}
public enum SourceType
{
    bg,
    norml,
}
public class MusicUtil
{
    public static Clip GetSourceName(SourceClip clip)
    {
        return Clip.FindById((int)clip);
    }
    /*播放Clip里的音效*/
    public static void PlayClip(string sid, SourceClip clip, bool loop = false, SourceType type = SourceType.norml)
    {
        Clip c = MusicUtil.GetSourceName(clip);
        /*播放音效*/
        MusicManager.Instance.Play(sid, Utils.MusicPath + c.ClipName, c.Delay, loop, type);
    }

    /*播放音乐*/
    public static void PlayClip(string sid, int clipId, bool loop = false, SourceType type = SourceType.norml)
    {
        Clip c = Clip.FindById(clipId);
        if(c != null)
        {
            /*播放音效*/
            MusicManager.Instance.Play(sid, Utils.MusicPath + c.ClipName, c.Delay, loop, type);
        }
        
    }
}
