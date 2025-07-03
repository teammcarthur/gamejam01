using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartCutscene : MonoBehaviour
{
    void Start()
    {
       StartCoroutine(WaitForCutscene());
    }

    void Update()
    {   
    }

    IEnumerator WaitForCutscene()
    {
        yield return new WaitForSeconds(48F);
        SceneManager.LoadScene("NextScene");
    }
}
