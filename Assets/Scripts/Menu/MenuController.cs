using UnityEngine;

public class MenuController : MonoBehaviour
{
    private void Update()
    {
        Camera.main.transform.Translate(new Vector3(1 * Time.deltaTime, 0, 0));
    }

    public void StartGame()
    {
        GameController.Instance.RetryLevels();
    }

    public void OpenOnGithub()
    {
        Application.OpenURL("https://github.com/DoginUwU/portfolio-game");
    }
}
