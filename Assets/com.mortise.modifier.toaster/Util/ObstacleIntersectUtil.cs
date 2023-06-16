using UnityEngine;
using MortiseFrame.Knot;
using MortiseFrame.Knot.Shape2D;
using MortiseFrame.Modifier.Toaster.Generic;
using System;

namespace MortiseFrame.Modifier.Toaster.Util {

    public static class ObstacleIntersectUtil {

        public static void IntesctAABB_AABB(AABB[] aabbs, AABB[] cells, Vector2Int cellCount, Action<Vector2Int> OnIntersect) {

            if (aabbs == null || aabbs.Length == 0) {
                Debug.Log("aabbs is null");
                return;
            }

            for (int j = 0; j < aabbs.Length; j++) {

                var aabb = aabbs[j];
                if (aabb == null) {
                    Debug.LogError("aabb is null");
                }

                if (cells == null) {
                    Debug.LogError("cells is null");
                }

                for (int i = 0; i < cells.Length; i++) {

                    var cell = cells[i];
                    if (cell == null) {
                        Debug.LogError($"cell is null: {i}");
                    }
                    var intersect = Intersect2DUtil.IsIntersectAABB_AABB(cell, aabb, float.Epsilon);

                    if (intersect) {

                        var x = i % cellCount.x;
                        var y = i / cellCount.x;
                        var index = new Vector2Int(x, y);
                        OnIntersect.Invoke(index);
                        Debug.Log($"intersect: x:{x}, y:{y}");

                    }

                }

            }

        }

        public static void IntesctOBB_AABB(OBB[] obbs, AABB[] cells, Vector2Int cellCount, Action<Vector2Int> OnIntersect) {

            if (obbs == null || obbs.Length == 0) {
                Debug.Log("obbs is null");
                return;
            }

            for (int j = 0; j < obbs.Length; j++) {

                var obb = obbs[j];

                for (int i = 0; i < cells.Length; i++) {

                    var cell_aabb = cells[i];
                    var intersect = Intersect2DUtil.IsIntersectAABB_OBB(cell_aabb, obb, float.Epsilon);

                    if (intersect) {

                        var x = i % cellCount.x;
                        var y = i / cellCount.x;
                        var index = new Vector2Int(x, y);
                        OnIntersect.Invoke(index);
                        Debug.Log($"intersect: x:{x}, y:{y}");

                    }

                }

            }

        }

        public static void IntesctCircle_AABB(Circle[] circles, AABB[] cells, Vector2Int cellCount, Action<Vector2Int> OnIntersect) {

            if (circles == null || circles.Length == 0) {
                Debug.Log("circles is null");
                return;
            }

            for (int j = 0; j < circles.Length; j++) {

                var obstacle_circle = circles[j];

                for (int i = 0; i < cells.Length; i++) {

                    var cell_aabb = cells[i];
                    var intersect = Intersect2DUtil.IsIntersectAABB_Circle(cell_aabb, obstacle_circle, float.Epsilon);

                    if (intersect) {

                        var x = i % cellCount.x;
                        var y = i / cellCount.x;
                        var index = new Vector2Int(x, y);
                        OnIntersect.Invoke(index);
                        Debug.Log($"intersect: x:{x}, y:{y}");

                    }

                }

            }

        }

    }

}