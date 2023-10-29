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
        Ammo_Crate, Explosive_Crate
    }

    [System.Serializable]
    public class LevelObjectiveList{
        public List<LevelObjective> objectives;
    }

    [System.Serializable]
    public class Levels{
        public List<LevelObjectiveList> levelObjectiveList;
    }

    public void ReadLevels(TextAsset levelJson){
        levels = JsonUtility.FromJson<Levels>(levelJson.text);
        
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
