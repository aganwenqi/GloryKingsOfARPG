using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicSound
{
    private MusicManager _manager;
    private string _sid;
    /*当前的音量*/
    private float _volume;
    /*静音*/
    private bool _mute;
    /*循环播放*/
    private bool _loop;
    /*状态*/
    private MusicState _state;

    /*延时播放*/
    private float _delayPlay;
    private float _delayTime;
    private bool _playEnable;
    private AudioClip _clip;
    private AudioSource _source;


    public void Init(MusicManager manager, AudioSource source, float normlVolume, string sid)
    {
        _manager = manager;
        _source = source;
        _volume = normlVolume;
        _sid = sid;
        InitData();
    }
    public void InitData()
    {
        _delayPlay = 0;
        _delayTime = 0;
        _playEnable = true;
        _source.Stop();
        _state = MusicState.Stop;
    }
    /*播放。文件，延时播放，是否循环*/
    public void Play(AudioClip clip, bool loop = false, float delayPlay = 0)
    {
        _clip = clip;
        _loop = loop;
        _delayPlay = delayPlay;
        _delayTime = 0;
        _playEnable = false;

        _state = MusicState.Stop;
        _source.Stop();
        _source.clip = _clip;
        _source.loop = loop;
        Update(Time.deltaTime);//事先更新一帧
    }
    public void Stop(AudioClip clip)
    {
        _clip = clip;
        if(_clip == clip)
        {
            InitData();
        }
    }
    /*音量设置*/
    public float Volume
    {
        get
        {
            return _volume;
        }

        set
        {
            _volume = value;
            _source.volume = _volume;
        }
    }
    /*静音设置*/
    public bool Mute
    {
        get
        {
            return _mute;
        }

        set
        {
            _mute = value;
            _source.mute = _mute;
        }
    }

    /*状态设置*/
    public MusicState State
    {
        get
        {
            return _state;
        }

        set
        {
            _state = value;
            if(_state == MusicState.Play)
            {
                _source.Play();
            }
            else if(_state == MusicState.Pause)
            {
                _source.Pause();
            }
            else if(_state == MusicState.Stop)
            {
                _source.Stop();
            }
        }
    }

    public void Update(float timing)
    {
        if(null == _source)
        {
            return;
        }

        _delayTime += timing;
        if(!_playEnable)
        {
            if(_delayTime >= _delayPlay)
            {
                _playEnable = true;
                _source.Play();
                _state = MusicState.Play;
            }
        }
        if(_state == MusicState.Play && !_source.isPlaying)//停止
        {
            _state = MusicState.Stop;
        }
    }
}
