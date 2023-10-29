using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelOutcomeUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI outcome_text, score_text;

    private void Start() {
        Setup();
        StartCoroutine(Continue());
    }

    private void Setup() {
        if(GameDetails.levelObjectivesData.ObjectivesComplete() == GameDetails.levelObjectivesData.ObjectivesTotal()){
            outcome_text.text = "Level " + GameDetails.current_level + " Complete";
            outcome_text.color = Color.green;
        }
        else{
            outcome_text.text = "Level Failed"; 
            outcome_text.color = Color.red;
        }

        score_text.text = GameDetails.levelObjectivesData.ObjectivesComplete().ToString() + "/" + GameDetails.levelObjectivesData.ObjectivesTotal().ToString();
    }

    private IEnumerator Continue(){
        yield return new WaitForSeconds(7f);
        if (GameDetails.current_level >= 5) SceneManager.LoadScene("StoryEnd", LoadSceneMode.Single);
        else SceneManager.LoadScene("LevelFinishScene", LoadSceneMode.Single);
    }
}
