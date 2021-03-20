using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bee : Animal
{
    public float duration;    //the max time of a walking session (set to ten)
    public float waitMinTime;
    public float waitMaxTime;
    float elapsedTime = 0f; //time since started walk
    float wait = 1f; //wait this much time
    float waitTime = 0f; //waited this much time

    float randomX;  //randomly go this X direction
    float randomZ;  //randomly go this Z direction

    bool move = true; //start moving

    public float moveDisMin = 1f;
    public float moveDisMax = 2f;

    public float focusTime = 1f;
    float currentFocusTime = 0f;

    public float beeLife = 10;
    float currentBeeLife;
    protected override void Start()
    {
        currentBeeLife = 0;
        base.Start();
    }

    protected override void attack()
    {
        if (target.GetComponent<TreeFlower>())
        {
            target.GetComponent<TreeFlower>().GetPollinate();
            die();
        }
        else
        {
        }
    }

    public void setTarget(Transform t)
    {
        currentFocusTime = 0;
        target = t;
        hasTargetP = false;
        if (!audiosource.isPlaying)
        {

            audiosource.PlayOneShot(attackSound);
        }
    }

    public  override void die()
    {
        BeeManager.Instance.currentBeeCount -= 1;
        base.die();
    }

    protected override void Update()
    {
        if(currentBeeLife > beeLife && !GetComponent<SpriteRenderer>().isVisible)
        {
            die();
        }
            //if waiting, wait
            currentBeeLife += Time.deltaTime;
        if (currentBeeLife > beeLife && !hasTarget())
        {
            targetP = BeeManager.Instance.getClosestSpawnTransform(transform.position).position;
            hasTargetP = true;
        }

        if (waitTime < wait)
        {
            //you are waiting
            waitTime += Time.deltaTime;
            return;
        }
        //if no target, randomly set a target
        if (!hasTarget())
        {
            randomX = Random.Range(-1f, 1f);
            randomZ = Random.Range(-1f, 1f);
            var dir = new Vector3(randomX, randomZ, 0);
            dir = dir.normalized;
            var dis = Random.Range(moveDisMin, moveDisMax);
            targetP = transform.position + dir*dis;
            Vector3 viewPos = Camera.main.WorldToViewportPoint(targetP);
            if (viewPos.x < 0 || viewPos.x > 1 || viewPos.y < 0 || viewPos.y > 1)
            {
                var pos = -transform.position;
                dir = pos.normalized;
                targetP = transform.position + dir * dis;
            }  

            hasTargetP = true;
        }
        if(target)
        {
            currentFocusTime += Time.deltaTime;
        }
        if (isCloseToTarget())
        {
            targetP = Vector3.positiveInfinity;
            hasTargetP = false;
            waitTime = 0;
        }
        if(target && currentFocusTime>= focusTime)
        {
            target = null;
            waitTime = 0;
        }
        base.Update();

    }
}
