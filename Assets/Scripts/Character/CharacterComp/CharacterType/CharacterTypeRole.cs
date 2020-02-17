using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterTypeRole : CharacterTypeBase {

	public override void Init (Character character)
	{
		base.Init (character);
		CameraManager.Instance.RoleCamera.Binding (_character.gameObject);

	}

	public override CameraBase GetCameraBase ()
	{
		return CameraManager.Instance.RoleCamera;
	}
}
