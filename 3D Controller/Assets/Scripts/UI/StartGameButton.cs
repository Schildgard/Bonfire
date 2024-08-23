using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGameButton : MonoBehaviour
{
    [SerializeField] GameEvent loadSceneEvent;
    public void LoadGameScene()
    {
        SceneManager.LoadScene(1);
        loadSceneEvent.Raise();
    }
}
