using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Stage1_Puzzle2_Tile : MonoBehaviour, IPointerClickHandler
{
    private TextMeshProUGUI textNumeric;
    private Stage1_Puzzle2_Board board;
    private Vector3 correctPosition; 
    private int numeric;
    private IPointerClickHandler pointerClickHandlerImplementation;

    public int Numeric
    {
        set
        {
            numeric = value;
            textNumeric.text = numeric.ToString();
        }
        get => numeric;
    }

    public void Setup(Stage1_Puzzle2_Board board,int hideNumeric, int numeric)
    {
        this.board = board;
        textNumeric = GetComponentInChildren<TextMeshProUGUI>();
        Numeric = numeric;
        if (Numeric == hideNumeric)
        {
            GetComponent<Image>().enabled = false;
            textNumeric.enabled = false;
        }
    }

    public void setCorrectPosition()
    {
        correctPosition = GetComponent<RectTransform>().localPosition;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
    }

    public void OnMoveTo(Vector3 end)
    {
        StartCoroutine("MoveTo", end);
    }

    private IEnumerator MoveTo(Vector3 end)
    {
        float current = 0f;
        float percent = 0f;
        float moveTime = 0.1f;
        Vector3 start = GetComponent<RectTransform>().localPosition;
        while (percent < 1)
        {
            current += Time.deltaTime;
            percent = current / moveTime;
            GetComponent<RectTransform>().localPosition = Vector3.Lerp(start, end, percent);
            yield return null;
        }
    }
}
