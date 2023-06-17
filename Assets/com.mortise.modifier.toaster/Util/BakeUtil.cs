using System.Collections.Generic;
using MortiseFrame.Knot.Shape2D;
using MortiseFrame.Modifier.Toaster.Generic;
using UnityEngine;

namespace MortiseFrame.Modifier.Toaster.Util {

    public static class BakeUtil {

        public static void BakeGrid(ToasterGridTM tm, int MPU, Vector2 stageOffset, Vector2Int UnitCount, out AABB[] cells) {

            var cellSize = new Vector2(1 / (float)MPU, 1 / (float)MPU);
            var cellCount = new Vector2Int(UnitCount.x * MPU, UnitCount.y * MPU);

            tm.CellCount = cellCount;
            tm.CellSize = cellSize;
            tm.LocalOffset = stageOffset;
            tm.MPU = MPU;

            var passable = new bool[cellCount.x * cellCount.y];
            var capability = new int[cellCount.x * cellCount.y];
            tm.SetPassable(passable);
            tm.SetCapability(capability);
            cells = new AABB[cellCount.x * cellCount.y];
            Debug.Log($"cellCount: {cellCount.x}, {cellCount.y}, Length: {cells.Length}");

            for (int i = 0; i < cells.Length; i++) {

                var x = i % cellCount.x;
                var y = i / cellCount.x;
                var index = new Vector2Int(x, y);

                tm.SetPassableValue(index, true);
                tm.SetCapabilityValue(index, 1);

                var cell_aabb = BakeMathUtil.Index2AABB(index, cellSize, stageOffset);
                cells[i] = cell_aabb;

            }

            if (cells.Length == 0) {
                Debug.LogError("cells.Length == 0");
            }

        }

        public static void BakeIntersectInfo(AABB[] aabbs, OBB[] obbs, Circle[] circles, AABB[] cells, Vector2Int cellCount, ToasterGridTM tm) {

            ObstacleIntersectUtil.IntesctCircle_AABB(circles, cells, cellCount, (index) => {
                tm.SetPassableValue(index, false);
                Debug.Log($"intersect: x:{index.x}, y:{index.y}");

            });
            ObstacleIntersectUtil.IntesctAABB_AABB(aabbs, cells, cellCount, (index) => {
                tm.SetPassableValue(index, false);
                Debug.Log($"intersect: x:{index.x}, y:{index.y}");
            });
            ObstacleIntersectUtil.IntesctOBB_AABB(obbs, cells, cellCount, (index) => {
                tm.SetPassableValue(index, false);
                Debug.Log($"intersect: x:{index.x}, y:{index.y}");
            });

        }

        public static void BakeObstacleArray(GameObject[] elements, out AABB[] aabbs, out OBB[] obbs, out Circle[] circles) {

            aabbs = null;
            obbs = null;
            circles = null;

            var _aabbs = new List<AABB>();
            var _obbs = new List<OBB>();
            var _circles = new List<Circle>();

            if (elements == null || elements.Length == 0) {
                return;
            }

            for (int i = 0; i < elements.Length; i++) {

                var boxCol = elements[i].GetComponent<BoxCollider2D>();
                var circleCol = elements[i].GetComponent<CircleCollider2D>();

                Debug.Assert(boxCol == null || circleCol == null);

                if (boxCol == null && circleCol != null) {
                    var circle = BakeCollider2ShapeUtil.CircleCollider2Circle(circleCol);
                    _circles.Add(circle);
                }

                if (boxCol != null && circleCol == null && boxCol.transform.eulerAngles.z == 0) {
                    var aabb = BakeCollider2ShapeUtil.BoxCollider2AABB(boxCol);
                    _aabbs.Add(aabb);
                }

                if (boxCol != null && circleCol == null && boxCol.transform.eulerAngles.z != 0) {
                    var obb = BakeCollider2ShapeUtil.BoxCollider2OBB(boxCol);
                    _obbs.Add(obb);
                }

            }

            aabbs = _aabbs.ToArray();
            obbs = _obbs.ToArray();
            circles = _circles.ToArray();

        }

        public static void BakeCapability(bool enable, ToasterGridTM tm) {

            var cellCount = tm.CellCount;
            var cellSize = tm.CellSize;
            var stageOffset = tm.LocalOffset;
            var MPU = tm.MPU;

            // 1. 遍历每个格子
            // 2. 计算通行度
            // 3. 烘焙
            for (int x = 0; x < cellCount.x; x++) {
                for (int y = 0; y < cellCount.y; y++) {

                    if (enable == false) {
                        tm.SetCapabilityValue(new Vector2Int(x, y), 1);
                        continue;
                    } else {
                        var i = x + y * cellCount.x;
                        var index = new Vector2Int(x, y);
                        BakeCapabilityCalculateUtil.CalculateCapability(tm, index);
                    }

                }
            }

        }

    }

}