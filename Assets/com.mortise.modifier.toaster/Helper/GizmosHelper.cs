using UnityEngine;
using MortiseFrame.Modifier.Toaster.Generic;
using MortiseFrame.Modifier.Toaster.Util;

namespace MortiseFrame.Modifier.Toaster.Helper {

    public static class GizmosHelper {

        public static void DrawGrid(Vector2 cellSize, Vector2Int cellCount, Vector2 localOffset, ToasterGridTM tm) {

            for (int i = 0; i < cellCount.x; i++) {

                for (int j = 0; j < cellCount.y; j++) {

                    var index = new Vector2Int(i, j);
                    var walkable = tm.GetPassableValue(index);

                    if (walkable) {

                        Gizmos.color = Color.white;
                        var pos = BakeMathUtil.Index2GizmosCenter(index, cellSize, localOffset);
                        Gizmos.DrawWireCube(pos, cellSize);

                    } else {

                        Gizmos.color = Color.red;
                        var pos = BakeMathUtil.Index2GizmosCenter(index, cellSize, localOffset);
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