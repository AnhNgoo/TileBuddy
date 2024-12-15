/*
 * Created on 2024
 *
 * Copyright (c) 2024 dotmobstudio
 * Support : dotmobstudio@gmail.com
 */
using UnityEngine;
using System.Collections;

public class Line {

	public Point a;
	public Point b;
	
	public int direct;

	public Line(Point aa,Point bb,int dir)
	{
		a = aa;
		b = bb;
		direct = dir;
	}

}
