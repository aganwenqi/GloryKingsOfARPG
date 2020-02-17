using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitScene : MonoBehaviour {

    private void Awake()
    {
        Resources.UnloadUnusedAssets();
        GC.Collect();
        GC.WaitForPendingFinalizers();//挂起当前线程，直到处理终结器队列的线程清空该队列为止。
        GC.Collect();
        LoadingScene.LoadNewScene("LoadScene");
    }

}
