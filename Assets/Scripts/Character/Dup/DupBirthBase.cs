using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class DupBirthBase : MonoBehaviour {

    private DupController _dupControl;
    /*全部怪物消失*/
    public UnityEvent killAllMonster;

    /*第几点*/
    public int pointNumber;

    /*怪物出生点*/
    public List<Transform> _points = new List<Transform>();

    /*当前点维护的怪物*/
    private List<Character> _allMonster = new List<Character>();
    public virtual void Init(DupController dupControl)
    {
        _dupControl = dupControl;
        Dictionary<int, int> monsters = _dupControl.DupMonster[pointNumber];
        int pointCount = 0;
        foreach(var item in monsters)//创建怪物
        {
            for(int i = 0; i < item.Value; i++)
            {
                Character monster = CharacterManager.Instance.CreateCharacter(item.Key, _points[pointCount++].position);
                //monster.transform.parent = _points[pointCount++];
                monster.gameObject.SetActive(false);
                _allMonster.Add(monster);
            }
        }
        _dupControl.AddListen(this.UpdataCharacter);
    }

    /*向DupManager注册了添加和删除怪物监听,用于判断是否消灭完这个点怪物*/
    public void UpdataCharacter(Character target, bool isAdd)
    {
        if (isAdd)
            return;

        if(_allMonster.Contains(target))
        {
            _allMonster.Remove(target);
            //Debug.Log("sdfsddddddddddfffffffff");
            if (_allMonster.Count == 0)
            {
                killAllMonster.Invoke();
                _dupControl.HavePointBeKill(pointNumber);
            }
        }
        Debug.Log(pointNumber +"dian:" + _allMonster.Count);
            
    }
    /*玩家进来区域激活怪物*/
    private void OnTriggerEnter(Collider other)
    {
        Character target = other.GetComponent<Character>();
        if(target != null && target.CharacterUtilData.characterType == CharacterType.Player)
        {
            foreach(Character item in _allMonster)
            {
                item.gameObject.SetActive(true);
            }
            this.GetComponent<Collider>().enabled = false;
        }
    }
    private void OnDisable()
    {
        if(_dupControl != null)
            _dupControl.RemoveListen(this.UpdataCharacter);
    }
    #region 对象属性
    public DupController DupControl
    {
        get
        {
            return _dupControl;
        }

        set
        {
            _dupControl = value;
        }
    }
    #endregion
}
