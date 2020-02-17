using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSetting : MonoBehaviour {

    public Camera cameraBase;
    public GameObject uiCanvas;
    void Start () {
        uiCanvas.SetActive(false);
        uiCanvas.SetActive(true);
    }
}
