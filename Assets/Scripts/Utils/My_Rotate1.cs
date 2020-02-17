using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class My_Rotate1 : MonoBehaviour {

    public float xSpeed;
    public float ySpeed;

    private void Update()
    {
#if UNITY_EDITOR
        if (Input.GetMouseButton(0))
        {
            transform.Rotate(Vector3.up * Input.GetAxis("Mouse X") * -xSpeed * Time.deltaTime, Space.Self);
            transform.Rotate(Vector3.forward * Input.GetAxis("Mouse Y") * -ySpeed * Time.deltaTime, Space.Self);
        }
#elif (UNITY_IOS || UNITY_ANDROID)
        if (Input.GetMouseButton(0))
        {
            if (Input.touchCount == 1)
            {
                if (Input.GetTouch(0).phase == TouchPhase.Moved)
                {
                    transform.Rotate(Vector3.up * Input.GetAxis("Mouse X") * -xSpeed * Time.deltaTime,Space.Self);
                    transform.Rotate(Vector3.forward * Input.GetAxis("Mouse Y") * -ySpeed * Time.deltaTime, Space.Self);
                }
            }
        }
#endif

    }
}
