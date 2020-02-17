using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 单例模式 : 设计模式最简单的一个模式，这个对象在游戏世界中有且只有一个
/// </summary>
public class CameraManager : MonoBehaviour {

	public static CameraManager Instance;

	/// <summary>
	/// 主角相机
	/// </summary>
	public CameraBase RoleCamera;

	// Use this for initialization
	void Awake () {
		Instance = this;
	}

    public void SetCameraBase(CameraBase roleCamera)
    {
        RoleCamera = roleCamera;
    }
}
