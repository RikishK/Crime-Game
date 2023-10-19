using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Slider levelTimerBar;
    [SerializeField] private Image timerBarFill;
    [SerializeField] private Gradient timerBarGradient;
    private float timeLeft;
    [SerializeField] private float levelTimeLimit;
    [SerializeField] private Vector3 startObjectiveLocation;
    [SerializeField] private int objectiveSlotsWidth;

    [SerializeField] private LevelObjective[] levelObjectives;
    private Dictionary<LevelObjective.Objective, int> completedObjectives, reqirementsObjectives;
    [SerializeField] private GameObject crate;
    private int objectiveSummons = 0;
    private List<ObjectiveStatus> objectStatuses;
    private int score;
    private float bonus_time = 0f;

    // Start is called before the first frame update
    void Start()
    {
        levelTimerBar.maxValue = levelTimeLimit;
        levelTimerBar.minValue = timeLeft;
        levelTimerBar.value = levelTimeLimit;
        SetupObjectives();
    }

    // Update is called once per frame
    void Update()
    {
        timeLeft = levelTimeLimit + bonus_time - Time.time;
        levelTimerBar.value = timeLeft;
        timerBarFill.color = timerBarGradient.Evaluate(timeLeft/levelTimeLimit);
        if (timeLeft <= 0) TimeUp();
    }

    [Serializable]
    private class LevelObjective {
        public Objective objective;
        public int count;
        public enum Objective{
            Ammo_Crate
        }
    }

    private void SetupObjectives(){
        completedObjectives = new Dictionary<LevelObjective.Objective, int>();
        reqirementsObjectives = new Dictionary<LevelObjective.Objective, int>();
        objectStatuses = new List<ObjectiveStatus>();
        foreach(LevelObjective levelObjective in levelObjectives){
            completedObjectives.Add(levelObjective.objective, 0);
            reqirementsObjectives.Add(levelObjective.objective, levelObjective.count);
            for(int i=0; i<levelObjective.count; i++) SummonObjective(levelObjective.objective);
        }
    }

    private void SummonObjective(LevelObjective.Objective objective){
        // Summon an objective in a world location at correct slot based on objectiveSummons
        Vector3 summon_position = startObjectiveLocation;
        float x_shift = objectiveSummons % objectiveSlotsWidth;
        float y_shift = objectiveSummons / objectiveSlotsWidth;
        summon_position.x += x_shift;
        summon_position.y += y_shift * -1f;
        GameObject levelObjective = Instantiate(ObjectivePrefab(objective), summon_position, Quaternion.identity);
        ObjectiveStatus objectiveStatus = levelObjective.GetComponent<ObjectiveStatus>();
        objectiveStatus.gameManager = this;
        objectStatuses.Add(objectiveStatus);
        objectiveSummons++;
    }

    private GameObject ObjectivePrefab(LevelObjective.Objective objective){
        if(objective == LevelObjective.Objective.Ammo_Crate) return crate;
        return null;
    }

    public void CompleteObjective(ObjectiveStatus objectiveStatus){
        score += objectiveStatus.points;
        bonus_time += objectiveStatus.time_value;
        CheckFinished();
    }

    private void CheckFinished(){
        // Check if all objectives are done 
    }

    private void TimeUp(){
        Debug.Log("Times up");
        
    }
}
