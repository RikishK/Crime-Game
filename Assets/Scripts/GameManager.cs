using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Slider levelTimerBar;
    [SerializeField] private Image timerBarFill;
    [SerializeField] private Gradient timerBarGradient;
    private float timeLeft = 0;
    [SerializeField] private float levelTimeLimit;
    [SerializeField] private Vector3 startObjectiveLocation;
    [SerializeField] private int objectiveSlotsWidth;

    
    [SerializeField] private GameObject ammo_crate, explosive_crate;
    private int objectiveSummons = 0;
    private List<ObjectiveStatus> objectStatuses;
    private int score = 0;
    private float bonus_time = 0f, initial_time;

    // Start is called before the first frame update
    void Start()
    {
        levelTimerBar.maxValue = levelTimeLimit;
        levelTimerBar.minValue = timeLeft;
        levelTimerBar.value = levelTimeLimit;
        initial_time = Time.time;
        SetupObjectives();
    }

    // Update is called once per frame
    void Update()
    {
        timeLeft = levelTimeLimit + bonus_time - (Time.time - initial_time);
        levelTimerBar.value = timeLeft;
        timerBarFill.color = timerBarGradient.Evaluate(timeLeft/levelTimeLimit);
        if (timeLeft <= 0) TimeUp();
    }

    private void SetupObjectives(){
        GameDetails.levelObjectivesData.completedObjectives = new Dictionary<LevelObjectivesData.Objective, int>();
        GameDetails.levelObjectivesData.reqirementsObjectives = new Dictionary<LevelObjectivesData.Objective, int>();
        objectStatuses = new List<ObjectiveStatus>();
        
        foreach(LevelObjectivesData.LevelObjective levelObjective in GameDetails.GetLevelObjectives()){
            GameDetails.levelObjectivesData.completedObjectives.Add(levelObjective.objective, 0);
            GameDetails.levelObjectivesData.reqirementsObjectives.Add(levelObjective.objective, levelObjective.count);
            for(int i=0; i<levelObjective.count; i++) SummonObjective(levelObjective.objective);
        }
    }

    private void SummonObjective(LevelObjectivesData.Objective objective){
        // Summon an objective in a world location at correct slot based on objectiveSummons
        Vector3 summon_position = startObjectiveLocation;
        float x_shift = objectiveSummons % objectiveSlotsWidth;
        float y_shift = objectiveSummons / objectiveSlotsWidth;
        summon_position.x += x_shift;
        summon_position.y += y_shift * -1f;
        GameObject levelObjective = Instantiate(ObjectivePrefab(objective), summon_position, Quaternion.identity);
        ObjectiveStatus objectiveStatus = levelObjective.GetComponent<ObjectiveStatus>();
        objectiveStatus.gameManager = this;
        objectiveStatus.objective = objective;
        objectStatuses.Add(objectiveStatus);
        objectiveSummons++;
    }

    private GameObject ObjectivePrefab(LevelObjectivesData.Objective objective){
        if(objective == LevelObjectivesData.Objective.Ammo_Crate) return ammo_crate;

        switch(objective){
            case LevelObjectivesData.Objective.Ammo_Crate:
                return ammo_crate;
            case LevelObjectivesData.Objective.Explosive_Crate:
                return explosive_crate;
        }
        return null;
    }

    public void CompleteObjective(ObjectiveStatus objectiveStatus){
        score += objectiveStatus.points;
        bonus_time += objectiveStatus.time_value;
        GameDetails.levelObjectivesData.completedObjectives[objectiveStatus.objective]++;
        CheckFinished();
    }

    private void CheckFinished(){
        // Check if all objectives are done
        if(GameDetails.levelObjectivesData.ObjectivesComplete() == GameDetails.levelObjectivesData.ObjectivesTotal()){
            GainScore((int)timeLeft + 100);
            Continue();
        }
    }

    private void TimeUp(){
        Debug.Log("Times up");
        GainScore(0);
        Continue();
    }

    private void GainScore(int bonus){
        score += 30 * GameDetails.levelObjectivesData.ObjectivesComplete();
        score += bonus * 5;
        GameDetails.player_money += score;
        
    }

    private void Continue(){
        SceneManager.LoadScene("LevelOutcome", LoadSceneMode.Single);
    }
}
