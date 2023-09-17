using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class ChangeTOGameScene : MonoBehaviour
{
    [SerializeField] GameObject panelPrincipal;
    [SerializeField] GameObject panelOptions;
    [SerializeField] GameObject panelLetter;
    [SerializeField] AudioMixer master;

    public void ChangeToGame()
    {
        SceneManager.LoadScene(1);
    }

    public void PlayButton()
    {
        panelPrincipal.SetActive(false);
        panelLetter.SetActive(true);
    }

    public void OptionsPanel()
    {
        panelPrincipal.SetActive(false);
        panelOptions.SetActive(true);
    }

    public void ButtonBack()
    {
        panelPrincipal.SetActive(true);
        panelOptions.SetActive(false);
    }

    public void MasterVolume(float volume)
    {
        master.SetFloat("masterVolume", volume);
        Debug.Log("master");
    }

    public void AppQuit()
    {
        Application.Quit();
    }
}
