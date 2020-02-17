using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SceneController : MonoBehaviour {

    /*切换场景*/
	public static void ChangeScene(int dupId)
    {
        DupManager.Instance.SetDupId(dupId);
        LoadingScene.dupId = dupId;
        LoadingScene.LoadNewScene("InitScene");
    }
}
