using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroShowAnimator : MonoBehaviour {
 
    private Animator ani;
    void Awake()
    {
        ani = this.GetComponentInChildren<Animator>();
    }
	public void PlayShower()
    {
        ani.Play("dance");
    }
}
