using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour {

    public static MusicManager Instance;

    /*当前全局的音量*/
    public static float _globalVolume = 0.8f;
    /*默认背景音量*/
    public float _normlBgVolume = 0.8f;
    /*默认效果音量*/
    public float _normlFxVolume = 0.8f;
    /*是否静音*/
    public static bool _globalMute;

    /*管理当前场景背景AudioSource(sid,AudioSource)*/
    private Dictionary<string, MusicSound> _bgSource = new Dictionary<string, MusicSound>();
    /*管理当前场景其他音效AudioSource(sid,AudioSource)*/
    private Dictionary<string, MusicSound> _normlSource = new Dictionary<string, MusicSound>();

    /*管理所有的AudioClip*/
    private Dictionary<string, AudioClip> _allClip = new Dictionary<string, AudioClip>();
    private void Awake()
    {
        Instance = this;
    }
    private Dictionary<string, MusicSound> GetSourceDic(SourceType sourceType)
    {
        if(sourceType == SourceType.norml)
        {
            return _normlSource;
        }
        else if(sourceType == SourceType.bg)
        {
            return _bgSource;
        }
        return null;
    }
    /*播放:音源，文件，延时播放，循环，什么类型音源，音量*/
    public void Play(string sidSource, string sidClip, float delayPlay = 0, bool loop = false, SourceType sourceType = SourceType.norml, float volume = -1000)
    {
        MusicSound sound = null;
        Dictionary<string, MusicSound>  sources = GetSourceDic(sourceType);
        if (!sources.TryGetValue(sidSource, out sound))
        {
            Debug.LogError("AudioSource不存在");
            return;
        }
        AudioClip clip = GetClip(sidClip);
        if(clip == null)
        {
            return;
        }

        sound.Play(clip, loop, delayPlay);
        if(volume != -1000)
        {
            sound.Volume = volume * _globalVolume;
        }
    }
    /*停止*/
    public void Stop(string sidSource, string sidClip, SourceType sourceType = SourceType.norml)
    {
        MusicSound sound = null;
        Dictionary<string, MusicSound> sources = GetSourceDic(sourceType);
        if (!sources.TryGetValue(sidSource, out sound))
        {
            Debug.LogError("AudioSource不存在");
            return;
        }
        AudioClip clip = GetClip(sidClip);
        if (clip == null)
        {
            return;
        }
        sound.Stop(clip);
    }
    /*添加声源*/
    public void AddAudioSource(string sid, AudioSource source, SourceType sourceType = SourceType.norml)
    {
        Dictionary<string, MusicSound> sources = GetSourceDic(sourceType);
        if (sources.ContainsKey(sid))
            return;

        MusicSound sound = new MusicSound();
        sound.Init(this, source, _globalVolume, sid);
        sources.Add(sid, sound);
    }
    /*删除声源*/
    public void RemoveAudioSource(string sid, bool stop = false, SourceType sourceType = SourceType.norml)
    {
        Dictionary<string, MusicSound> sources = GetSourceDic(sourceType);
        if (stop)
        {
            MusicSound sound = null;
            if(sources.TryGetValue(sid, out sound))
            {
                sound.State = MusicState.Stop;
            }
        }
        sources.Remove(sid);
    }
    /*获得Clip*/
    private AudioClip GetClip(string sid)
    {
        AudioClip clip = null;
        if(_allClip.TryGetValue(sid, out clip))
        {
            return clip;
        }
        clip = LoadClip(sid);
        return clip;
    }
    /*加载资源*/
    private AudioClip LoadClip(string sid)
    {
        ResourceRequest resource = Resources.LoadAsync(sid);
        if(resource.asset == null)
        {
           // Debug.Log("声音文件不存在:" + sid);
            return null;
        }
        return resource.asset as AudioClip;
    }

    /*设置音量*/
    public void SetVolume(float volume, SourceType sourceType)
    {
        _globalVolume = volume;
        Dictionary<string, MusicSound> sources = GetSourceDic(sourceType);
        foreach(var item in sources)
        {
            item.Value.Volume = _globalVolume;
        }
    }
    void Update () {
        float timing = Time.deltaTime;

        foreach (var item in _normlSource)
        {
            item.Value.Update(timing);
        }
        foreach (var item in _bgSource)
        {
            item.Value.Update(timing);
        }
    }
}
