using MortiseFrame.Knot.Shape2D;
using UnityEngine;

namespace MortiseFrame.Modifier.Toaster.Util {

    public static class BakeCollider2ShapeUtil {

        public static Circle CircleCollider2Circle(CircleCollider2D collider) {

            var offset = new Vector3(collider.offset.x, collider.offset.y, 0);
            var center = collider.transform.position + offset;
            var radius = collider.radius;
            radius *= collider.transform.localScale.x;
            var circle = new Circle(center, radius);
            return circle;

        }

        public static OBB BoxCollider2OBB(BoxCollider2D collider) {

            var offset = new Vector3(collider.offset.x, collider.offset.y, 0);
            var size = collider.size;
            size.x *= collider.transform.localScale.x;
            size.y *= collider.transform.localScale.y;
            var center = collider.transform.position + offset;
            var angle = collider.transform.eulerAngles.z * Mathf.Deg2Rad;
            var obb = new OBB(center, size, angle);
            return obb;

        }

        public static AABB BoxCollider2AABB(BoxCollider2D collider) {

            var offset = collider.offset;
            var size = collider.size;
            size.x *= collider.transform.localScale.x;
            size.y *= collider.transform.localScale.y;
            var xMin = collider.transform.position.x - size.x / 2 + offset.x;
            var xMax = collider.transform.position.x + size.x / 2 + offset.x;
            var yMin = collider.transform.position.y - size.y / 2 + offset.y;
            var yMax = collider.transform.position.y + size.y / 2 + offset.y;
            var min = new Vector2(xMin, yMin);
            var max = new Vector2(xMax, yMax);
            var aabb = new AABB(min, max);

            return aabb;

        }

    }

}
