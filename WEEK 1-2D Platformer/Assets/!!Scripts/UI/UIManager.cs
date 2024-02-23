using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [Header("GameOver")]
    [SerializeField] private GameObject _gameOverPanel;
    [SerializeField] private AudioClip _gameOverSFX;

    [Header("Pause")]
    [SerializeField] private GameObject _pausePanel;

    public void GameOver()
    {
        _gameOverPanel.SetActive(true);
        AudioManager.Instance.PlaySound(_gameOverSFX);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (_pausePanel.activeInHierarchy)
                PauseGame(false);
            else
                PauseGame(true);
        }
    }

    public void PauseGame(bool status)
    {
        _pausePanel.SetActive(status);

        if (status)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;
    }

    public void ChangeSoundVol()
    {
        AudioManager.Instance.ChangeSoundVolume(0.2f);
    }
    public void ChangeMusicVol()
    {
        AudioManager.Instance.ChangeMusicVolume(0.2f);
    }
    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    public void Quit()
    {
        Application.Quit();
    }

    
}
