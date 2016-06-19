using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour {
    private int sceneNumber;

    void Start ()
    {
        sceneNumber = SceneManager.GetActiveScene().buildIndex;
    }

    public void LoadNextLevel ()
    {
        SceneManager.LoadScene(sceneNumber + 1);
    }

    public void LoadLastLevel()
    {
        SceneManager.LoadScene(sceneNumber - 1);
    }

    public void QuitGame ()
    {
        Application.Quit();
    }
}
