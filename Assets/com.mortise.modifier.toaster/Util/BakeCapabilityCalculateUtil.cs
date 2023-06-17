using MortiseFrame.Modifier.Toaster.Generic;
using UnityEngine;

namespace MortiseFrame.Modifier.Toaster.Util {

    public static class BakeCapabilityCalculateUtil {

        public static void CalculateCapability(ToasterGridTM tm, Vector2Int index) {

            // 0. 获取到当前坐标
            // 1. 基于当前坐标扩散，直到遇到障碍物或者边界
            // 2. 每成功扩散一次，capability + 1

            var cellCount = tm.CellCount;
            var isBlocked = false;

            // 迭代圈数
            for (int i = 0; i < 10; i++) {

                // 上边
                for (int j = -i; j <= i; j++) {

                    var checkIndex = index + new Vector2Int(j, i);

                    if (checkIndex.x >= cellCount.x || checkIndex.x < 0 || checkIndex.y >= cellCount.y || checkIndex.y < 0) {
                        isBlocked = true;
                        break;
                    }

                    var passable = tm.GetPassableValue(checkIndex);
                    if (!passable) {
                        isBlocked = true;
                        break;
                    }

                }

                if (isBlocked) {
                    tm.SetCapabilityValue(index, i);
                    return;
                }

                // 下边
                for (int j = -i; j <= i; j++) {

                    var checkIndex = index + new Vector2Int(j, -i);

                    if (checkIndex.x >= cellCount.x || checkIndex.x < 0 || checkIndex.y >= cellCount.y || checkIndex.y < 0) {
                        isBlocked = true;
                        break;
                    }

                    var passable = tm.GetPassableValue(checkIndex);
                    if (!passable) {
                        isBlocked = true;
                        break;
                    }

                }

                if (isBlocked) {
                    tm.SetCapabilityValue(index, i);
                    return;
                }

                // 左边
                for (int j = -i; j <= i; j++) {

                    var checkIndex = index + new Vector2Int(-i, j);

                    if (checkIndex.x >= cellCount.x || checkIndex.x < 0 || checkIndex.y >= cellCount.y || checkIndex.y < 0) {
                        isBlocked = true;
                        break;
                    }

                    var passable = tm.GetPassableValue(checkIndex);
                    if (!passable) {
                        isBlocked = true;
                        break;
                    }

                }

                if (isBlocked) {
                    tm.SetCapabilityValue(index, i);
                    return;
                }

                // 右边
                for (int j = -i; j <= i; j++) {

                    var checkIndex = index + new Vector2Int(i, j);

                    if (checkIndex.x >= cellCount.x || checkIndex.x < 0 || checkIndex.y >= cellCount.y || checkIndex.y < 0) {
                        isBlocked = true;
                        break;
                    }

                    var passable = tm.GetPassableValue(checkIndex);
                    if (!passable) {
                        isBlocked = true;
                        break;
                    }

                }

                if (isBlocked) {
                    tm.SetCapabilityValue(index, i);
                    return;
                }

            }

        }

    }

}