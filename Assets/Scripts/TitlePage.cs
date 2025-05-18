using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitlePage : MonoBehaviour
{

    private string sceneName = "SampleScene";
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnStartButtonClick()
    {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
    }
}
