using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryData
{
    public Story story;

    [System.Serializable]
    public class Dialogue {
        public string talker_name;
        public string message;
        public bool isLeftTalker;
    }

    [System.Serializable]
    public class Story {
        public Dialogue[] script;
    }

    public void ReadStory(TextAsset storyJson){
        story = JsonUtility.FromJson<Story>(storyJson.text);
    }
}
