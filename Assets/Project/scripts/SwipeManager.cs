using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Swipe { None, Up, Down, Left, Right };
public delegate void VoidDelegate();
public delegate void MoveDelegate(float f);


public class SwipeManager : MonoBehaviour
{
    private event VoidDelegate touchStart;
    private event VoidDelegate touchEnded;

    private event MoveDelegate touchMoved;
    private event MoveDelegate touchMovedEnded;

    public float minSwipeLength = 200f;

    Vector2 firstPressPos;
    Vector2 secondPressPos;
    Vector2 currentSwipe;

    /// <summary>
    /// If true, swipes will be handled.
    /// </summary>
    public bool handleSwipes = true;

    /// <summary>
    /// Flicks are classed as swipes but with a force greater than SwipeHandler#requiredForceForFlick.
    /// </summary>
    public bool handleFlicks = true;

    public float requiredForceForFlick = 7f;

    public enum FlickType
    {
        Inertia,
        MoveOne
    }
    
    public FlickType flickType = FlickType.Inertia;
        
    /// <summary>
    /// Limits the maximum force applied when swiping.
    /// </summary>
    public float maxForce = 15f;

    private Vector3 finalPosition, startpos, endpos, oldpos;
    private float length, startTime, mouseMove, force;
    private bool SW;

    public static Swipe swipeDirection;

    void Update()
    {
        HandleMobileSwipe();
        DetectSwipe();
    }

    public void AddStartTouch(VoidDelegate d)
    {
        touchStart += d;
    }

    public void AddFinishTouch(VoidDelegate d)
    {
        touchEnded += d;
    }

    public void AddMoveTouch(MoveDelegate d)
    {
        touchMoved += d;
    }
    public void AddEndMoveTouch(MoveDelegate d)
    {
        touchMovedEnded += d;
    }

    private void HandleMobileSwipe()
    {
        if (Input.touchCount > 0)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                touchStart();
                startTime = Time.time;
                finalPosition = Vector3.zero;
                length = 0;
                SW = false;
                Vector2 touchDeltaPosition = Input.GetTouch(0).position;
                startpos = new Vector3(touchDeltaPosition.x, 0, touchDeltaPosition.y);
                oldpos = startpos;
            }

            if (Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                SW = true;

                Vector2 touchDeltaPosition = Input.GetTouch(0).position;
                Vector3 pos = new Vector3(touchDeltaPosition.x, 0, touchDeltaPosition.y);

                if (handleSwipes && pos.x != oldpos.x)
                {
                    Vector3 f = pos - oldpos;

                    float l = f.x < 0 ? (f.magnitude * Time.deltaTime) : -(f.magnitude * Time.deltaTime);

                    l *= .2f;

                    touchMoved(l);
                }

                oldpos = pos;
            }

            if (Input.GetTouch(0).phase == TouchPhase.Canceled)
            {
               // touchEnded();
                //SW = false;
            }

            if (Input.GetTouch(0).phase == TouchPhase.Stationary)
            {
               // touchEnded();
                //SW = false;
            }

            if (Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                touchEnded();
                if (handleFlicks)
                {
                    Vector2 touchPosition = Input.GetTouch(0).position;
                    endpos = new Vector3(touchPosition.x, 0, touchPosition.y);
                    finalPosition = endpos - startpos;
                    length = finalPosition.x < 0 ? -(finalPosition.magnitude * Time.deltaTime) : (finalPosition.magnitude * Time.deltaTime);

                    length *= .35f;

                    var force = length / (Time.time - startTime);

                    force = Mathf.Clamp(force, -maxForce, maxForce);

                    if (handleFlicks && Mathf.Abs(force) > requiredForceForFlick)
                    {
                        touchMovedEnded(-force);
                    }
                }
            }

        }
    }

    public void DetectSwipe()
    {
        if (Input.touches.Length > 0)
        {
            Touch t = Input.GetTouch(0);

            if (t.phase == TouchPhase.Began)
            {
                firstPressPos = new Vector2(t.position.x, t.position.y);
            }

            if (t.phase == TouchPhase.Ended)
            {
                secondPressPos = new Vector2(t.position.x, t.position.y);
                currentSwipe = new Vector3(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);

                // Make sure it was a legit swipe, not a tap
                if (currentSwipe.magnitude < minSwipeLength)
                {
                    swipeDirection = Swipe.None;
                    return;
                }

                currentSwipe.Normalize();

                // Swipe up
                if (currentSwipe.y > 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f)
                {
                    swipeDirection = Swipe.Up;
                    // Swipe down
                }
                else if (currentSwipe.y < 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f)
                {
                    swipeDirection = Swipe.Down;
                    // Swipe left
                }
                else if (currentSwipe.x < 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
                {
                    swipeDirection = Swipe.Left;
                    // Swipe right
                }
                else if (currentSwipe.x > 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
                {
                    swipeDirection = Swipe.Right;
                }
            }
        }
        else
        {
            swipeDirection = Swipe.None;
        }
    }
}