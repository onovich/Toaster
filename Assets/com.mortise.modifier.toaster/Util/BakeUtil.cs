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
            tm.StageOffset = stageOffset;
            tm.MPU = MPU;

            var grid = new bool[cellCount.x * cellCount.y];
            tm.SetPassable(grid);
            cells = new AABB[cellCount.x * cellCount.y];

            for (int x = 0; x < cells.Length; x++) {
                var y = cells.Length / cellCount.x;
                var index = new Vector2Int(x, y);
                var pos = BakeMathUtil.Index2GizmosCenter(index, cellSize, stageOffset);
                tm.SetPassableValue(index, true);
                var cell_aabb = BakeMathUtil.Index2AABB(index, cellSize, stageOffset);
                cells[cells.Length % cellCount.x] = cell_aabb;
            }

        }

        public static void BakeIntersectInfo(AABB[] aabbs, OBB[] obbs, Circle[] circles, AABB[] cells, Vector2Int cellCount, ToasterGridTM tm) {

            ObstacleIntersectUtil.IntesctCircle_AABB(circles, cells, cellCount, (index) => {
                tm.SetPassableValue(index, false);
            });
            ObstacleIntersectUtil.IntesctAABB_AABB(aabbs, cells, cellCount, (index) => {
                tm.SetPassableValue(index, false);
            });
            ObstacleIntersectUtil.IntesctOBB_AABB(obbs, cells, cellCount, (index) => {
                tm.SetPassableValue(index, false);
            });

        }

        public static void BakeObstacleArray<T>(T[] elements, out AABB[] aabbs, out OBB[] obbs, out Circle[] circles) where T : MonoBehaviour {

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

                Debug.Assert(boxCol == null && circleCol == null);

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


    }

}