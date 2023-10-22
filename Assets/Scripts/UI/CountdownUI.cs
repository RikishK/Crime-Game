using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountdownUI : MonoBehaviour
{
    [SerializeField] GameObject[] countdown_objective_objects;
    [SerializeField] Sprite Ammo_Crate_Icon;
    
    private void Start() {
        Setup();
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
}
