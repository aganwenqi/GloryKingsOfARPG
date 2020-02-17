using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
class DupEvent : UnityEvent<Character, bool>
{

}
public class DupController : MonoBehaviour {
    public static DupController Instance;
    Character _player;
    /*true加入角色，false删除角色*/
    private DupEvent _listenFun = new DupEvent();
    private List<Character> _dupCharacters = new List<Character>();
    private DupResult _dupResult = new DupResult();

    /*玩家出生点*/
    public Transform playerPoint;
    /*玩家进入其他副本返回的点*/
    public Transform playerToBackPoint;
   

    /*当前副本信息*/
    private Dup _curDup;

    /*当前副本分解后的怪物信息，<第几点> = <怪物id:数量>*/
    private List<Dictionary<int, int>> _dupMonster = new List<Dictionary<int, int>>();

    /*当前有几个节点被消灭*/
    private int _beKillPointCount;
    void Awake()
    {
        Instance = this;
        _curDup = Dup.FindById(DupManager.Instance.CurDupId);
        if (_curDup == null)
            return;
       
        if(_curDup.PointNum > 0)
        {
            string[] pointMonster = _curDup.PointValue.Split('-');//点分割
            if (pointMonster.Length == 1 && pointMonster[0] == "")
            {

            }
            else
            {
                for (int i = 0; i < pointMonster.Length; i++)
                {
                    Dictionary<int, int> monster = DupManager.Instance.DicIIPool.Spawn();
                    monster.Clear();
                    /*每点怪物分割*/
                    foreach (string m in pointMonster[i].Split('、'))
                    {
                        string[] mAndnum = m.Split(':');
                        monster.Add(int.Parse(mAndnum[0]), int.Parse(mAndnum[1]));
                    }
                    _dupMonster.Add(monster);
                }
            }
            DupBirthPoint[] dupBirth = this.GetComponentsInChildren<DupBirthPoint>();
            foreach (DupBirthPoint item in dupBirth)
            {
                item.Init(this);
            }
        }
        _beKillPointCount = 0;
        _dupResult.Init(this);
    }

    /*音乐*/
    private string _bgSourceSid;
    private void Start()
    {
        StartCoroutine(InitPlayer());
        Dup dup = Dup.FindById(DupManager.Instance.CurDupId);
        if(dup == null)
        {
            return;
        }
        _bgSourceSid = SerialIdManager.Instance.GetSid();
        MusicManager.Instance.AddAudioSource(_bgSourceSid, gameObject.AddComponent<AudioSource>(), SourceType.bg);
        MusicUtil.PlayClip(_bgSourceSid, dup.BgMusic, true, SourceType.bg);
    }
    /*初始化玩家,并将玩家加入当前副本,同时将场景中的character同步到玩家身上*/
    IEnumerator InitPlayer()
    {
        while(true)
        {
            yield return new WaitForFixedUpdate();
            if (ResourceLoad.player != null)
            {
                _player = ResourceLoad.player;
                if(DupManager.Instance.FristIn == true || DupManager.Instance.CurDupId != 1000)//第一次进入主城，或不是主城，那么原点出生
                {
                    DupManager.Instance.FristIn = false;
                    _player.InitData(playerPoint.position, playerPoint.rotation);
                } 
                else//进入点出生
                    _player.InitData(playerToBackPoint.position, playerPoint.rotation);

                UICanvasManager.Instance.InitData();
                break;
            }
        }
    }
    /*当玩家死亡时要触发一些操作*/
    private void Update()
    {
        if(_player != null)
        {
            if(_player.IsDead)
            {
                //玩家死亡
                _dupResult.PlayerGetOver(_player);
                _player = null;
            }
        }
        _dupResult.Updata();
    }
    /*有节点怪物被杀完*/
    public void HavePointBeKill(int point)
    {
        if(++_beKillPointCount >= _dupMonster.Count)
        {
            //删了所有怪物，需要做通关结算或其他操作
            _dupResult.CrossDup();
        }
    }
    /*广播*/
    public void Broadcast(Character target, bool isAdd)
    {
        if (_listenFun != null)
            _listenFun.Invoke(target, isAdd);
    }
    #region 监听事件
    public void AddListen(UnityAction<Character, bool> call)
    {
        _listenFun.AddListener(call);
    }
    public void RemoveListen(UnityAction<Character, bool> call)
    {
        _listenFun.RemoveListener(call);
    }
    #endregion

    #region 添加和删除角色在副本
    public void AddCharacter(Character target)
    {
        _dupCharacters.Add(target);
        Broadcast(target, true);
    }
    public void RemoveCharacter(Character target)
    {
        _dupCharacters.Add(target);
        Broadcast(target, false);
    }
    #endregion
    #region 属性链接
    public List<Character> DupCharacters
    {
        get
        {
            return _dupCharacters;
        }
        set
        {
            _dupCharacters = value;
        }
    }

    public Dup CurDup
    {
        get
        {
            return _curDup;
        }

        set
        {
            _curDup = value;
        }
    }

    public List<Dictionary<int, int>> DupMonster
    {
        get
        {
            return _dupMonster;
        }

        set
        {
            _dupMonster = value;
        }
    }

    public Character Player
    {
        get
        {
            return _player;
        }

        set
        {
            _player = value;
        }
    }
    #endregion
    private void OnDestroy()
    {
        Instance = null;
        foreach (var item in _dupMonster)
        {
            DupManager.Instance.DicIIPool.UnSpawn(item);
        }
        if (ResourceLoad.player != null)
        {
            foreach (Character item in _dupCharacters)
            {
                ResourceLoad.player.CharacterDup.UpdateAttackTarget(item, false);
            }
        }
    }
}
