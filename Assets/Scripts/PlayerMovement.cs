using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerMovement : MonoBehaviour
{

    #region Components for movement
    public Vector3 startPos;
    private Vector2 fingerDownPos;
    private Vector2 fingerUpPos;

    private bool detectSwipeAfterRelease;
    private bool detectSwipeAfterJump;
    public float SWIPE_THRESHOLD = 2f;
    public bool isJumping;
    public int jumpPower = 25;
    public int tempLocalPos = 0;
    #endregion
    #region Components for Speed
    public float moveSpeed = 4f;
    float tempSpeed;
    #endregion

    public Camera cam;
    public int camPosZ = 0;
    public bool stillAlive;
    Rigidbody rb;
    private List<Touch> touchList = new List<Touch>();

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        startPos = transform.position;
        camPosZ = Convert.ToInt32(cam.transform.position.z) - Convert.ToInt32(transform.position.z);
        // stillAlive = true;
    }

    public void StartGame()
    {
        transform.position = startPos;
        moveSpeed = 4f;
        tempLocalPos = 0;
        jumpPower = 25;
    }

    void Update()
    {


        if (stillAlive)
        {
            touchList.Clear();
            transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed, Space.World);
            cam.transform.position = new Vector3(cam.transform.position.x, cam.transform.position.y, transform.position.z + camPosZ);

            foreach (Touch touch in Input.touches)
            {

                touchList.Add(touch);
                if (touch.phase == TouchPhase.Began)
                {
                    fingerUpPos = touch.position;
                    fingerDownPos = touch.position;
                }

                //Detects Swipe while finger is still moving on screen
                if (touch.phase == TouchPhase.Moved)
                {
                    if (!detectSwipeAfterRelease)
                    {
                        fingerDownPos = touch.position;
                        DetectSwipe();
                    }
                }

                //Detects swipe after finger is released from screen
                if (touch.phase == TouchPhase.Ended)
                {
                    fingerDownPos = touch.position;
                    DetectSwipe();
                }

            }
        }

    }

    void DetectSwipe()
    {
        detectSwipeAfterRelease = false;
        if (VerticalMoveValue() > SWIPE_THRESHOLD && VerticalMoveValue() > HorizontalMoveValue())
        {
            if (fingerDownPos.y - fingerUpPos.y > 0)
            {
                if (!detectSwipeAfterJump)
                {
                    OnSwipeUp();
                }
            }
            //else if (fingerDownPos.y - fingerUpPos.y < 0)
            //{
            //    OnSwipeDown();
            //}
            fingerUpPos = fingerDownPos;

        }
        else if (HorizontalMoveValue() > SWIPE_THRESHOLD && HorizontalMoveValue() > VerticalMoveValue())
        {
            if (fingerDownPos.x - fingerUpPos.x > 0)
            {
                OnSwipeRight();
            }
            else if (fingerDownPos.x - fingerUpPos.x < 0)
            {
                OnSwipeLeft();
            }
            fingerUpPos = fingerDownPos;

        }
        detectSwipeAfterRelease = true;
    }

    float VerticalMoveValue()
    {
        return Mathf.Abs(fingerDownPos.y - fingerUpPos.y);
    }

    float HorizontalMoveValue()
    {
        return Mathf.Abs(fingerDownPos.x - fingerUpPos.x);
    }

    void OnSwipeUp()
    {
        detectSwipeAfterJump = true;
        StartCoroutine(Jump());
    }

    IEnumerator Jump()
    {
        transform.DOMoveY(4f, 0.4f);
        transform.DORotate(new Vector3(-35,0,0), 0.4f);
        tempSpeed = moveSpeed;
        moveSpeed += 2 + (moveSpeed / 100f);
        yield return new WaitForSeconds(0.45f);
        StartCoroutine(Fall());
    }

    IEnumerator Fall()
    {
        transform.DOMoveY(startPos.y, 0.55f);
        transform.DORotate(new Vector3(35, 0, 0), 0.4f);
        yield return new WaitForSeconds(0.4f);
        transform.DORotate(new Vector3(0, 0, 0), 0.1f);
        moveSpeed -= 2 + (tempSpeed / 100f);
        detectSwipeAfterJump = false;
    }

    //void OnSwipeDown()
    //{       
    //}

    void OnSwipeLeft()
    {
        if (transform.position.x - 2 >= -3) //to left
        {
            tempLocalPos -= 2;
            transform.DOMoveX((Mathf.Clamp(tempLocalPos, -2, 2)), (0.4f + (moveSpeed / 100f)));
        }
    }

    void OnSwipeRight()
    {
        if (transform.position.x + 2 <= +3) //to right
        {
            tempLocalPos += 2;
            transform.DOMoveX((Mathf.Clamp(tempLocalPos, -2, 2)), (0.4f + (moveSpeed / 100f)));
        }
    }

}
