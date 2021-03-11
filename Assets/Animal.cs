using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal : HPObject
{
    public Transform target;
    public float attackRadius = 1f;
    public float attackCooldown = 1f;
    float currentAttackCooldown = 0;
    public float walkSpeed = 1f;
    public int atk = 1;
    public bool isFacingRight = true;
    // Start is called before the first frame update
    protected override void Start()
    {

        base.Start();
    }

    protected bool isCloseToTarget()
    {
        return (target.position - transform.position).magnitude <= attackRadius;
    }

    protected void attack()
    {
        target.GetComponent<HPObject>().beAttacked(atk);
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
    protected override void Update()
    {
        if (isAlive)
        {
            if (target)
            {
                currentAttackCooldown += Time.deltaTime;
                if (isCloseToTarget())
                {
                    if (currentAttackCooldown >= attackCooldown)
                    {

                        attack();
                        currentAttackCooldown = 0;
                    }
                }
                else
                {

                    Vector3 direction = ((Vector2)(target.position - transform.position)).normalized;
                    if(direction.x<0&& isFacingRight)
                    {
                        flip();
                    }else if(direction.x>0 && !isFacingRight)
                    {
                        flip();
                    }
                    transform.position += direction * walkSpeed * Time.deltaTime;

                }
            }
        }
    }
    void flip() {
        isFacingRight = !isFacingRight;
        var scale = transform.localScale;
        scale.x = -scale.x;
        transform.localScale = scale;
    }
}
