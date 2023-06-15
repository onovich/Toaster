using ClipperLib;
using System;
using System.Linq;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using MortiseFrame.Knot;
using MortiseFrame.Knot.Shape2D;
using MortiseFrame.Modifier.Toaster.Util;
using MortiseFrame.Modifier.Toaster.Generic;

namespace MortiseFrame.Modifier.Toaster {

    public class ToasterEditorEntity : MonoBehaviour {

        Vector2Int cellCount;
        Vector2 cellSize;

        AABB[] cells;
        AABB[] aabbs;
        OBB[] obbs;
        Circle[] circles;

        Vector2 stageOffset;
        int MPU = 1;

        Vector2Int UnitCount = new Vector2Int(10, 10);

        ToasterGridTM tm;

        bool isBaked = false;

        [ContextMenu("一键烘焙")]
        void Bake<T>() where T : MonoBehaviour {

            isBaked = false;

            ClearGrid();
            ClearObstacle();
            ClearIntersectInfo();

            BakeUtil.BakeGrid(tm, MPU, stageOffset, UnitCount, out cells);
            BakeUtil.BakeObstacleArray<T>(GetComponentsInChildren<T>(), out aabbs, out obbs, out circles);
            BakeUtil.BakeIntersectInfo(aabbs?.ToArray(), obbs?.ToArray(), circles?.ToArray(), cells, cellCount, tm);

            EditorUtility.SetDirty(this);

            isBaked = true;

        }

        void ClearGrid() {

            tm.Clear();

            EditorUtility.SetDirty(this);

        }

        void ClearObstacle() {

            aabbs = null;
            obbs = null;
            circles = null;

            EditorUtility.SetDirty(this);

        }

        void ClearIntersectInfo() {

            if (cells == null) {
                return;
            }

            cells = null;

            EditorUtility.SetDirty(this);

        }

        void OnDrawGizmos() {

            if (!isBaked || tm.Passable == null) {
                return;
            }

            DrawGrid();

        }

        void DrawGrid() {

            for (int i = 0; i < cellCount.x; i++) {

                for (int j = 0; j < cellCount.y; j++) {

                    var index = new Vector2Int(i, j);
                    var walkable = tm.GetPassableValue(index);

                    if (walkable) {

                        Gizmos.color = Color.white;
                        var pos = BakeMathUtil.Index2GizmosCenter(index, cellSize, stageOffset);
                        Gizmos.DrawWireCube(pos, cellSize);

                    } else {

                        Gizmos.color = Color.red;
                        var pos = BakeMathUtil.Index2GizmosCenter(index, cellSize, stageOffset);
                        Gizmos.DrawWireCube(pos, cellSize);

                        var localMin = Vector2.zero - cellSize / 2;
                        var localMax = cellSize / 2;

                        var min = pos + localMin;
                        var max = pos + localMax;

                        Gizmos.DrawLine(min, max);
                        Gizmos.DrawLine(new Vector2(min.x, max.y), new Vector2(max.x, min.y));

                    }

                }

            }


        }


    }

}
