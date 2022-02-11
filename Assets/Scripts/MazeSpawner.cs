using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class MazeSpawner : MonoBehaviour
{
    [SerializeField]private int Width = 10;
    [SerializeField]private int Height = 10;
    public static Action<Cell,float> _generated;
    public Cell CellPrefab;
    private RectTransform _layoutRect;
    private GridLayoutGroup _layout;
    void Start()
    {
        TryGetComponent(out _layout);
        TryGetComponent(out _layoutRect);
        MazeGenerator generator = new MazeGenerator();
        MazeCell[,] maze = generator.GenerateMaze(Width,Height);
        Cell startCell = null;

        for(int i = 0; i < Height + 1; i++)
        {
            Cell c = Instantiate(CellPrefab);
            c.transform.SetParent(transform);
            c.transform.localScale = Vector3.one;
            c.WallLeft.SetActive(false);
            c.WallBottom.SetActive(false);
        }

        for(int x = 0; x < maze.GetLength(0); x++)
        {
            Cell c1 = Instantiate(CellPrefab);
            c1.transform.SetParent(transform);
            c1.transform.localScale = Vector3.one;
            c1.WallLeft.SetActive(false);
            c1.WallBottom.SetActive(false);
            for(int y = 0; y < maze.GetLength(1); y++)
            {
                
                Cell c = Instantiate(CellPrefab);
                c.WallLeft.SetActive(maze[x,y].WallLeft);
                c.WallBottom.SetActive(maze[x,y].WallBottom);
                c.transform.SetParent(transform);
                c.transform.localScale = Vector3.one;

                if(x==0 && y ==0)
                    startCell = c;
            }
        }
        float step = _layoutRect.rect.height / (Height + 1);
        StartCoroutine(GeneratorCallback(startCell, step));
    }

    private IEnumerator GeneratorCallback(Cell startCell, float step)
    {
        yield return new WaitForSeconds(.1f);
        _generated?.Invoke(startCell, step);
    }

    private void LateUpdate()
    {
        float width = _layoutRect.rect.width / (Width + 1);
        float height = _layoutRect.rect.height / (Height + 1);
        _layout.cellSize = new Vector3(height, height);
    }

}
