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

    //private LevelObjectivesData.LevelObjective[] levelObjectives;
    //private Dictionary<LevelObjectivesData.LevelObjective.Objective, int> completedObjectives, reqirementsObjectives;
    
    [SerializeField] private GameObject crate;
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

    // [Serializable]
    // private class LevelObjective {
    //     public Objective objective;
    //     public int count;
    //     public enum Objective{
    //         Ammo_Crate
    //     }
    // }

    private void SetupObjectives(){
        GameDetails.levelObjectivesData.completedObjectives = new Dictionary<LevelObjectivesData.Objective, int>();
        GameDetails.levelObjectivesData.reqirementsObjectives = new Dictionary<LevelObjectivesData.Objective, int>();
        objectStatuses = new List<ObjectiveStatus>();
        // foreach(LevelObjectivesData.LevelObjective levelObjective in levelObjectives){
        //     completedObjectives.Add(levelObjective.objective, 0);
        //     reqirementsObjectives.Add(levelObjective.objective, levelObjective.count);
        //     for(int i=0; i<levelObjective.count; i++) SummonObjective(levelObjective.objective);
        // }
        foreach(LevelObjectivesData.LevelObjective levelObjective in GetLevelObjectives()){
            GameDetails.levelObjectivesData.completedObjectives.Add(levelObjective.objective, 0);
            GameDetails.levelObjectivesData.reqirementsObjectives.Add(levelObjective.objective, levelObjective.count);
            for(int i=0; i<levelObjective.count; i++) SummonObjective(levelObjective.objective);
        }
    }

    private LevelObjectivesData.LevelObjective[] GetLevelObjectives(){
        return GameDetails.levelObjectivesData.levels.levelObjectiveList[GameDetails.current_level].objectives;
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
        if(objective == LevelObjectivesData.Objective.Ammo_Crate) return crate;
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
            GainScore((int)timeLeft);
            Continue();
        }
    }

    private void TimeUp(){
        Debug.Log("Times up");
        GainScore(0);
        Continue();
    }

    private void GainScore(int bonus){
        score += GameDetails.levelObjectivesData.ObjectivesComplete();
        score += bonus * 5;
        GameDetails.player_money += score;
    }

    private void Continue(){
        SceneManager.LoadScene("LevelFinishScene", LoadSceneMode.Single);
    }
}
