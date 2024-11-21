using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Stage1_Puzzle1_Manager : MonoBehaviour
{
    [SerializeField] private GameObject hideGameObject;
    [SerializeField] private GameObject puzzle1;
    [SerializeField] private GameObject puzzle1Btn;
    [SerializeField] private GameObject puzzleSign;
    [SerializeField] private GameObject backGround;
    public Stage1_Puzzle1_GoalBoxCheck goalCheck;
    private Camera camera;
    private Vector3 currentPlayerPosition;
    
    
    private bool puzzle1PlayCondition = false;

    private void Awake()
    {
        camera = Camera.main;
    }

    private void Update()
    {
        if (puzzle1PlayCondition)
        {
            PuzzleStart();
        }
        if(goalCheck.goalCheck)PuzzleClear();
        
    }

    private void PuzzleStart()
    {
        if (Input.GetKey(KeyCode.E))
        {
            puzzle1Btn.SetActive(true);
            puzzleSign.SetActive(false);
            backGround.SetActive(false);
            currentPlayerPosition = camera.transform.position;
            camera.transform.position = puzzle1.transform.position;
            puzzle1PlayCondition = false;
        }
    }

    private void PuzzleClear()
    {
        hideGameObject.SetActive(true);
        Destroy(puzzle1);
        Destroy(puzzle1Btn);
        puzzleSign.SetActive(false);
        camera.transform.position = currentPlayerPosition;
        backGround.SetActive(true);
        Destroy(this);
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
            puzzleSign.SetActive(true);
            puzzle1PlayCondition = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        puzzleSign.SetActive(false);
        puzzle1PlayCondition = false;
    }
}
