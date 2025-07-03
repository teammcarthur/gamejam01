using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButton : MonoBehaviour

{
    public void LoadGame()
    {
        Debug.Log("PlayButtonPressed");
        Debug.Log("ButtonPressed");
        SceneManager.LoadScene("OpeningCutscene");
    }
}
