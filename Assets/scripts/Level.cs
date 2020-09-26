using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Level : MonoBehaviour
{
    float timeToWaitGameOver = 1f;

    public void LoadNextScene()
    {
        int activeScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(activeScene + 1);
    }

    public void ResetGame()
    {
        SceneManager.LoadScene(0);
        FindObjectOfType<GameSession>().ResetGame();
    }

    public void LoadGameOverScene()
    {
        StartCoroutine(WaitAndLoadGameOver());
    }

    IEnumerator WaitAndLoadGameOver()
    {
        yield return new WaitForSeconds(timeToWaitGameOver);
        SceneManager.LoadScene("Game Over");

    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
