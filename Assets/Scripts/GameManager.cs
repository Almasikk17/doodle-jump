using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public TMP_Text CountDownText;
    public Doodler Doodler;
    public GameObject ReloadPanel;
    public Button ReloadButton;
    public Button MenuButton;
    //public Button QuitButton;
    //public Button StartButton;
    
    private void Awake()
    {
        ReloadButton.onClick.AddListener(ReloadGame);
        MenuButton.onClick.AddListener(LoadMenu);
        //QuitButton.onClick.AddListener(()=> Application.Quit());
        //StartButton.onClick.AddListener(ReloadGame);
    }

    private void OnEnable()
    {
        Doodler.OnDoodlerDestroyed += EndGameHandler;
    }

    private void OnDisable()
    {
        Doodler.OnDoodlerDestroyed -= EndGameHandler;
    }

    private void EndGameHandler()
    {
        StartCoroutine(CountDown());
    }

    private void ReloadGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void LoadMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private IEnumerator CountDown()
    {
        float timer = 5f;
        while(timer > 0)
        {
            CountDownText.text = $"Restart in: {timer:F0}";
            ReloadPanel.SetActive(true);
            CountDownText.fontSize = 100;
            timer -= Time.deltaTime;
            yield return null; 
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    } 
}
