using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StorySceneManager : MonoBehaviour
{
    private GameObject image;

    private GameObject text;

    private GameObject btn;

    private int firstStoryCount = 0;

    private int fullStoryCount = 4;
    //front.GetComponent<Image>().sprite = Resources.Load<Sprite>($"KiHyeok{idx - (type * 2) + 1}");

    private void Awake()
    {
        image = GameObject.Find("Image");
        text = GameObject.Find("Text");
        btn = GameObject.Find("Button");
        image.GetComponent<Image>().sprite = Resources.Load<Sprite>($"1-1 happy family");
        text.GetComponent<TextMeshProUGUI>().text = "There was a family living happily in a village.";
        
    }
    

    public void StoryChangerBtn()
    {
        firstStoryCount++;
        switch (firstStoryCount)
        {
            case 1:
                image.GetComponent<Image>().sprite = Resources.Load<Sprite>($"1-2 mother's death");
                text.GetComponent<TextMeshProUGUI>().text = "However, one day, misfortune struck, and the knight's wife, who was also the girl's mother, passed away due to illness.";
                break;
            case 2:
                image.GetComponent<Image>().sprite = Resources.Load<Sprite>($"1-3 sick daughter");
                text.GetComponent<TextMeshProUGUI>().text = "Perhaps because of that sorrow, the girl also fell ill.";
                break;
            case 3:
                image.GetComponent<Image>().sprite = Resources.Load<Sprite>($"1-4 consultation with a doctor");
                text.GetComponent<TextMeshProUGUI>().text = "Perhaps because of that sorrow, the girl also fell ill.";
                break;
            default:
                SceneManager.LoadScene("Stage1");
                break;
        }
    }
}
