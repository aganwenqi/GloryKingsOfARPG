using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnEnterDupSwitch : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        Character target = other.GetComponent<Character>();
        
        if(target != null && target.CharacterUtilData.characterType == CharacterType.Player)
        {
            UICanvasManager.Instance.OnEnterUI(DupSwitchUI.Instance);
        }
    }
}
