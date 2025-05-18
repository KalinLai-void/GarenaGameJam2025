using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StoryManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI storyText;
    private bool isStoryPlaying;
    private bool isForcePlay;
    private int currStoryIndex = 0;
    private int storyLen = 0;
    private float playNextCharTime = 0.1f;
    private string nextSceneName = "SampleScene";

    [SerializeField]
    private GameObject messageTeller;
    [SerializeField]
    private GameObject mainUI;

    private KeyCode continueKey = KeyCode.Mouse0;
    private void Initialize()
    {
        isStoryPlaying = false;
        isForcePlay = false;
        storyLen = StoryContent.contents.Count;
        currStoryIndex = 0;
        NextText();

        
    }
    private void Awake()
    {
        Initialize();
    }

    private void OnEnable()
    {
        mainUI.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(continueKey) && !isStoryPlaying)
        {
            NextText();
        }
        else if (Input.GetKeyDown(continueKey) && isStoryPlaying)
        {
            ForceNextText();
        }
    }

    private void NextText()
    {
        storyText.text = "";
        if (currStoryIndex >= storyLen)
        {
            messageTeller.SetActive(false);
            mainUI.SetActive(true);
            //SceneManager.LoadScene(nextSceneName, LoadSceneMode.Additive);
            return;
        }
        StartCoroutine(StoryPlay(StoryContent.contents[currStoryIndex]));
        
    }

    IEnumerator StoryPlay(string contents)
    {
        isStoryPlaying = true;
        if (!isForcePlay)
        {
            for (int i = 0; i < contents.Length; i++)
            {
                if (!isForcePlay)
                {
                    storyText.text += contents[i];
                    yield return new WaitForSeconds(playNextCharTime);
                }
                else
                {
                    break;
                }
            }
        }
        if (!isForcePlay)
        {
            currStoryIndex++;
            isStoryPlaying = false;
        }
        else
        {
            isForcePlay = false;
        }
    }

    private void ForceNextText()
    {
        isForcePlay = true;
        isStoryPlaying = false;
        storyText.text = StoryContent.contents[currStoryIndex];
        currStoryIndex++;
    }
    
}
