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
    public bool hasTargetP = false;
    public Vector3 targetP = Vector3.zero;
    // Start is called before the first frame update
    protected override void Start()
    {

        base.Start();
    }

    protected bool isCloseToTarget()
    {
        return ((Vector2)(targetPosition() - transform.position)).magnitude <= attackRadius;
    }

    protected virtual void attack()
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

    Vector3 targetPosition()
    {
        return target!=null ? target.position : targetP;
    }

    public bool hasTarget()
    {
        return target!=null || hasTargetP;
    }

    // Update is called once per frame
    protected override void Update()
    {
        if (isAlive)
        {
            if (hasTarget())
            {
                currentAttackCooldown += Time.deltaTime;
                if (isCloseToTarget())
                {
                    if (currentAttackCooldown >= attackCooldown)
                    {

                        attack();
                        if(GetComponent<Animator>())
                        {

                            GetComponent<Animator>().SetTrigger("attack");
                        }
                        currentAttackCooldown = 0;
                    }

                    if (GetComponent<Animator>())
                        GetComponent<Animator>().SetFloat("speed", 0);
                }
                else
                {

                    Vector3 direction = ((Vector2)(targetPosition() - transform.position)).normalized;
                    if(direction.x<0&& isFacingRight)
                    {
                        flip();
                    }else if(direction.x>0 && !isFacingRight)
                    {
                        flip();
                    }
                    transform.position += direction * walkSpeed * Time.deltaTime;
                    if (GetComponent<Animator>())
                        GetComponent<Animator>().SetFloat("speed", 1);
                }
            }
            else
            {

                if (GetComponent<Animator>())
                    GetComponent<Animator>().SetFloat("speed", 0);
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
