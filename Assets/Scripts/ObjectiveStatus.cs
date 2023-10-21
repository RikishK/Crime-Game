using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveStatus : MonoBehaviour
{
    public bool completed = false;
    public int points;
    public float time_value;

    public LevelObjectivesData.Objective objective;
    public GameManager gameManager;

    public void Complete(){
        completed = true;
        gameManager.CompleteObjective(this);
    }
}
