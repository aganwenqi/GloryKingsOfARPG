using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SettingFristPanel : InfoPanelBase {

    //背景音乐控制
    public Slider bgVolume;
    public Text bgVolumeText;
    private float _curBgVal;
    //效果音乐控制
    public Slider fxVolume;
    public Text fxVolumeText;
    private float _curFxVal;

    //帧数控制
    public Toggle fpsToggle;
    private int _tallFps = 60;
    private int _desFps = 30;
    private void Awake()
    {
        bgVolume.value = MusicManager.Instance._normlBgVolume;
        fxVolume.value = MusicManager.Instance._normlFxVolume;
        _curBgVal = bgVolume.value;
        _curFxVal = fxVolume.value;
        bgVolumeText.text = (bgVolume.value * 100).ToString("0");
        fxVolumeText.text = (fxVolume.value * 100).ToString("0");
        fpsToggle.isOn = true;
        Application.targetFrameRate = _tallFps;
        fpsToggle.onValueChanged.AddListener(ChangeFps);
    }
    /*监听fps开关*/
    private void ChangeFps(bool isOn)
    {
        Application.targetFrameRate = fpsToggle.isOn ? _tallFps : _desFps;
    }

    public override void Init(BaseUI manager)
    {
        base.Init(manager);
        MusicManager.Instance.SetVolume(_curBgVal, SourceType.bg);
        MusicManager.Instance.SetVolume(_curBgVal, SourceType.norml);
    }
    private void Update()
    {
        if(_curBgVal != bgVolume.value)//背景
        {
            _curBgVal = bgVolume.value;
            bgVolumeText.text = (bgVolume.value * 100).ToString("0");
            MusicManager.Instance.SetVolume(_curBgVal, SourceType.bg);
        }
        if (_curFxVal != fxVolume.value)//效果
        {
            _curFxVal = fxVolume.value;
            fxVolumeText.text = (fxVolume.value * 100).ToString("0");
            MusicManager.Instance.SetVolume(_curFxVal, SourceType.norml);
        }
    }
}
