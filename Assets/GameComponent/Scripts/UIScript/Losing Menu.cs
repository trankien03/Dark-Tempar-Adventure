using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LosingMenu : MonoBehaviour
{
    public GameObject losingMenu;
    Animator animator;
    private float delayCounting = 0f;
    public float delayTime = 1f;
    void Awake()
    {
        losingMenu.SetActive(false);
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        animator = player.GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!animator.GetBool(AnimationStrings.isAlive) && delayCounting <= delayTime)
        {
            delayCounting += Time.deltaTime;
        }
        if (delayCounting >= delayTime)
        {
            NahIllLose();
        }
    }

    public void NahIllLose()
    {
        Time.timeScale = 0f;
        losingMenu.SetActive(true);
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
}
