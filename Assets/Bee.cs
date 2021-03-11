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

    void Start()
    {
        randomX = Random.Range(-3, 3);
        randomZ = Random.Range(-3, 3);
    }

    protected override void Update()
    {
        //if waiting, wait

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
            hasTargetP = true;
        }
        if (isCloseToTarget())
        {
            targetP = Vector3.positiveInfinity;
            hasTargetP = false;
            waitTime = 0;
        }
        base.Update();

        //if (elapsedTime < duration && move)
        //{
        //    //if its moving and didn't move too much
        //    transform.Translate(new Vector3(randomX, 0, randomZ) * Time.deltaTime);
        //    elapsedTime += Time.deltaTime;

        //}
        //else
        //{
        //    //do not move and start waiting for random time
        //    move = false;
        //    wait = Random.Range(waitMinTime, waitMaxTime);
        //    waitTime = 0f;
        //}

        //else if (!move)
        //{
        //    //done waiting. Move to these random directions
        //    move = true;
        //    elapsedTime = 0f;
        //    randomX = Random.Range(-3, 3);
        //    randomZ = Random.Range(-3, 3);
        //}
    }
}
