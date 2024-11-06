using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinningMenu : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI enemyDefeated;
    [SerializeField] TextMeshProUGUI bossDefeated;
    [SerializeField] TextMeshProUGUI gotHit;
    [SerializeField] TextMeshProUGUI timeRemaining;
    [SerializeField] TextMeshProUGUI totalPoint;


    public GameObject winningMenu;

    private float delayCounting = 0f;
    public float delayTime = 0.5f;

    private CDTimer timer;
    // Start is called before the first frame update
    void Awake()
    {
        delayCounting = 0f;
        winningMenu.SetActive(false);
        timer = GetComponent<CDTimer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (StageProgress.winningCondition && delayCounting<=delayTime)
        {
            delayCounting += Time.deltaTime;
        }
        if (delayCounting >= delayTime)
        {
            NahIllWin();
        }
    }

    public void NahIllWin()
    {
        Time.timeScale = 0f;
        winningMenu.SetActive(true);

        float multiply = 1f;
        if (StageProgress.gotHit >=8) multiply = 0.8f;
        if (StageProgress.gotHit <=2) multiply = 1.2f;

        int total = Mathf.FloorToInt( (StageProgress.enemyDeathCount*10*multiply + StageProgress.bossDeathCount*1000*multiply + timer.getTime()) / 1 );


        enemyDefeated.text = "Enemy defeated: " + StageProgress.enemyDeathCount + " (x10)";
        bossDefeated.text = "Boss defeated: " + StageProgress.bossDeathCount + " (x1000)";
        gotHit.text = "Got hit: " + StageProgress.gotHit + " times (x" + multiply + ")";
        timeRemaining.text = "Time Remaining: "+ timer.getTime() + " sec";
        totalPoint.text = "Total Point: " + total;
    }

    public void GoToTheMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
        
    }

    public void PlayAgain()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        
    }
    public void ProceedToTheNextStage()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

}

