#if UNITY_EDITOR

using System.Linq;
using UnityEngine;
using MortiseFrame.Knot.Shape2D;
using MortiseFrame.Modifier.Toaster.Util;
using MortiseFrame.Modifier.Toaster.Generic;

namespace MortiseFrame.Modifier.Toaster {

    public class Toaster {

        // Cell
        Vector2Int cellCount;
        Vector2 cellSize;

        // Unit
        int MPU = 1;
        Vector2Int UnitCount = new Vector2Int(10, 10);

        // Grid
        AABB[] cells;

        // Obstacle
        GameObject[] obstacles;
        AABB[] aabbs;
        OBB[] obbs;
        Circle[] circles;

        // Offset
        Vector2 offset;

        // Output
        ToasterGridTM tm;

        // Capibility
        bool needCapacity = true;
        int maxCapacity = 10;

        public Toaster(int MPU, Vector2Int UnitCount, Vector2 offset, GameObject[] obstacles, bool needCapacity, int maxCapacity) {

            this.tm = new ToasterGridTM();
            this.MPU = MPU;
            this.UnitCount = UnitCount;
            this.offset = offset;
            this.obstacles = obstacles;
            this.needCapacity = needCapacity;
            this.maxCapacity = maxCapacity;

            if (MPU <= 0) {
                throw new System.Exception("MPU must be greater than 0");
            }

            this.cellSize = new Vector2(1 / (float)MPU, 1 / (float)MPU);
            this.cellCount = new Vector2Int(UnitCount.x * MPU, UnitCount.y * MPU);

            if (obstacles == null || obstacles.Length == 0) {
                throw new System.Exception("Obstacles is null or empty");
            }

        }

        public ToasterGridTM Bake() {

            ClearGrid();
            ClearObstacle();
            ClearIntersectInfo();

            BakeUtil.BakeGrid(tm, MPU, offset, UnitCount, out cells);
            BakeUtil.BakeObstacleArray(obstacles, out aabbs, out obbs, out circles);
            BakeUtil.BakeIntersectInfo(aabbs.ToArray(), obbs.ToArray(), circles.ToArray(), cells, cellCount, tm);
            BakeUtil.BakeCapacity(needCapacity, tm, maxCapacity);

            return tm;

        }

        void ClearGrid() {

            tm.Clear();

        }

        void ClearObstacle() {

            aabbs = null;
            obbs = null;
            circles = null;

        }

        void ClearIntersectInfo() {

            if (cells == null) {
                return;
            }

            cells = null;

        }

    }

}

#endif