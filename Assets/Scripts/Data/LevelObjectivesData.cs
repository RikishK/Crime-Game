using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelObjectivesData
{
    public Dictionary<Objective, int> completedObjectives, reqirementsObjectives;
    public Levels levels;
    //private LevelObjective[] levelObjectives;
    [System.Serializable]
    public class LevelObjective {
        public Objective objective;
        public int count;
    }

    [System.Serializable]
    public enum Objective{
        Ammo_Crate
    }

    [System.Serializable]
    public class LevelObjectiveList{
        public LevelObjective[] objectives;
    }

    [System.Serializable]
    public class Levels{
        public LevelObjectiveList[] levelObjectiveList;
    }

    public void ReadLevels(TextAsset levelJson){
        levels = JsonUtility.FromJson<Levels>(levelJson.text);
        Debug.Log("Reading --------------------");
        foreach(LevelObjectiveList list in levels.levelObjectiveList)
        Debug.Log("-----------------------");
    }

    public int ObjectivesComplete(){
        int total = 0;
        foreach(KeyValuePair<Objective, int> obj in completedObjectives){
            total += obj.Value;
        }
        return total;
    }

    public int ObjectivesTotal(){
        int total = 0;
        foreach(KeyValuePair<Objective, int> obj in reqirementsObjectives){
            total += obj.Value;
        }
        return total;
    }
}
