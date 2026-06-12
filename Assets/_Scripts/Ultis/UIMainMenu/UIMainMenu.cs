using UnityEngine;

public class UIMainMenu : MonoBehaviour
{
    [SerializeField] private GameObject StartButton;
    [SerializeField] private GameObject QuitButton;
    [SerializeField] private GameObject BackToStartScreenButton;
    [SerializeField] private GameObject ClassChoices;

    public void StartButtonPressed()
    {
        StartButton.SetActive(false);
        QuitButton.SetActive(false);
        ClassChoices.SetActive(true);
        BackToStartScreenButton.SetActive(true);
    }

    public void BackToStartScreenButtonPressed()
    {
        StartButton.SetActive(true);
        QuitButton.SetActive(true);
        BackToStartScreenButton.SetActive(false);
        ClassChoices.SetActive(false);
    }
    public void Play(int choice)
    {
        GameManager.instance.Play(choice);
    }
    public void Quit()
    {
        Application.Quit();
    }
}
