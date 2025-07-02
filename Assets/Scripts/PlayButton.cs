using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButton : MonoBehaviour
{
    void LoadGame()
    {
        Debug.Log("PlayButtonPressed");
        SceneManager.LoadScene("SampleScene");
    }
}
