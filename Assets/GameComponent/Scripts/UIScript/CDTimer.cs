using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class CDTimer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] float remainingTime;

    Animator animator;

    public bool _isOutOfTime;
    public bool IsOutOfTime
    {
        get
        {
            return _isOutOfTime;
        }
        set
        {
            _isOutOfTime = value;    
            animator.SetBool(AnimationStrings.isOutOfTime, value);
        }
    }

    public int getTime()
    {
        return Mathf.FloorToInt(remainingTime /1);
    }
    private void Awake()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        animator = player.GetComponent<Animator>();
    }

    void Update()
    {
        if (remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;
            IsOutOfTime = false;
        }
        else
        {
            remainingTime = 0;
            IsOutOfTime = true;
        }
        
        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);


        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
