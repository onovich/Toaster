using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToasterSampleEditor : MonoBehaviour {

    [Header("输出")] public ToasterSampleSO model;
    [Header("覆盖单位数")] public Vector2Int UnitCount = new Vector2Int(10, 10);
    [Header("每单位网格数(n x n)")] public int MPU = 1;
    Vector2 stageOffset => new Vector2(-UnitCount.x * 0.5f, -UnitCount.y * 0.5f);


}
