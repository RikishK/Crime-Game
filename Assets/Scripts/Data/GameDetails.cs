using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameDetails
{
    public static int player_money = 0;

    // These should be uneccessary, levelObjectivesData should return details on completion
    public static int objectives_complete = 0;
    public static int objectives_total = 0;

    public static float time_left;

    public static int current_level = 0;

    public static LevelObjectivesData levelObjectivesData;
}
