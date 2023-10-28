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
        if(GameDetails.objectives_complete == GameDetails.objectives_total){
            outcome_text.text = "Level Complete";
            outcome_text.color = Color.green;
        }
        else{
            outcome_text.text = "Level Failed"; 
            outcome_text.color = Color.red;
        }

        score_text.text = GameDetails.objectives_complete.ToString() + "/" + GameDetails.objectives_total.ToString();
    }

    private IEnumerator Continue(){
        yield return new WaitForSeconds(7f);
        SceneManager.LoadScene("LevelFinishScene", LoadSceneMode.Single);
    }
}
