// GameManager.cs - Reemplaza completamente el anterior
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    
    [Header("Pause UI")]
    public GameObject pauseCanvas;
    
    public bool isPaused = false;
    
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    void Start()
    {
        pauseCanvas.SetActive(false);
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }
    
    public void TogglePause()
    {
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0f : 1f;
        
        pauseCanvas.SetActive(isPaused);
        AudioListener.pause = isPaused;
    }
    
    public void ResumeGame()
    {
        TogglePause();
    }
    
    public void RegisterPausable(MonoBehaviour script)
    {
        // Ya no necesario - Time.timeScale pausa todo automáticamente
    }
}
