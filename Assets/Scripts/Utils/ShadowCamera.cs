using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowCamera : MonoBehaviour {

    public Camera shadowCamera;
    public MeshRenderer render;
    public int width = 256;
    public int height = 256;
    private RenderTexture renderTexture;
    private void OnEnable()
    {
        if (renderTexture == null)
        {
            renderTexture = RenderTexture.GetTemporary(width, height, 0);
        }
        shadowCamera.targetTexture = renderTexture;
        render.material.SetTexture("_MainTex", renderTexture);
    }
    private void OnDestroy()
    {
        RenderTexture.ReleaseTemporary(renderTexture);
    }
}
