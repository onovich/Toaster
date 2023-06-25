using MortiseFrame.Knot.Shape2D;
using UnityEngine;

namespace MortiseFrame.Modifier.Toaster.Util {

    public static class BakeMathUtil {

        public static AABB Index2AABB(Vector2Int index, Vector2 cellSize, Vector2 stageOffset) {

            var offset = cellSize / 2;
            var center = new Vector2(index.x * cellSize.x, index.y * cellSize.y) + offset + stageOffset;

            var xMin = center.x - cellSize.x / 2;
            var xMax = center.x + cellSize.x / 2;
            var yMin = center.y - cellSize.y / 2;
            var yMax = center.y + cellSize.y / 2;
            var min = new Vector2(xMin, yMin);
            var max = new Vector2(xMax, yMax);
            var cell_aabb = new AABB(min, max);
            return cell_aabb;

        }

        public static Vector2 Index2GizmosCenter(Vector2Int index, Vector2 cellSize, Vector2 stageOffset) {

            var cell_offset = cellSize / 2;
            var pos = new Vector2(index.x * cellSize.x, index.y * cellSize.y) + cell_offset + stageOffset;

            return pos;

        }
    }

}