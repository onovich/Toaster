using MortiseFrame.Knot.Shape2D;
using UnityEngine;

namespace MortiseFrame.Modifier.Toaster.Util {

    public static class BakeMathUtil {

        public static AABB Index2AABB(Vector2Int index, Vector2 cellSize, Vector2 stageOffset) {

            var offset = cellSize / 2;
            var x = index.x;
            var y = index.y;

            var xMin = x * cellSize.x + stageOffset.x;
            var xMax = x * cellSize.x + offset.x * 2 + stageOffset.x;
            var yMin = y * cellSize.y + stageOffset.y;
            var yMax = y * cellSize.y + offset.y * 2 + stageOffset.y;
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