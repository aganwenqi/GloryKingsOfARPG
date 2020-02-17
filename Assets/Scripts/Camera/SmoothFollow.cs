using UnityEngine;
using System.Collections;

public class SmoothFollow : MonoBehaviour
{

    public Transform target;
    public float offsetForward = 0;
    public float minMoveSpeed = 0;
    public float maxMoveSpeed = 10;
    public float minDistance = 1;
    public float maxDistance = 5;

    public enum UpdateType
    {
        FixedUpdate,
        Update,
        LateUpdate,
    }
    public UpdateType updateType = UpdateType.LateUpdate;

    // Use this for initialization
    void Start()
    {
        if (target == null)
        {
            target = DupController.Instance.Player.transform;
          
            
        }
            

    }

    void FixedUpdate()
    {
        if (target && updateType == UpdateType.FixedUpdate)
            _update(Time.fixedDeltaTime);
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
            target = DupController.Instance.Player.transform;

        if (target && updateType == UpdateType.Update)
            _update(Time.deltaTime);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (target && updateType == UpdateType.LateUpdate)
            _update(Time.deltaTime);
    }

    void _update(float deltaTime)
    {
        //		if(target){
        float dist = Vector3.Distance(target.position, transform.position);
        if (dist > minDistance)
        {
            transform.position = Vector3.MoveTowards(
                transform.position, target.position + target.forward * offsetForward,
                deltaTime * Mathf.SmoothStep(minMoveSpeed, maxMoveSpeed, dist / maxDistance));
            //		}
        }
    }
}
