using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private bool _pauseMenu;
    [SerializeField] private GameObject _mainPanel;
    [SerializeField] private GameObject _settingsPanel;
    [SerializeField] private GameObject _pausePanel;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(!_pauseMenu) SetMainPanel();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void SetMainPanel()
    {
        if(_pauseMenu)
        {
            if(_pausePanel) _pausePanel.SetActive(true);
            if(_mainPanel) _mainPanel.SetActive(false);
        }
        else
        {
            if(_mainPanel) _mainPanel.SetActive(true);
            if(_pausePanel) _pausePanel.SetActive(false);
        }
        _settingsPanel.SetActive(false);
    }

    public void SetSettingsPanel()
    {
        _settingsPanel.SetActive(true);
        if(_pauseMenu)
            if (_pausePanel) _pausePanel.SetActive(false);
            else 
            if(_mainPanel) _mainPanel.SetActive(false);
        _mainPanel.SetActive(false);
    }
    
    public void RetryGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        
        Time.timeScale = 1f;
    }

    public void Play()
    {
        SceneManager.LoadScene("Forest");
    }

    public void Quit()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.ExitPlaymode();
        #else
            Application.Quit();
        #endif
    }

    public void Pause()
    {
        Time.timeScale = 0.0f;
        SetMainPanel();
    }
    public void Resume()
    {
        Time.timeScale = 1.0f;
        _pausePanel.SetActive(false);
        
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void SwitchPause()
    {
        if (Time.timeScale > 0.0f)
            Pause();
        else
            Resume();
    }
    
}