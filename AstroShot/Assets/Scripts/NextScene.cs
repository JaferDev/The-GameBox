using UnityEngine;
using UnityEngine.SceneManagement;

public class NextScene : MonoBehaviour
{
    public void GoNextScene()
    {
        FindFirstObjectByType<AudioManager>().PlayOnce("ButtonClick");
        SceneManager.LoadScene(1);
    }
}
