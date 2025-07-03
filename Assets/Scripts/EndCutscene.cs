using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndCutscene : MonoBehaviour
{
    public GameObject cutscene;
    public GameObject dialouge;
    public GameObject credits;
    public GameObject gamejam;
    void Start()
    {
        dialouge.SetActive(false);
        credits.SetActive(false);
        gamejam.SetActive(false);
       StartCoroutine(WaitForCutscene());
    }

    void Update()
    {   
    }

    IEnumerator WaitForCutscene()
    {
        yield return new WaitForSeconds(12F);
        cutscene.SetActive(false);
        dialouge.SetActive(true);
        yield return new WaitForSeconds(8f);
        dialouge.SetActive(false);
        gamejam.SetActive(true);
        yield return new WaitForSeconds(4f);
        gamejam.SetActive(false);
        credits.SetActive(true);
    }
}
