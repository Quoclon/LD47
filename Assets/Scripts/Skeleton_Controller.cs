using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton_Controller : MonoBehaviour
{
    Animator anim;
    Rigidbody2D rb;

    [SerializeField] private float moveTimer;
    private float moveTimerMin = 1.0f;
    private float moveTimerMax = 6.0f;
    private float moveTimerCurrent;

    [SerializeField] private float idleTimer;
    private float idleTimerMin = 0.25f;
    private float idleTimerMax = 3.0f;
    private float idleTimerCurrent;

    private float moveSpeed = 0.5f;
    private float randMoveX;
    private float randMoveY;




    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();

        //Set the move timers
        moveTimer = UnityEngine.Random.Range(moveTimerMin, moveTimerMax+1);
        moveTimerCurrent = moveTimer;
        idleTimer = UnityEngine.Random.Range(idleTimerMin, idleTimerMax + 1);
        idleTimerCurrent = idleTimer;

        SetDirection(-1, 1);
    }

    private void SetIdleDirection()
    {
        randMoveX = 0;
        randMoveY = 0;
    }

    private void SetDirection(int min, int max)
    {
        int intervalForRandom = 1;

        randMoveX = Mathf.Floor(UnityEngine.Random.Range(min, max + 1) / intervalForRandom);
        randMoveY = Mathf.Floor(UnityEngine.Random.Range(min, max + 1) / intervalForRandom);

        if (randMoveX == 0 && randMoveY == 0)
        {
            SetDirection(-1, 1);
        }
    }


    private void HandleAnimation()
    {
        //Idle (NOT USED)
        if(idleTimerCurrent >= 0 && moveTimerCurrent <= 0)
        {
            anim.SetBool("walking", false);
        }

        //Moving
        if (moveTimerCurrent >= 0)
        {
            anim.SetBool("walking", true);
        }

        //Attacking
        if (idleTimerCurrent >= 0 && moveTimerCurrent <= 0)
        {
            //anim.SetBool("idle", true);
        }

    }


    private void HandleMovement()
    {

        //Set to idle for duration of idleTimerCurrent
        if (moveTimerCurrent < 0 && idleTimerCurrent > 0)
        {
            //lastXDir = randX;
            SetIdleDirection();
            idleTimerCurrent -= Time.deltaTime;
        }


        //Set to Move once idleTimerCurrent less than zero
        if (moveTimerCurrent < 0 && idleTimerCurrent < 0)
        {
            SetDirection(-1, 1);
            Debug.Log("RandX: " + randMoveX + "RandY: " + randMoveY);
            idleTimerCurrent = idleTimer;
            moveTimerCurrent = moveTimer;
        }

        //Move the NPC
        transform.position += new Vector3(randMoveX, randMoveY, 0) * moveSpeed * Time.deltaTime;
        moveTimerCurrent -= Time.deltaTime;
    }


    private void HandleOrientation()
    {
        if (randMoveX != 0)
        {
            if (randMoveX > 0)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
            else
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
        }
    }


    // Update is called once per frame
    void Update()
    {
        HandleMovement();
        HandleOrientation();
        //HandleAttack();
        HandleAnimation();
    }

    
}
