

using UnityEngine;
using UnityEngine.SceneManagement;


public class Scene : MonoBehaviour
{
  public void PlayGame()
  {
    Scene activeScene = SceneManager.GetActiveScene();
    SceneManager.LoadScene(((Scene) ref activeScene).buildIndex + 1);
  }

  public void FinishGame()
  {
    Scene activeScene = SceneManager.GetActiveScene();
    SceneManager.LoadScene(((Scene) ref activeScene).buildIndex + 1);
  }

  public void QuitGame() => Application.Quit();
}
