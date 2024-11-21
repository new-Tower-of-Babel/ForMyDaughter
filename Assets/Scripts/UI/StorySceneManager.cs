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

    private int firstStoryCount = 0;

    private int fullStoryCount = 4;
    //front.GetComponent<Image>().sprite = Resources.Load<Sprite>($"KiHyeok{idx - (type * 2) + 1}");

    private void Awake()
    {
        image = GameObject.Find("Image");
        text = GameObject.Find("Text");
    }

    private void Start()
    {
        StorySceneFirstImage();
    }

    private void StorySceneFirstImage()
    {
        if (firstStoryCount == 0 && !Finish.AllClearCheck)
        {
            image.GetComponent<Image>().sprite = Resources.Load<Sprite>($"1-1 happy family");
            text.GetComponent<TextMeshProUGUI>().text = "There was a family living happily in a village.";
        }else if (firstStoryCount == fullStoryCount && !Finish.AllClearCheck)
        {
            image.GetComponent<Image>().sprite = Resources.Load<Sprite>($"fail");
            text.GetComponent<TextMeshProUGUI>().text = "As you collapsed in the forest, the girl, who had been suffering from her illness, passed away.";
        }else if (firstStoryCount == fullStoryCount &&Finish.AllClearCheck)
        {
            image.GetComponent<Image>().sprite = Resources.Load<Sprite>($"clear");
            text.GetComponent<TextMeshProUGUI>().text = "When you safely returned with the herbs, the doctor used them to cure the girl's illness, and you lived happily with her.";
        }
    }
    

    public void StoryChangerBtn()
    {
        if (firstStoryCount < fullStoryCount || !Finish.AllClearCheck)
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
        else
        {
            SceneManager.LoadScene("TitleScene");
        }
    }
}
