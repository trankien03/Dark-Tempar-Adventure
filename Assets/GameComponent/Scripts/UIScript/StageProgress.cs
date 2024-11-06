using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class StageProgress : MonoBehaviour
{
    public static int enemyDeathCount = 0;
    public static int bossDeathCount = 0;
    public static int gotHit = 0;
    public static bool winningCondition = false;
    [SerializeField]
    public int enemyDeafeatingCondition = 20;
    [SerializeField]
    public int bossDeafeatingCondition = 0;

    [SerializeField] TMP_Text gameConditionText;

    Animator animator;
    void Awake()
    {
        enemyDeathCount = 0;
        bossDeathCount = 0;
        gotHit = 0;
        winningCondition = false;
        Time.timeScale = 1.0f;
        if (bossDeafeatingCondition >0 )
        {
            Object boss = GameObject.FindGameObjectWithTag("Boss");
            animator = boss.GetComponent<Animator>();   
        }
   
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (bossDeafeatingCondition == 0)
        {
            gameConditionText.text = "Enemies deafeated Count: " + enemyDeathCount + "/" + enemyDeafeatingCondition;
            if (enemyDeathCount >= enemyDeafeatingCondition) winningCondition = true;
        }
        else
        {
            if (!animator.GetBool("isAlive")) bossDeathCount = 1;
            gameConditionText.text = "Boss deafeated Count: " + bossDeathCount + "/" + bossDeafeatingCondition;
            if (bossDeathCount >= bossDeafeatingCondition) winningCondition = true;
        }
        
    }
}
