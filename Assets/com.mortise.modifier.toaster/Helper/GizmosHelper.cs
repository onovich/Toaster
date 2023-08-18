#if UNITY_EDITOR

using UnityEngine;
using MortiseFrame.Modifier.Toaster.Generic;
using MortiseFrame.Modifier.Toaster.Util;

namespace MortiseFrame.Modifier.Toaster.Helper {

    public static class GizmosHelper {

        public static void DrawRealCapacity(bool enable, Vector2 cellSize, Vector2Int cellCount, Vector2 localOffset, ToasterGridTM tm, Color color) {

            if (!enable) {
                return;
            }

            for (int i = 0; i < cellCount.x; i++) {

                for (int j = 0; j < cellCount.y; j++) {

                    var index = new Vector2Int(i, j);
                    var capacity = tm.GetCapacityValue(index);
                    var mpu = tm.MPU;

                    Gizmos.color = color;
                    var pos = BakeMathUtil.Index2GizmosCenter(index, cellSize, localOffset);

                    var realCapacity = (int)capacity / mpu;

                    UnityEditor.Handles.Label(pos, realCapacity.ToString());

                }

            }

        }

        public static void DrawCapacityTest(bool enable, Vector2 cellSize, Vector2Int cellCount, Vector2 localOffset, ToasterGridTM tm, Color color) {

            if (!enable) {
                return;
            }

            for (int i = 0; i < cellCount.x; i++) {

                for (int j = 0; j < cellCount.y; j++) {

                    var index = new Vector2Int(i, j);
                    var capacity = tm.GetCapacityValue(index);

                    Gizmos.color = color;
                    var pos = BakeMathUtil.Index2GizmosCenter(index, cellSize, localOffset);

                    UnityEditor.Handles.Label(pos, capacity.ToString());

                }

            }

        }

        public static void DrawLargeGrid(bool enable, Vector2 cellSize, Vector2Int cellCount, Vector2 localOffset, ToasterGridTM tm, Color color) {

            if (!enable) {
                return;
            }

            var largeCellCount = cellCount / tm.MPU;
            var largeCellSize = cellSize * tm.MPU;

            for (int i = 0; i < largeCellCount.x; i++) {

                for (int j = 0; j < largeCellCount.y; j++) {

                    var index = new Vector2Int(i, j);
                    var walkable = tm.GetPassableValue(index);

                    if (!walkable) {
                        // continue;
                    }

                    Gizmos.color = color;
                    var pos = BakeMathUtil.Index2GizmosCenter(index, largeCellSize, localOffset);
                    Gizmos.DrawWireCube(pos, largeCellSize);

                }

            }

        }

        public static void DrawGrid(bool enable, Vector2 cellSize, Vector2Int cellCount, Vector2 localOffset, ToasterGridTM tm, Color color) {

            if (!enable) {
                return;
            }

            for (int i = 0; i < cellCount.x; i++) {

                for (int j = 0; j < cellCount.y; j++) {

                    var index = new Vector2Int(i, j);
                    var walkable = tm.GetPassableValue(index);

                    if (!walkable) {
                        // continue;
                    }

                    Gizmos.color = color;
                    var pos = BakeMathUtil.Index2GizmosCenter(index, cellSize, localOffset);
                    Gizmos.DrawWireCube(pos, cellSize);

                }

            }

        }

        public static void DrawObstacle(bool enable, Vector2 cellSize, Vector2Int cellCount, Vector2 localOffset, ToasterGridTM tm, Color color) {

            if (!enable) {
                return;
            }

            for (int i = 0; i < cellCount.x; i++) {

                for (int j = 0; j < cellCount.y; j++) {

                    var index = new Vector2Int(i, j);
                    var walkable = tm.GetPassableValue(index);

                    if (walkable) {
                        continue;
                    }

                    Gizmos.color = color;
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

#endif