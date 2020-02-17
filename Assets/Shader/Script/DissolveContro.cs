using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class DissolveContro : MonoBehaviour {

    public float _time;
    public float _dissolveSpeed;
    public Renderer _render;
    public Renderer _render2;
    public Material norml;
    public Material dead;
    private float _curTime;
    private UnityAction _action;
    private void Awake()
    {
        _render = this.GetComponent<Renderer>();
    }
    void Start () {
        _curTime = 0;
    }
    private void OnEnable()
    {
        InitNorml();
    }
    public void Play(UnityAction action)
    {
        _action = action;
        _curTime = 0;
        _render.material = dead;
        _render.material.SetFloat("_BurnAmount", 0);
        if (_render2 != null)
            _render2.material = dead;
        StartCoroutine(Dissolve());
    }
    public void InitNorml()
    {
        this.StopAllCoroutines();
        _render.material = norml;
        if (_render2 != null)
            _render2.material = norml;

    }
	// Update is called once per frame
	IEnumerator Dissolve () {
       while(_curTime < _time)
        {
            _curTime += Time.deltaTime;
            yield return new WaitForFixedUpdate();
        }

        _curTime = 0;
        while (_render && _curTime <= _dissolveSpeed)
        {
            _curTime += Time.deltaTime;
            _render.material.SetFloat("_BurnAmount", _curTime / _dissolveSpeed);
            yield return new WaitForFixedUpdate();
        }
        _action.Invoke();
    }
}
