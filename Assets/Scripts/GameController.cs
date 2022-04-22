using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{

    public string[] levels;

    private int currentLevel;

    private static GameController instance;
    public static GameController Instance
    {
        get { return instance; }
    }

    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(this);
    }
    
    public void RetryLevels()
    {
        currentLevel = 0;
        LoadLevel();
    }

    public void LoadNextLevel()
    {
        if (currentLevel >= (levels.Length - 1))
        {
            SceneManager.LoadScene("Menu", LoadSceneMode.Single);

            return;
        }

        currentLevel++;
        LoadLevel();
    }

    private void LoadLevel()
    {
        SceneManager.LoadScene(levels[currentLevel], LoadSceneMode.Single);
    }
}
