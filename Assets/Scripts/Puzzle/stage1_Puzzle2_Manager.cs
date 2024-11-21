using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stage1_Puzzle2_Manager : MonoBehaviour
{
    [SerializeField] private GameObject hideGameObject;
    [SerializeField] private GameObject puzzle2;
    [SerializeField] private GameObject puzzleSign;

    public Stage1_Puzzle2_Board clearCheck;
    private bool puzzle2PlayCondition = false;
    private void Update()
    {
        if (puzzle2PlayCondition)
        {
            PuzzleStart();
        }
        if(clearCheck.clearCondition)PuzzleClear();
        
    }
    private void PuzzleStart()
    {
        if (Input.GetKey(KeyCode.E))
        {
            puzzle2.SetActive(true);
            puzzleSign.SetActive(false);
            puzzle2PlayCondition = false;
        }
    }

    private void PuzzleClear()
    {
        hideGameObject.SetActive(true);
        Destroy(puzzle2);
        Destroy(puzzleSign);
        Destroy(this);
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        puzzleSign.SetActive(true);
        puzzle2PlayCondition = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        puzzleSign.SetActive(false);
        puzzle2PlayCondition = false;
    }
}
