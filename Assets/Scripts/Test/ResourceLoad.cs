using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ResourceLoad : MonoBehaviour {
    public static ResourceLoad Instance;
    private static GameObject cameraBase;
    private static GameObject cameraManager;
    private static GameObject uiCanvas;
    public static Character player;
    private void Awake()
    {
        Instance = this;
    }
    void Start()
	{
        LoadResource();
    }
    public void LoadResource()
    {
        if(cameraManager == null)
        {
            cameraManager = Instantiate(Resources.Load("LoadResource/NormlResource/CameraManager") as GameObject);
            DontDestroyOnLoad(cameraManager);
        }
        if (cameraBase == null)
        {
            cameraBase = Instantiate(Resources.Load("LoadResource/NormlResource/CameraBase") as GameObject);
            DontDestroyOnLoad(cameraBase);
        }
        if (uiCanvas == null)
        {
            uiCanvas = Instantiate(Resources.Load("LoadResource/NormlResource/UICanvas") as GameObject);
            DontDestroyOnLoad(uiCanvas);
        }
        CameraBase cB = cameraBase.GetComponent<CameraBase>();
        CameraManager.Instance.SetCameraBase(cameraBase.GetComponent<CameraBase>());

        Camera uiCam = uiCanvas.transform.Find("UICamera2D").GetComponent<Camera>();
        CommonHelper.SetScreenAdjust(uiCam, cB.Camera);
        if (player == null)
        {
            player = CharacterManager.Instance.CreateCharacter(HeroSelectController.HeroId, Vector3.zero);
            DontDestroyOnLoad(player);
        }
    }
	
    //释放资源
    public void DestroyResources()
    {
        Destroy(cameraManager);
        Destroy(cameraBase);
        Destroy(uiCanvas);
        Destroy(player.gameObject);
    }
}
