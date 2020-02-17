using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AttributePanel : InfoPanelBase {

    private Text _phy;
    private Text _magic;
    private Text _phyDefend;
    private Text _magicDefend;
    private Text _power;
    private Text _spell;
    private Text _hp;
    private Text _mp;
    private Text _speed;
    private Text _crit;
    private Text _role;
    public override void Init(BaseUI manager)
    {
        base.Init(manager);
        
        Transform value = this.transform.Find("value");
        _phy = value.Find("phy").GetComponent<Text>();
        _magic = value.Find("magic").GetComponent<Text>();
        _phyDefend = value.Find("phyDefend").GetComponent<Text>();
        _magicDefend = value.Find("magicDefend").GetComponent<Text>();
        _power = value.Find("power").GetComponent<Text>();
        _spell = value.Find("spell").GetComponent<Text>();
        _hp = value.Find("hp").GetComponent<Text>();
        _mp = value.Find("mp").GetComponent<Text>();
        _speed = value.Find("speed").GetComponent<Text>();
        _crit = value.Find("crit").GetComponent<Text>();
        _role = value.Find("role").GetComponent<Text>();
        InfoType = InfoBtType.attribute;

        _role.text = ((InformationUI)manager).Character.HeroData.Name;
    }
    /*设置UI显示属性*/
    public void SetAttribute(List<float> att)
    {
        string value;
        for(int i = 0; i < att.Count; i++)
        {
            value = ((int)att[i]).ToString();
            switch (i)
            {
                case 0:
                    _speed.text = value;
                    break;
                case 1:
                    _phy.text = value;
                    break;
                case 2:
                    _magic.text = value;
                    break;
                case 3:
                    _phyDefend.text = value;
                    break;

                case 4:
                    _magicDefend.text = value;
                    break;
                case 5:
                    _power.text = value;
                    break;
                case 6:
                    _spell.text = value;
                    break;
                case 7:
                    _crit.text = value;
                    break;
                case 8:
                    _hp.text = value;
                    break;
                case 9:
                    _mp.text = value;
                    break;
            }
        }
    }
}
