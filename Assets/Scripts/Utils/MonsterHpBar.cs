using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterHpBar : MonoBehaviour {

    public GameObject m_HpGauge;
    public GameObject m_HpGaugeBack;

    private Character _character;
    void LateUpdate()
    {
        if (Camera.main != null)
        {
            gameObject.transform.rotation = Camera.main.transform.rotation;
        }
    }

    public void Init(Character character)
    {
        _character = character;

        
        InitData();
    }
    public void InitData()
    {
        this.m_HpGauge.transform.localScale = Vector3.one;
        this.m_HpGauge.transform.localPosition = Vector3.zero;
        this.m_HpGauge.SetActive(true);
        this.m_HpGaugeBack.SetActive(true);
        this.m_HpGauge.GetComponentInChildren<MeshRenderer>().material.renderQueue = 20000;
        this.m_HpGaugeBack.GetComponentInChildren<MeshRenderer>().material.renderQueue = 19999;
    }
    public void Dead()
    {
        this.m_HpGauge.SetActive(false);
        this.m_HpGaugeBack.SetActive(false);
    }
    /// <summary>
    /// 刷新血条计量
    /// </summary>
    public void RefreshHpGauge(float num)
    {
        Vector3 localScale = this.m_HpGauge.transform.localScale;
        localScale.x = num;
        this.m_HpGauge.transform.localScale = localScale;
        Vector3 localPosition = this.m_HpGauge.transform.localPosition;
        localPosition.x = -(0.31f * (1 - num));
        this.m_HpGauge.transform.localPosition = localPosition;

    }
}
