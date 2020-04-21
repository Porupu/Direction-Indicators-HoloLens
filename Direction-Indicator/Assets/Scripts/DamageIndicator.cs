using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageIndicator : MonoBehaviour
{
    private const float MaxTimer = 8; //how long does indicator stay visible
    private float timer = MaxTimer;

    private CanvasGroup canvasGroup = null;
    protected CanvasGroup CanvasGroup //a getter for canvasGroup
    {
        get
        {
            if (canvasGroup == null) //check if it doesnt exist
            {
                canvasGroup = GetComponent<CanvasGroup>();
                if (canvasGroup == null) //still doesnt exist? then it's not attached - just add component of it
                {
                    canvasGroup = gameObject.AddComponent<CanvasGroup>();
                }
            }
            return canvasGroup;
        }
    }

    protected RectTransform rect = null;
    protected RectTransform Rect
    {
        get
        {
            if (rect == null) //check if it doesnt exist
            {
                rect = GetComponent<RectTransform>();
                if (rect == null) //still doesnt exist? then it's not attached - just add component of it
                {
                    rect = gameObject.AddComponent<RectTransform>();
                }
            }
            return rect;
        }
    }

    public Transform Target { get; protected set; } = null; //can be get from other classes, but not set it
    private Transform player = null;

    private IEnumerator IE_Countdown = null; //countodwn of the timer, once counted, destroy itself
    private Action unRegister = null;

    //damage indicator is registered inside the DI_System.cs, in cases where one damage indicator is already pointing to a source,
    //instead of spawning another one we just restart the timer; so the unRegister Action above unregisters them from the list when destroyed

    //we gonna need to cache in data from the target's location and pointer's rotation and such so then even if the target dissapears 
    //and the pointer still has timer remaining, the pointer will know where to point to
    //so if the target is null we can still use the cached data
    private Quaternion targetRotation = Quaternion.identity;
    private Vector3 targetPosition = Vector3.zero;

    public void Register(Transform target, Transform player, Action unRegister)
    {
        this.Target = target;
        this.player = player;
        this.unRegister = unRegister;

        StartCoroutine(RotateToTheTarget());
        StartTimer();
    }

    public void Restart() //restarting the timer if the indicator already exists on the same target
    {
        timer = MaxTimer;
        StartTimer();
    }

    private void StartTimer()
    {
        if (IE_Countdown != null)
        {
            StopCoroutine(IE_Countdown);
        }
        IE_Countdown = Countdown();
        StartCoroutine(IE_Countdown);
    }

    IEnumerator RotateToTheTarget() //rotate the pointer to the target, while enabled
    {
        while(enabled)
        {
            if (Target) //Target is not equal to null
            {
                targetPosition = Target.position; //caching in the data
                targetRotation = Target.rotation;
            }
            Vector3 direction = player.position - targetPosition; //direction from the player to the target

            targetRotation = Quaternion.LookRotation(direction);
            targetRotation.z = -targetRotation.y; //rotate only on the z axis in 2D
            targetRotation.x = 0;
            targetRotation.y = 0;

            Vector3 northDirection = new Vector3(0, 0, player.eulerAngles.y); //gives us direction to the north
            Rect.localRotation = targetRotation * Quaternion.Euler(northDirection);

            yield return null; //yield for a single frame
        }
    }

    private IEnumerator Countdown() //fade in the damage indicator, start the timer, once the timer runs out, fade out the indicator, destroy and unregister this indicator
    {
        while(CanvasGroup.alpha < 1.0f) //fade in the indicator
        {
            CanvasGroup.alpha += 4 * Time.deltaTime;
            yield return null;
        }
        while(timer > 0) //timer countdown each second
        {
            timer--;
            yield return new WaitForSeconds(1);
        }
        while(CanvasGroup.alpha > 0.0f) //fade out the indicator
        {
            CanvasGroup.alpha -= 2 * Time.deltaTime;
            yield return null;
        }
        unRegister(); //the indicator is invisible so unregister and destroy it
        Destroy(gameObject);
    }
}
