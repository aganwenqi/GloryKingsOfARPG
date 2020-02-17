using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager {
	private static CharacterManager _instance;

    public PoolBase _AllCharacterPool = new PoolBase();
    public static CharacterManager Instance
	{
		get{ 
			if (_instance == null) {
				_instance = new CharacterManager ();
			}
			return _instance;
		}
	}

	private List<Character> _allCharacters = new List<Character>();
	public List<Character> AllCharacters
	{
		get{
			return _allCharacters;
		}
	}
	public Character CreateCharacter(int id, Vector3 initPos)
	{
        CharacterUtilData data = new CharacterUtilData();
        HeroData heroData = HeroData.FindById(id);
        data.sid = SerialIdManager.Instance.GetSid();
        data.heroId = id;
        data.characterType = (CharacterType)heroData.characterType;
        data.modelPath = Utils.GetHeroPath(data.characterType);
        

        Character character = _AllCharacterPool.Spawn(data.modelPath + heroData.ModelName) as Character;
        if(character == null)
        {
            return null;
        }
        character.transform.position = initPos;
        character.transform.rotation = Quaternion.identity;
        //character.transform.localScale = Vector3.one;
        character.Init(data);

        if (data.characterType == CharacterType.Player) {
            UICanvasManager.Instance.Init(character);
            List<BaseUI> showUI = new List<BaseUI>();
            showUI.Add(BattleUI.Instance);
            showUI.Add(MenuPanelUI.Instance);
            UICanvasManager.Instance.OnEnterUI(showUI);
            character.CharacterAttribute.AttControl.UpdataState(true);
        }

        _allCharacters.Add (character);
        if(DupController.Instance != null)
        {
            DupController.Instance.AddCharacter(character);
        }
        return character;
    }
    /// <summary>
	/// 回收英雄
	/// </summary>
    public void RemoveCharacter(Character character)
    {
        if(_allCharacters.Contains(character))
        {
            _allCharacters.Remove(character);
        }
        _AllCharacterPool.UnSpawn(character.Path, character);
        if (DupController.Instance != null)
        {
            DupController.Instance.RemoveCharacter(character);
        }
    }
    /// <summary>
	/// 删除英雄
	/// </summary>
    public void DeleteCharacter(Character character)
    {
        //_AllCharacterPool.DeleteSpawn(character.Path, character);
    }
    /*
    /// <summary>
    /// 获取副本敌方英雄 
    /// </summary>
    public List<Character> FindOppoCharacters(Character scr)
	{
		List<Character> results = new List<Character> ();
		
		for (int i = 0; i < _allCharacters.Count; i++)
        {
            if(CanAttack(scr, _allCharacters[i]))
            {
                results.Add(_allCharacters[i]);
            }
		}
		return results;
    }*/
    /// <summary>
    /// 判断是否可攻击
    /// </summary>
    public bool CanAttack(Character scr, Character dst)
    {
        if (scr == null || dst == null || scr == dst)
        {
            return false;
        }
        if(dst.IsDead)
        {
            return false;
        }
        return IsAttackTarget(scr, dst);
    }
    /// <summary>
    /// 是否为攻击对象
    /// </summary>
    public bool IsAttackTarget(Character scr, Character dst)
    {
        AttackTarget attackTarget = scr.CharacterDup.AttackTarget;
        //先判断是否为攻击对象
        if (attackTarget == AttackTarget.Player)
        {
            if (dst.CharacterUtilData.characterType != CharacterType.Player)
                return false;
        }
        else if (attackTarget == AttackTarget.Monster)
        {
            if (dst.CharacterUtilData.characterType != CharacterType.Monster)
                return false;
        }
        return true;
    }
}
