using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HeroSelectController : MonoBehaviour {

    public static int HeroId = 1001;//选择角色的id,用于主场景加载角色用

    /*所有展示角色*/
    private Dictionary<int, GameObject> _allModel = new Dictionary<int, GameObject>();

    /*开始按钮*/
    public Button BeginBt;
    /*角色选择按钮父节点*/
    public Transform BtnParent;

    /*当前展示的英雄heroData和英雄*/
    private HeroData _curHeroData;
    private GameObject _curHero;

    /*audioSource1 的sid*/
    private string _speakSid;
    /*audioSource2 的sid*/
    private string _btSid;
    /*audioSource3 的sid*/
    private string _bgSid;
    void Start () {
        LoadHero();
        BeginBt.onClick.AddListener(this.Begin);
        BeginBt.gameObject.SetActive(false);

        _speakSid = SerialIdManager.Instance.GetSid();
        _btSid = SerialIdManager.Instance.GetSid();
        _bgSid = SerialIdManager.Instance.GetSid();
        
        MusicManager.Instance.AddAudioSource(_speakSid, gameObject.AddComponent<AudioSource>());
        MusicManager.Instance.AddAudioSource(_btSid, gameObject.AddComponent<AudioSource>());
        MusicManager.Instance.AddAudioSource(_bgSid, gameObject.AddComponent<AudioSource>(), SourceType.bg);
        //播放背景音乐
        MusicUtil.PlayClip(_bgSid, SourceClip.selectHeroBg, true, SourceType.bg);
    }
    /*加载角色*/
    private void LoadHero()
    {
        Dictionary<int, HeroData> heroDatas = HeroData.GetDatas();
        int count = 0;
        GameObject bt = null;
        foreach (var item in heroDatas)
        {
            if(LoginPanel.Admin == false)//不是管理员过滤角色
            {
                if(AdminHero.FindById(item.Value.Id) != null)
                {
                    continue;
                }
            }
            HeroData hero = item.Value;
            if (hero.characterType == (int)CharacterType.Player)
            {
                object obj = Resources.Load(Utils.CharacterPath_Shower + hero.ShowModel);
                if (obj == null)
                {
                    Debug.LogError("没有展示模型");
                    continue;
                }
                GameObject model = GameObject.Instantiate(obj as GameObject);
                model.transform.parent = this.transform;
                model.transform.localPosition = Vector3.zero;
                model.transform.localRotation = Quaternion.identity;
                model.SetActive(false);
                _allModel.Add(hero.Id, model);

                //初始化按钮
                Sprite sprite = Resources.Load<Sprite>("Picture/HeroIcon/" + hero.Icon);
                bt = ((GameObject)Instantiate((Resources.Load("Prefab/UI/HeroSelect/heroSelectBt") as GameObject)));
                bt.transform.SetParent(BtnParent);
                (bt.transform as RectTransform).anchoredPosition3D = Vector3.zero;
                bt.transform.localScale = Vector3.one;
                bt.transform.localRotation = Quaternion.Euler(0, 0, -90);
                bt.GetComponent<HeroSelectButton>().Init(this, hero);
                count++;
            }
        }
        if (bt)
        {
            float height = count * (bt.transform as RectTransform).rect.width;
            RectTransform rect = BtnParent.GetComponent<RectTransform>();
            rect.sizeDelta = new Vector2(rect.sizeDelta.x, height);
        }
    }
    /*英雄选择按钮回调*/
    public void BtnReCall(HeroData herData)
    {
        if(!BeginBt.gameObject.activeSelf)
        {
            BeginBt.gameObject.SetActive(true);
        }
        if(_curHeroData == herData)
        {
            return;
        }
        if(_curHero != null)
        {
            _curHero.SetActive(false);
        }
        _curHeroData = herData;
        GameObject obj = null;
        if(_allModel.TryGetValue(_curHeroData.Id, out obj))
        {
            _curHero = obj;
            _curHero.SetActive(true);
            _curHero.GetComponent<HeroShowAnimator>().PlayShower();
        }
        /*播放英雄音效*/
        CharcaterOtherClip otherClip = CharcaterOtherClip.FindById(herData.Sounds);
        if (null != otherClip)
        {
            
            MusicManager.Instance.Play(_speakSid, Utils.MusicPath + otherClip.Speak1, otherClip.DelaySp1);
        }
        /*播放按钮音效*/
        MusicUtil.PlayClip(_btSid, SourceClip.selectBt);
    }
    /*开始按钮按下回调*/
    public void Begin()
    {
        if(_curHeroData != null)
        {
            HeroId = _curHeroData.Id;
            DupManager.Instance.FristIn = true;
            SceneController.ChangeScene(1000);
        }
        /*播放按钮音效*/
        MusicUtil.PlayClip(_btSid, SourceClip.beginBt);
    }
    #region 对象属性
    public Dictionary<int, GameObject> AllModel
    {
        get
        {
            return _allModel;
        }

        set
        {
            _allModel = value;
        }
    }
    #endregion
}
