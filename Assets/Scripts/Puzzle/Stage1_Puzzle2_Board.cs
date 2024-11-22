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
    private float neighborTileDistance = 222;
    public bool clearCondition = false;

    public Vector3 emptyTilePosition { set; get; }

    private IEnumerator Start()
    {
        tileList = new List<Stage1_Puzzle2_Tile>();
        SpawnTiles();
        LayoutRebuilder.ForceRebuildLayoutImmediate(tilesParent.GetComponent<RectTransform>());
        yield return new WaitForEndOfFrame(); // 레이아웃이 재구성된 후 위치 처리를 위해 대기
        tileList.ForEach(x => x.SetCorrectPosition());
        StartCoroutine(OnShuffle());
    }

    private void SpawnTiles()
    {
        for (int y = 0; y < puzzleSize.y; ++y)
        {
            for (int x = 0; x < puzzleSize.x; ++x)
            {
                GameObject clone = Instantiate(tilePrefab, tilesParent);
                Stage1_Puzzle2_Tile tile = clone.GetComponent<Stage1_Puzzle2_Tile>();

                tile.Setup(this, puzzleSize.x * puzzleSize.y, y * puzzleSize.x + x + 1);

                tileList.Add(tile);
            }
        }
    }

    private IEnumerator OnShuffle()
    {
        bool solvable = false;
        while (!solvable)
        {
            // 타일 셔플
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

            // 빈 타일 위치 업데이트
            emptyTilePosition = tileList[tileList.Count - 1].GetComponent<RectTransform>().localPosition;

            // 퍼즐이 풀 수 있는지 확인
            solvable = IsSolvable();
        }
    }

    private bool IsSolvable()
    {
        // 현재 타일 숫자들을 가져옴
        List<int> tileNumbers = new List<int>();

        int childCount = tilesParent.childCount;
        for (int i = 0; i < childCount; i++)
        {
            Transform child = tilesParent.GetChild(i);
            Stage1_Puzzle2_Tile tile = child.GetComponent<Stage1_Puzzle2_Tile>();
            tileNumbers.Add(tile.Numeric);
        }

        int gridWidth = puzzleSize.x;

        // 빈 타일 숫자 제거
        int emptyTileNumber = puzzleSize.x * puzzleSize.y;
        int emptyTileIndex = tileNumbers.IndexOf(emptyTileNumber);
        tileNumbers.Remove(emptyTileNumber);

        // Inversion 수 계산
        int inversionCount = 0;
        for (int i = 0; i < tileNumbers.Count - 1; i++)
        {
            for (int j = i + 1; j < tileNumbers.Count; j++)
            {
                if (tileNumbers[i] > tileNumbers[j])
                {
                    inversionCount++;
                }
            }
        }

        // 빈 타일의 행 위치 (아래로부터)
        int emptyTileRowFromTop = emptyTileIndex / gridWidth;
        int emptyTileRowFromBottom = gridWidth - emptyTileRowFromTop;

        // 그리드 너비와 inversion 수에 따른 풀이 가능 여부 판단
        if (gridWidth % 2 == 0)
        {
            // 그리드 너비가 짝수인 경우
            if ((emptyTileRowFromBottom % 2 == 0 && inversionCount % 2 == 1) ||
                (emptyTileRowFromBottom % 2 == 1 && inversionCount % 2 == 0))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            // 그리드 너비가 홀수인 경우
            return inversionCount % 2 == 0;
        }
    }

    public void IsMoveTile(Stage1_Puzzle2_Tile tile)
    {
        if (Vector3.Distance(emptyTilePosition,
                tile.GetComponent<RectTransform>().localPosition) == neighborTileDistance)
        {
            Vector3 goalPosition = emptyTilePosition;
            emptyTilePosition = tile.GetComponent<RectTransform>().localPosition;
            tile.OnMoveTo(goalPosition);
        }
    }

    public void IsGameOver()
    {
        List<Stage1_Puzzle2_Tile> tiles = tileList.FindAll(x => x.isCorrected == true);
        if (tiles.Count == puzzleSize.x * puzzleSize.y - 1)
        {
            clearCondition = true;
        }
    }
}
