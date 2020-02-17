using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HeroSelectButton : MonoBehaviour {
    public Image Icon;
    private HeroSelectController _manager;
    private HeroData _heroData;
    public void Init(HeroSelectController manager, HeroData heroData)
    {
        _manager = manager;
        _heroData = heroData;
        UpdataUI();
        this.GetComponent<ButtonState>().onPointerDown.AddListener(Invoke);
        
    }
    private void UpdataUI()
    {
        Sprite sprite = Resources.Load<Sprite>("Picture/HeroIcon/" + _heroData.Icon);
        Icon.sprite = sprite;
    }
    /*按钮按下*/
    private void Invoke()
    {
        _manager.BtnReCall(_heroData);
    }
}
