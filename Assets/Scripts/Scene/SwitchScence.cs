using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SwitchScence : MonoBehaviour {

    public void InJoinGame()
    {
        SceneManager.LoadScene("InJoinGame");
    }
    public void HeroSelect()
    {
        SceneManager.LoadScene("HeroSelect");
    }
}
