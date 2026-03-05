using UnityEngine;

public class Credits : MonoBehaviour
{
    [SerializeField] GameObject credits;

    public void OpenCredits()
    {
        FindFirstObjectByType<AudioManager>().PlayOnce("ButtonClick");
        credits.SetActive(true);
    }

    public void CloseCredits()
    {
        FindFirstObjectByType<AudioManager>().PlayOnce("ButtonClick");
        credits.SetActive(false);
    }
}
