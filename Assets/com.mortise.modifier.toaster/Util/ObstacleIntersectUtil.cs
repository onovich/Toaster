using UnityEngine;
using MortiseFrame.Knot;
using MortiseFrame.Knot.Shape2D;
using MortiseFrame.Modifier.Toaster.Generic;
using System;

namespace MortiseFrame.Modifier.Toaster.Util {

    public static class ObstacleIntersectUtil {

        public static void IntesctAABB_AABB(AABB[] aabbs, AABB[] cells, Vector2Int cellCount, Action<Vector2Int> OnIntersect) {

            if (aabbs == null || aabbs.Length == 0) {
                return;
            }

            for (int i = 0; i < aabbs.Length; i++) {

                var aabb = aabbs[i];

                for (int x = 0; x < cells.Length; x++) {

                    var cell = cells[x];
                    var intersect = Intersect2DUtil.IsIntersectAABB_AABB(cell, aabb, float.Epsilon);

                    if (intersect) {
                        var y = x / cellCount.x;
                        var index = new Vector2Int(x, y);
                        OnIntersect.Invoke(index);
                        //ToasterGridTM tm,
                        // tm.SetPassableValue(index, false);
                        // EditorUtility.SetDirty(model);
                    }

                }

            }

        }

        public static void IntesctOBB_AABB(OBB[] obbs, AABB[] cells, Vector2Int cellCount, Action<Vector2Int> OnIntersect) {

            if (obbs == null || obbs.Length == 0) {
                return;
            }

            for (int i = 0; i < obbs.Length; i++) {

                var obb = obbs[i];

                for (int x = 0; x < cells.Length; x++) {

                    var y = x / cellCount.x;
                    var cell_aabb = cells[x];
                    var intersect = Intersect2DUtil.IsIntersectAABB_OBB(cell_aabb, obb, float.Epsilon);

                    if (intersect) {
                        var index = new Vector2Int(x, y);
                        OnIntersect.Invoke(index);
                        // model.tm.SetWalkableValueWithIndex(index, false);

                    }

                }

            }

        }

        public static void IntesctCircle_AABB(Circle[] circles, AABB[] cells, Vector2Int cellCount, Action<Vector2Int> OnIntersect) {

            if (circles == null || circles.Length == 0) {
                return;
            }

            for (int i = 0; i < circles.Length; i++) {

                var obstacle_circle = circles[i];

                for (int x = 0; x < cells.Length; x++) {

                    var y = x / cellCount.x;
                    var cell_aabb = cells[x];
                    var intersect = Intersect2DUtil.IsIntersectAABB_Circle(cell_aabb, obstacle_circle, float.Epsilon);

                    if (intersect) {
                        var index = new Vector2Int(x, y);
                        OnIntersect.Invoke(index);
                        // model.tm.SetWalkableValueWithIndex(index, false);

                    }

                }

            }

        }

    }

}