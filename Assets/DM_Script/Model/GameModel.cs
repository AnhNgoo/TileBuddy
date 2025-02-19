﻿/*
 * Created on 2024
 *
 * Copyright (c) 2024 dotmobstudio
 * Support : dotmobstudio@gmail.com
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class GameModel{
    public static string BACK_GAME_CONTIUE = "contiue";
    public static string BACK_GAME_CLOSE = "close";
    public static string BACK_GAME_RESTART = "restart";
    public static string BACK_GAME_ADDTIME = "addTime";
    public static string BACK_GAME_FAIL = "fail";
    
    
    
        
    public static List<Point> CheckLink(Point a, Point b , List<List<Item>> itemList )
    {
        List<Point> pathList = new List<Point>();
        if (a.x == b.x && CheckHorizon(a, b, itemList))
        {
            pathList.Add(a);
            pathList.AddRange(CheckPathList(a, b , itemList));
            pathList.Add(b);
            return pathList;
        }
        if (a.y == b.y && CheckVertical(a, b, itemList))
        {
            pathList.Add(a);
            pathList.AddRange(CheckPathList(a, b , itemList));
            pathList.Add(b);
            return pathList;
        }
        pathList = CheckOneCorner(a, b, itemList);
        if (pathList.Count != 0)
        {
            return pathList;
        }
        pathList = CheckTwoCorner(a, b, itemList);
        return pathList;
    }


    private static bool CheckHorizon(Point a, Point b, List<List<Item>> itemList)
    {
        if (a.x == b.x && a.y == b.y) return false;  
        int x_start = a.y < b.y ? a.y : b.y;        
        int x_end = a.y < b.y ? b.y : a.y;          
        
        for (int i = x_start + 1; i < x_end; i++)
        {
            if (itemList[a.x][i].hasItem) 
            {
                return false;
            }
        }
        return true;
    }

    private static bool CheckVertical(Point a, Point b, List<List<Item>> itemList)
    {
        if (a.x == b.x && a.y == b.y) return false;
        int y_start = a.x < b.x ? a.x : b.x;
        int y_end = a.x < b.x ? b.x : a.x;
        for (int i = y_start + 1; i < y_end; i++)
        {
            if (itemList[i][a.y].hasItem)
            {
                return false;
            }
        }
        return true;
    }

    private static List<Point> CheckOneCorner(Point a, Point b, List<List<Item>> itemList) 
    {
        List<Point> pathList = new List<Point>();
        Point c = new Point(b.x, a.y);
        Point d = new Point(a.x, b.y);
                     
        if (!itemList[c.x][c.y].hasItem)
        {
            bool path1 = CheckHorizon(b, c, itemList) && CheckVertical(a, c, itemList);
            if (path1)
            {
                pathList.Add(a);
                pathList.AddRange(CheckPathList(a, c , itemList));
                pathList.Add(c);
                pathList.AddRange(CheckPathList(c, b, itemList));
                pathList.Add(b);
            }
            return pathList;
        }
        
        if (!itemList[d.x][d.y].hasItem)
        {
            bool path2 = CheckHorizon(a, d, itemList) && CheckVertical(b, d , itemList);
            if (path2)
            {
                pathList.Add(a);
                pathList.AddRange(CheckPathList(a, d , itemList));
                pathList.Add(d);
                pathList.AddRange(CheckPathList(d, b , itemList));
                pathList.Add(b);
            }
            return pathList;
        }
        return pathList;
        
    }

  
    private static List<Point> CheckTwoCorner(Point a, Point b, List<List<Item>> itemList)
    {
        List<Point> pathList = new List<Point>();

        List<Line> ll = Scan(a, b , itemList);

        for (int i = 0; i < ll.Count; i++)
        {
            Line tmpLine = ll[i];
            if (tmpLine.direct == 1)
            {
                if (CheckVertical(a, tmpLine.a , itemList) && CheckVertical(b, tmpLine.b , itemList))
                {
                    pathList.Add(a);
                    pathList.AddRange(CheckPathList(a, tmpLine.a, itemList));
                    pathList.Add(tmpLine.a);
                    pathList.AddRange(CheckPathList(tmpLine.a, tmpLine.b, itemList));
                    
                    pathList.Add(tmpLine.b);
                    pathList.AddRange(CheckPathList(tmpLine.b, b, itemList));
                    pathList.Add(b);                    
                    return pathList;
                }
            }
            else if (tmpLine.direct == 0)
            {
                if (CheckHorizon(a, tmpLine.a, itemList) && CheckHorizon(b, tmpLine.b, itemList))
                {
                    pathList.Add(a);
                    pathList.AddRange(CheckPathList(a, tmpLine.a, itemList));
                    pathList.Add(tmpLine.a);
                    pathList.AddRange(CheckPathList(tmpLine.a, tmpLine.b, itemList));
                    pathList.Add(tmpLine.b);
                    pathList.AddRange(CheckPathList(tmpLine.b, b, itemList));
                    pathList.Add(b);
                    return pathList;
                }
            }
        }
        return pathList;
    }
    public static void debugList(List<Point> pathList) {
        foreach (var item in pathList)
        {
            Debug.Log(item.x + " " + item.y);
        }
    }

    private static List<Line> Scan(Point a, Point b, List<List<Item>> itemList)
    {
        int row = itemList.Count;
        int col = itemList[0].Count;
        List<Line> linkList = new List<Line>();
        
        for (int i = a.y; i >= 0; i--)
        {
            if (!itemList[a.x][i].hasItem && !itemList[b.x][i].hasItem && CheckVertical(new Point(a.x, i), new Point(b.x, i) , itemList))
            {
                linkList.Add(new Line(new Point(a.x, i), new Point(b.x, i), 0));
            }
        }
       
        for (int i = a.y; i < col; i++)
        {
            if (!itemList[a.x][i].hasItem && !itemList[b.x][i].hasItem && CheckVertical(new Point(a.x, i), new Point(b.x, i) , itemList))
            {
                linkList.Add(new Line(new Point(a.x, i), new Point(b.x, i), 0));
            }
        }
        
        for (int j = a.x; j >= 0; j--)
        {
            if (!itemList[j][a.y].hasItem && !itemList[j][b.y].hasItem && CheckHorizon(new Point(j, a.y), new Point(j, b.y) , itemList))
            {
                linkList.Add(new Line(new Point(j, a.y), new Point(j, b.y), 1));
            }
        }
        
        for (int j = a.x; j < row; j++)
        {
            if (!itemList[j][a.y].hasItem && !itemList[j][b.y].hasItem  && CheckHorizon(new Point(j, a.y), new Point(j, b.y) , itemList))
            {
                linkList.Add(new Line(new Point(j, a.y), new Point(j, b.y), 1));
            }
        }

        return linkList;
    }

    private static List<Point> CheckPathList(Point a, Point b, List<List<Item>> itemList)
    {
        List<Point> pathList = new List<Point>();
        if (a.x != b.x)
        {
            int y_start = a.x;
            int y_end = b.x;
            int co = y_start < y_end ? 1 : -1; 
            for (int i = y_start * co + 1; i < y_end * co; i++)
            {
                pathList.Add(itemList[i * co][a.y].pos);
            }
            return pathList;
        }

        if (a.y != b.y)
        {
            int x_start = a.y;     
            int x_end = b.y;         
            int co = x_start < x_end ? 1 : -1;

            for (int i = x_start * co + 1; i < x_end * co; i++)
            {
                pathList.Add(itemList[a.x][i * co].pos);
            }
            return pathList;
        }
        return pathList;
    }

    public static bool IsFinish(List<List<Item>> itemList)
    {
        for (int i = 0; i < itemList.Count; i++)
        {
            for (int j = 0; j < itemList[i].Count; j++)
            {
                if (itemList[i][j].hasItem)
                {
                    return false;
                }
            }
        }
        return true;
    }

}
