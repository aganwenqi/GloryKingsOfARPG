using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCameraRotate : CharacterCompBase {

	private float _rotateSpeedX = 100;
	private float _rotateSpeedY = 90;
	private CameraBase _cameraBase;
	public CameraBase CameraBase
	{
		get
		{
			return _cameraBase;
		}
	}

	public bool Using = true;

	public override void Init (Character character)
	{
		base.Init (character);
		_cameraBase = _character.CharacterTypeInstance.GetCameraBase();
		Using = true;
	}

	public override void Update (float _timing)
	{
		base.Update (_timing);
	}


	//float 

	public override void LateUpdate ()
	{
		if (_cameraBase == null) {
			return;
		}

		if (!Using) {
			return;
		}

		base.LateUpdate ();
#if UNITY_EDITOR
        if (Input.GetMouseButton(0))
        {
            _cameraBase.XObj.transform.RotateAround(_character.transform.position, Vector3.up, Input.GetAxis("Mouse X") * Time.deltaTime * _rotateSpeedX);

            float ySpeed = Input.GetAxis("Mouse Y") * Time.deltaTime * _rotateSpeedY;
            _cameraBase.YObj.transform.RotateAround(_character.transform.position, -_cameraBase.XObj.right, ySpeed);
            if (_cameraBase.YObj.transform.position.y <= _character.transform.position.y + 0.4)
            {
                _cameraBase.YObj.transform.RotateAround(_character.transform.position, _cameraBase.XObj.right, ySpeed);
            }

        }
#elif (UNITY_IOS || UNITY_ANDROID)
        if (Input.GetMouseButton(0))
        {
            if (Input.touchCount == 1 || Input.touchCount == 2)
            {
                if (Input.GetTouch(0).phase == TouchPhase.Moved)
                {
                    _cameraBase.XObj.transform.RotateAround(_character.transform.position, Vector3.up, Input.GetAxis("Mouse X") * Time.deltaTime * _rotateSpeedX);

                    float ySpeed = Input.GetAxis("Mouse Y") * Time.deltaTime * _rotateSpeedY;
                    _cameraBase.YObj.transform.RotateAround(_character.transform.position, -_cameraBase.XObj.right, ySpeed);
                    if (_cameraBase.YObj.transform.position.y <= _character.transform.position.y + 0.4)
                    {
                        _cameraBase.YObj.transform.RotateAround(_character.transform.position, _cameraBase.XObj.right, ySpeed);
                    }
                }
            }
        }
#endif

    }
}
