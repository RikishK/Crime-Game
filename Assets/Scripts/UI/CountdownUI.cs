using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CountdownUI : MonoBehaviour
{
    [SerializeField] private GameObject[] countdown_objective_objects;
    [SerializeField] private Sprite Ammo_Crate_Icon;
    [SerializeField] private TextMeshProUGUI countdown_timer_text;

    
    private void Start() {
        Setup();
        StartCoroutine(Countdown(5));
    }
    public void Setup(){
        LevelObjectivesData.LevelObjective[] level_objectives = GameDetails.GetLevelObjectives();
        for(int i=0; i<10; i++){
            if(i >= level_objectives.Length){
                countdown_objective_objects[i].SetActive(false);
                continue;
            }
            LevelObjectivesData.LevelObjective levelObjective = level_objectives[i];
            countdown_objective_objects[i].SetActive(true);
            CountdownObjectiveInfo objectiveInfo = countdown_objective_objects[i].GetComponent<CountdownObjectiveInfo>();
            Sprite icon = GetIcon(levelObjective.objective);
            objectiveInfo.Setup(icon, levelObjective.count);
        }
        
    }

    private Sprite GetIcon(LevelObjectivesData.Objective objective){
        switch(objective){
            case LevelObjectivesData.Objective.Ammo_Crate:
                return Ammo_Crate_Icon;
        }
        return null;
    }

    private IEnumerator Countdown(int count){
        countdown_timer_text.text = count.ToString();
        yield return new WaitForSeconds(1f);
        count--;
        if(count == 0){
            SceneManager.LoadScene("PlayScene", LoadSceneMode.Single);
        }
        else{
            StartCoroutine(Countdown(count));
        }
    }
}
