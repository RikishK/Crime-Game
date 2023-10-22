using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CountdownObjectiveInfo : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI objective_count_text;
    [SerializeField] private Image objective_image;

    public void Setup(Sprite icon, int count){
        objective_count_text.text = count.ToString();
        objective_image.sprite = icon;
    }

}
