using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleUI : BaseUI {
    public static BattleUI Instance;

	public BattleAxisPanel BattleAxisPanel; 

	public SkillPanel SkillPanel;

    

    void Awake()
	{
		Instance = this;
	}

	public override void Init(Character character)
	{
        base.Init();
        Character = character;
		BattleAxisPanel.Init (this);
		SkillPanel.Init (this);

    }

    public override void OnEnter()
    {
        base.OnEnter();
        (this.Character as Player).CharacterCameraRotate.Using = true;
    }
    public override void OnQuit()
    {
        base.OnQuit();
        Character.StateControl.ChangeState(StateType.Idle);
        (this.Character as Player).CharacterCameraRotate.Using = false;
    }
}
