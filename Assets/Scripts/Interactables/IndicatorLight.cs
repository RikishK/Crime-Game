using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndicatorLight : MonoBehaviour
{
    [SerializeField] private SpriteRenderer lightRenderer;
    [SerializeField] private Sprite emptyLight, lowLight, mediumLight, fullLight;

    public void updateLight(int curr, int max){
        lightRenderer.sprite = getLightLevel(curr, max);
    }
    private Sprite getLightLevel(int curr, int max){
        float percentage = (float)curr / (float)max;
        Debug.Log($"{percentage}%");
        if(curr == 0) return emptyLight;
        if(curr == max) return fullLight;
        if(percentage < 0.4f) return lowLight;
        return mediumLight;
    }
}
