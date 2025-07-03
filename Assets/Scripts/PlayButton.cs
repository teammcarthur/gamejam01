using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButton : MonoBehaviour

{
    public void LoadGame()
    {
        Debug.Log("PlayButtonPressed");
        SceneManager.LoadScene("Platfroms");
    }
}
