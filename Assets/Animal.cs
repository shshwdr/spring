using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal : MonoBehaviour
{
    protected Transform target;
    public float attackRadius = 1f;
    public bool isAlive;
    public float attackCooldown = 1f;
    float currentAttackCooldown = 0;
    public float walkSpeed = 1f;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        
    }

    protected bool isCloseToTarget()
    {
        return (target.position - transform.position).magnitude <= attackRadius;
    }

    protected void attack()
    {
        Debug.Log("attack");
    }

    public Transform getClosestTransform(List<Transform> list)
    {
        Transform res = null;
        float minDis = float.PositiveInfinity;
        foreach(var mb in list)
        {
            var tran = mb.transform;
            float dis = (tran.position - transform.position).magnitude;
            if (dis < minDis)
            {
                minDis = dis;
                res = tran;
            }
        }
        return res;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (isAlive)
        {
            if (target)
            {
                if (isCloseToTarget())
                {
                    attack();
                }
                else
                {
                    Vector3 direction = ((Vector2)(target.position - transform.position)).normalized;
                    transform.position += direction * walkSpeed * Time.deltaTime;

                }
            }
        }
    }
}
