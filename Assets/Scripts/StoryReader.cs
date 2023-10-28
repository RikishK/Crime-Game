using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class StoryReader : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI dialogue_text;
    [SerializeField] private Image left_talker_image;
    [SerializeField] private Image right_talker_image;
    [SerializeField] private TextAsset story_json;
    private StoryData storyData;
    private int dialogue_index;

    // Start is called before the first frame update
    void Start()
    {
        storyData = new StoryData();
        storyData.ReadStory(story_json);
        dialogue_index = 0;
        Dialogue();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void NextDialogue(){
        dialogue_index++;
        if(dialogue_index == storyData.story.script.Length){
            Continue();
        }
        else{
            Dialogue();
        }
    }

    private void Dialogue(){
        string dialogue = storyData.story.script[dialogue_index].message;
        string talker_name = storyData.story.script[dialogue_index].talker_name;
        if(storyData.story.script[dialogue_index].isLeftTalker){
            left_talker_image.CrossFadeAlpha(1.0f, 0.5f, true);
            right_talker_image.CrossFadeAlpha(0.5f, 0.5f, true);
        }
        else{
            left_talker_image.CrossFadeAlpha(0.5f, 0.5f, true);
            right_talker_image.CrossFadeAlpha(1.0f, 0.5f, true);
        }
        StartCoroutine(WriteDialogue(dialogue, talker_name, 1));
    }


    private IEnumerator WriteDialogue(string dialogue, string talker_name, int write_size){
        dialogue_text.text = talker_name + ": " + dialogue.Substring(0, write_size);
        if (write_size < dialogue.Length){
            yield return new WaitForSeconds(0.05f);
            StartCoroutine(WriteDialogue(dialogue, talker_name, Mathf.Min(write_size + 3, dialogue.Length)));
        }
        else{
            yield return new WaitForSeconds(2f);
            NextDialogue();
        }
    }

    private void Continue(){
        SceneManager.LoadScene("LevelCountdown", LoadSceneMode.Single);
    }
}
