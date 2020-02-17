using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SWInfoPanel : MonoBehaviour {

    BaseUI _manager;
    public void Init(BaseUI manager)
    {
        _manager = manager;

    }

    /*有按钮按下*/
    public void BtInvoke(int type)
    {
        InfoBtType btType = (InfoBtType)type;
        if (btType == InfoBtType.quit || btType == InfoBtType.quitDup)/*退出UI*/
        {
            List<BaseUI> showUI = new List<BaseUI>();
            showUI.Add(BattleUI.Instance);
            showUI.Add(MenuPanelUI.Instance);
            UICanvasManager.Instance.OnEnterUI(showUI);
            if (btType == InfoBtType.quitDup)
            {
                SceneController.ChangeScene(1000);
            }

            MusicUtil.PlayClip(UICanvasManager.Instance.SourceSid, SourceClip.quitBt);
        }
        else if(btType == InfoBtType.quitGame)
        {
            Application.Quit();
            MusicUtil.PlayClip(UICanvasManager.Instance.SourceSid, SourceClip.quitBt);
        }
        else if (btType == InfoBtType.selectHero)
        {
            ResourceLoad.Instance.DestroyResources();
            LoadingScene.LoadNewScene("HeroSelect");  
        }
        else/*切换属性面板*/
        {
            _manager.ChangePanel(btType);
            MusicUtil.PlayClip(UICanvasManager.Instance.SourceSid, SourceClip.normlBt);
        }
    }
}
