using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeCell
{
    public int X;
    public int Y;
    public bool WallLeft = true;
    public bool WallBottom = true;
    public bool isVisited = false;
    public int DistanceFromStart;
}
public class MazeGenerator
{

    public MazeCell[,] GenerateMaze(int Width, int Height)
    {

        MazeCell[,] maze = new MazeCell[Width, Height];

        for(int x = 0; x < maze.GetLength(0); x++)
        {
            for(int y = 0; y < maze.GetLength(1); y++)
            {
                maze[x,y] = new MazeCell {X = x, Y = y};
            }
        }

        for(int x = 0; x < maze.GetLength(0); x++)
        {
            maze[x, Height - 1].WallLeft = false;
        }
        for(int y = 0; y < maze.GetLength(1); y++)
        {
            maze[Width - 1, y].WallBottom = false;
        }


        RemoveWallWithBactracking(maze, Width, Height);
        PlaceExit(maze, Width, Height);

        return maze;
    }

    private void RemoveWallWithBactracking(MazeCell[,] maze, int Width, int Height)
    {
        MazeCell current = maze[0,0];
        current.isVisited = true;
        current.DistanceFromStart = 0;

        Stack<MazeCell> stack = new Stack<MazeCell>();

        do
        {
            List<MazeCell> unvisited = new List<MazeCell>();

            if(current.X > 0 && !maze[current.X - 1, current.Y].isVisited) 
                unvisited.Add(maze[current.X - 1, current.Y]);
            if(current.Y > 0 && !maze[current.X, current.Y - 1].isVisited) 
                unvisited.Add(maze[current.X, current.Y - 1]);
            if(current.X < Width - 2 && !maze[current.X + 1, current.Y].isVisited) 
                unvisited.Add(maze[current.X + 1, current.Y]);
            if(current.Y < Height - 2 && !maze[current.X, current.Y + 1].isVisited) 
                unvisited.Add(maze[current.X, current.Y + 1]);

            if(unvisited.Count > 0)
            {
                MazeCell chosen = unvisited[UnityEngine.Random.Range(0,unvisited.Count)];
                RemoveWall(current,chosen);

                chosen.isVisited = true;
                stack.Push(chosen);
                current = chosen;
                chosen.DistanceFromStart = stack.Count;
            }
            else
            {
                current = stack.Pop();
            }

        } while(stack.Count > 0);
    }

    private void RemoveWall(MazeCell a, MazeCell b)
    {
        if(a.X == b.X)
        {
            if(a.Y > b.Y)
                a.WallBottom = false;
            else 
                b.WallBottom = false;
        }
        else
        {
            if(a.X > b.X)
                a.WallLeft = false;
            else 
                b.WallLeft = false;
        }
    }

    private void PlaceExit(MazeCell[,] maze, int Width, int Height)
    {
        MazeCell furthest = maze[0,0];

        for(int x = 0; x < maze.GetLength(0); x++)
        {
            if(maze[x, Height - 2].DistanceFromStart > furthest.DistanceFromStart)
                furthest = maze[x, Height - 2];
            if(maze[x,0].DistanceFromStart > furthest.DistanceFromStart)
                furthest = maze[x,0];
        }

        for (int y = 0; y < maze.GetLength(1); y++)
        {
            if(maze[Width - 2, y].DistanceFromStart > furthest.DistanceFromStart)
                furthest = maze[Width - 2, y];
            if(maze[0,y].DistanceFromStart > furthest.DistanceFromStart)
                furthest = maze[0,y];
        }

        if(furthest.X == 0)
            furthest.WallLeft = false;
        else if(furthest.Y == 0)
            furthest.WallBottom = false;
        else if(furthest.X == Width - 2)
            maze[furthest.X + 1, furthest.Y].WallLeft = false; 
        else if(furthest.Y == Height - 2)
            maze[furthest.X, furthest.Y + 1].WallBottom = false; 

    }

}
