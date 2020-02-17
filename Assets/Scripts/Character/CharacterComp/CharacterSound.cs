using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSound : CharacterCompBase
{
    /*基础音效，比如说话、受伤等*/
    private CharcaterOtherClip _baseClip;
    private string skillSoundSid;
    private string heroSoundSid;

    public override void Init(Character character)
    {
        base.Init(character);
        skillSoundSid = SerialIdManager.Instance.GetSid();
        heroSoundSid = SerialIdManager.Instance.GetSid();  
        _baseClip = CharcaterOtherClip.FindById(_character.HeroData.Sounds);
        InitData();
    }
    public override void InitData()
    {
        base.InitData();
        SoundType[] sounds = _character.GetComponentsInChildren<SoundType>();
        foreach (SoundType sound in sounds)
        {
            if (sound.soundType == SoundTypes.hero)
            {
                MusicManager.Instance.AddAudioSource(heroSoundSid, sound.GetComponent<AudioSource>());
            }
            else if (sound.soundType == SoundTypes.skill)
            {
                MusicManager.Instance.AddAudioSource(skillSoundSid, sound.GetComponent<AudioSource>());
            }
        }
    }
    /*播放技能音效*/
    public void PlaySkillSound(string name, float delayPlay = 0, bool loop = false, float volume = -1000)
    {
        if(name.Equals(""))
        {
            return;
        }
        MusicManager.Instance.Play(skillSoundSid, Utils.MusicPath + name, delayPlay, loop, SourceType.norml, volume);
    }
    /*播放角色音效*/
    public void PlayHeroSound(string name, float delayPlay = 0, bool loop = false, float volume = -1000)
    {
        if (name.Equals(""))
        {
            return;
        }
        MusicManager.Instance.Play(heroSoundSid, Utils.MusicPath + name, delayPlay, loop, SourceType.norml, volume);
    }
    /*停止技能音效*/
    public void StopSkillSound(string name)
    {
        if (name.Equals(""))
        {
            return;
        }
        MusicManager.Instance.Stop(skillSoundSid, Utils.MusicPath + name, SourceType.norml);
    }
    /*停止角色音效*/
    public void StopHeroSound(string name)
    {
        if (name.Equals(""))
        {
            return;
        }
        MusicManager.Instance.Stop(heroSoundSid, Utils.MusicPath + name, SourceType.norml);
    }
    public override void UnSpawn()
    {
        base.UnSpawn();
        MusicManager.Instance.RemoveAudioSource(skillSoundSid);
        MusicManager.Instance.RemoveAudioSource(heroSoundSid);
    }
    #region 对象属性
    public CharcaterOtherClip BaseClip
    {
        get
        {
            return _baseClip;
        }

        set
        {
            _baseClip = value;
        }
    }
    #endregion
}
