using UnityEngine;
using System.Collections;
using System.Collections.Generic;
/// <summary>
/// 通用辅助类
/// </summary>
public class CommonHelper : MonoBehaviour {

    /// <summary>
    /// 屏幕适配调整
    /// </summary>
    public enum ScreenAdjustT
    {
        Clip = 0,
        Max = 2,
        Min = 0,
        None = -1,
        Scale = 1
    }
    public static ScreenAdjustT m_ScreenAdjustT = ScreenAdjustT.Min;
    public static bool m_bNeedScreenAdjust = true;
    public static int m_UIDefaultW = 1024;
    public static int m_UIDefaultH = 768;
    /// <summary>
    /// 设置调整屏幕适配
    /// </summary>
    /// <param name="uiCam"></param>
    /// <param name="worldCam"></param>
    public static void SetScreenAdjust(Camera uiCam, Camera worldCam)
    {

        if (m_bNeedScreenAdjust)
        {
            if (m_ScreenAdjustT == ScreenAdjustT.Min)
            {
                
                float default_aspect = (float)m_UIDefaultW / (float)m_UIDefaultH;
  
                float height = (Screen.width / default_aspect) / Screen.height;
                float top = (1 - height) / 2f;
                if (uiCam != null)
                {
                    uiCam.rect = new Rect(0f, top, 1f, height);
                }
                if (worldCam != null)
                {
                    worldCam.rect = new Rect(0f, top, 1f, height);
                }

            }
            else if (m_ScreenAdjustT == ScreenAdjustT.Scale && uiCam != null)
            {
                uiCam.aspect = (float)m_UIDefaultW / (float)m_UIDefaultH;
            }
        }

    }
}
