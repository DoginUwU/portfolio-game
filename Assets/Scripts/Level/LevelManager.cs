using UnityEngine;

public enum LevelState
{
    FAILED,
    PLAYING,
    PAUSED,
    WONNED
}

public class LevelManager : MonoBehaviour
{
    public LevelState state;
    
    private static LevelManager instance;

    public static LevelManager Instance
    {
        get
        {
            return instance;
        }
    }

    [Header("UI")]
    public GameObject FailedPanel;
    public GameObject WonPanel;

    private void Start()
    {
        instance = this;

        state = LevelState.PLAYING;

        LockCursor(true);
    }

    private void Update()
    {
        FailedPanel.SetActive(state == LevelState.FAILED);
        WonPanel.SetActive(state == LevelState.WONNED);
    }

    private void LockCursor(bool locked)
    {
        Cursor.visible = !locked;
        Cursor.lockState = locked ? CursorLockMode.Locked : CursorLockMode.None;
    }

    public void PassLevel()
    {
        state = LevelState.WONNED;
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBehaviour>().DisablePlayer();
        LockCursor(false);
    }

    public void FailLevel()
    {
        state = LevelState.FAILED;
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBehaviour>().DisablePlayer();
        LockCursor(false);
    }

    public void NextLevel()
    {
        GameController.Instance.LoadNextLevel();
    }

    public void RetryLevel()
    {
        GameController.Instance.RetryLevels();
    }
}
