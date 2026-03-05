using UnityEngine;

public class ShowTutorial : MonoBehaviour
{
    [SerializeField] GameObject tutorialPanel;
    [SerializeField] GameObject page1;
    [SerializeField] GameObject page2;
    [SerializeField] GameObject page3;
     
    private void Start()
    {
        FindFirstObjectByType<AudioManager>().Play("MainMenuSong");
    }

    public void OpenTutorial()
    {
        FindFirstObjectByType<AudioManager>().PlayOnce("ButtonClick");
        tutorialPanel.SetActive(true);
        page1.SetActive(true);
        page2.SetActive(false);
        page3.SetActive(false);
    }

    public void CloseTutorial()
    {
        FindFirstObjectByType<AudioManager>().PlayOnce("ButtonClick");
        page3.SetActive(false);
        tutorialPanel.SetActive(false);
    }

    public void NextPage(int pageNo)
    {
        FindFirstObjectByType<AudioManager>().PlayOnce("ButtonClick");
        switch (pageNo)
        {
            case 1:
                page2.SetActive(true);
                page1.SetActive(false);
                page3.SetActive(false);
                break;
            case 2:
                page2.SetActive(false); 
                page1.SetActive(false);
                page3.SetActive(true);
                break;
        }
    }
}
