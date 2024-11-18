using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.UI;

public class Stage1_Puzzle2_Board : MonoBehaviour
{
    [SerializeField] private GameObject tilePrefab;
    [SerializeField] private Transform tilesParent;

    private List<Stage1_Puzzle2_Tile> tileList;
    private Vector2Int puzzleSize = new Vector2Int(4, 4);
    private float neighborTileDistance = 102;
    
    public Vector3 emptyTilePosition { set; get; }

    private IEnumerator Start()
    {
        tileList = new List<Stage1_Puzzle2_Tile>();
        SpawnTiles();
        LayoutRebuilder.ForceRebuildLayoutImmediate(tilesParent.GetComponent<RectTransform>());
        yield return new WaitForEndOfFrame();
        StartCoroutine("OnSuffle");
    }

    private void SpawnTiles()
    {
        for (int y = 0; y < puzzleSize.y; ++ y)
        {
            for (int x = 0; x < puzzleSize.x; ++ x)
            {
                GameObject clone =  Instantiate(tilePrefab, tilesParent);
                Stage1_Puzzle2_Tile tile = clone.GetComponent<Stage1_Puzzle2_Tile>();

                tile.Setup(puzzleSize.x * puzzleSize.y, y * puzzleSize.x + x + 1);
                
                tileList.Add(tile);
            }
        }
    }

    private IEnumerator OnSuffle()
    {
        float current = 0f;
        float percent = 0f;
        float time = 1.5f;
        while (percent < 1)
        {
            current += Time.deltaTime;
            percent = current / time;
            int index = Random.Range(0, puzzleSize.x * puzzleSize.y);
            tileList[index].transform.SetAsLastSibling();

            yield return null;
        }
    }
}
