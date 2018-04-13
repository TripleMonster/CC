using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[Serializable]
public class PathInfo {
    public int level;
    public int cubeCount;
    public List<CoordinateInfo> coordinate = new List<CoordinateInfo>();
}

[Serializable]
public class CoordinateInfo {
    public float x;
    public float z;

    public CoordinateInfo(float infoX, float infoZ) {
        x = infoX;
        z = infoZ;
    }
}
