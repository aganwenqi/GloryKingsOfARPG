using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class AxisBtn : MonoBehaviour, IDragHandler, IPointerDownHandler , IPointerUpHandler {

	private BattleAxisPanel _manager;
    private Camera _uiCamera;

    private Vector3 _setPosition = new Vector3();

    
    public void Start()
    {
        
    }
    public void Init(BattleAxisPanel manager)
	{
		_manager = manager;
        _uiCamera = UICanvasManager.Instance.UICamera2D;
        
    }



	public void OnDrag (PointerEventData eventData)
	{
		//eventData.pointerDrag.transform.position = Input.mousePosition;

	}

	private bool _drag;

	public void OnPointerDown (PointerEventData eventData)
	{
		_drag = true;
		(_manager.Manager.Character as Player).CharacterCameraRotate.Using = false;
	}

	public void OnPointerUp (PointerEventData eventData)
	{
		_drag = false;
		this.transform.localPosition = Vector3.zero;
        (_manager.Manager.Character as Player).CharacterCameraRotate.Using = true;
	}


	/// <summary>
	/// 每一帧执行一次
	/// </summary>
	void Update()
	{
        if(_manager == null || _manager.Manager.Character.IsDead)
        {
            return;
        }
        if(Application.platform == RuntimePlatform.Android)
        {
            if (Input.touchCount == 1)
            {
                if (_drag)
                {
                    if (Vector3.Distance(this.transform.localPosition, Vector3.zero) <= _r)
                    {
                        Vector3 v = _uiCamera.ScreenToWorldPoint(Input.mousePosition);
                        _setPosition.x = v.x;
                        _setPosition.y = v.y;
                        _setPosition.z = this.transform.position.z;
                        this.transform.position = _setPosition;
                    }
                    else
                    {

                    }

                    if (Vector3.Distance(this.transform.localPosition, Vector3.zero) >= _r)
                    {
                        this.transform.localPosition = ((this.transform.localPosition - Vector3.zero).normalized * _r) + Vector3.zero;
                    }
                }

                MoveLogic();
                return;
            }
        }
        else
        {
            KeyboardLogic();
            if (_drag)
            {
                if (Vector3.Distance(this.transform.localPosition, Vector3.zero) <= _r)
                {
                    Vector3 v = _uiCamera.ScreenToWorldPoint(Input.mousePosition);
                    _setPosition.x = v.x;
                    _setPosition.y = v.y;
                    _setPosition.z = this.transform.position.z;
                    this.transform.position = _setPosition;
                }
                else
                {

                }

                if (Vector3.Distance(this.transform.localPosition, Vector3.zero) >= _r)
                {
                    this.transform.localPosition = ((this.transform.localPosition - Vector3.zero).normalized * _r) + Vector3.zero;
                }
            }
            MoveLogic();
        }
        
    }

    private float _r = 73;

	float _x;
	float _y;

	Vector3 _nor;

	void MoveLogic()
	{
        if (_manager.Manager.Character.CharacterSkill.Skilling)
        {
            return;
        }
        _nor = (this.transform.localPosition - Vector3.zero).normalized;

		_x = _nor.x;
		_y = _nor.y;

		//求出相机超前面的分量
		Vector3 forwardV = ((_manager.Manager.Character as Player).CharacterCameraRotate.CameraBase.GetForward ().normalized * _y);

		Vector3 rightV = ((_manager.Manager.Character as Player).CharacterCameraRotate.CameraBase.GetRight ().normalized * _x);

		if (this.transform.localPosition.Equals(Vector3.zero)) {
			_manager.Manager.Character.StateControl.ChangeState (StateType.Idle);

		} else 
		{


			_manager.Manager.Character.StateControl.ChangeState (StateType.Run);
			StateRun stateRun = _manager.Manager.Character.StateControl.CurState as StateRun;

			if (stateRun != null) {
				stateRun.SetDir ((forwardV + rightV).normalized, 0);
			}
		}

	}


	private Vector3 _keyBoardValue;
	void KeyboardLogic()
	{
		_keyBoardValue = Vector3.zero;
        /*
		if (Input.GetKey(KeyCode.W)) {
			_keyBoardValue.y = _r;
		}
		if (Input.GetKey(KeyCode.S)) {
			_keyBoardValue.y = - _r;
		}
		if (Input.GetKey(KeyCode.A)) {
			_keyBoardValue.x = - _r;
		}
		if (Input.GetKey(KeyCode.D)) {
			_keyBoardValue.x = _r;
		}*/
        if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            _keyBoardValue.x = Input.GetAxis("Horizontal") * _r;
            _keyBoardValue.y = Input.GetAxis("Vertical") * _r;
        }
        this.transform.localPosition = _keyBoardValue;
	}
    private void OnDisable()
    {
        _drag = false;
    }
}
