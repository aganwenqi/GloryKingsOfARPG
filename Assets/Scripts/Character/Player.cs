
using System.Collections.Generic;
using UnityEngine;

public class Player : Character {
    /// <summary>
    /// 相机旋转组件控制器 
    /// </summary>
    private CharacterCameraRotate _characterCameraRotate = new CharacterCameraRotate();

    public override void Init(CharacterUtilData data)
	{
        base.Init(data);
        _characterCameraRotate.Init(this);
    }
    // Update is called once per frame
    public override void Update () {
        
        if (IsDead)
            return;
        base.Update();
        _characterCameraRotate.Update (Time.deltaTime);

	}

	public override void LateUpdate()
	{
        if (IsDead)
            return;

        _characterCameraRotate.LateUpdate ();
	}
    #region 对象属性
    public CharacterCameraRotate CharacterCameraRotate
    {
        get
        {
            return _characterCameraRotate;
        }

        set
        {
            _characterCameraRotate = value;
        }
    }
    #endregion
}
