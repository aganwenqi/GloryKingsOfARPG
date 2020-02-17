using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBase : MonoBehaviour {

	//全局变量
	public GameObject _target;
	public Camera Camera;		//存放相机	用来旋转的节点，根节点用来移动
	public Transform XObj;
	public Transform YObj;


    /*.............防GC..................*/
    private Vector3 _offset = new Vector3(5, 3, 4);
    public void Binding(GameObject target)
	{
		//需要this
		this._target = target;
		//this.transform.localEulerAngles = _target.transform.localEulerAngles;
		this.transform.position = _target.transform.position + new Vector3(5, 3, 4);
		XObj.transform.LookAt (_target.transform.position + _target.transform.up.normalized * 1.5f);
	}

	public void Follow()
	{
        if(_target)
        {
            this.transform.position = _target.transform.position + _offset;
        }
		    
	}



    private void LateUpdate()
    {
        Follow();
    }


	/// <summary>
	/// 获取相机往前的方向(y轴设置为0)
	/// </summary>
	/// <returns>The forward.</returns>
	public Vector3 GetForward()
	{
		return new Vector3 (XObj.transform.forward.x, 0, XObj.transform.forward.z);
	}

	/// <summary>
	/// 获取相机往右的方向(y轴设置为0)
	/// </summary>
	/// <returns>The forward.</returns>
	public Vector3 GetRight()
	{
		return new Vector3 (XObj.transform.right.x, 0, XObj.transform.right.z);
	}


}
