using System;
using UnityEngine;

namespace MortiseFrame.Modifier.Toaster.Generic {

    [Serializable]
    public class ToasterGridTM {

        [SerializeField] bool[] passable;
        public bool[] Passable => passable;
        public void SetPassable(bool[] value) => passable = value;
        public void ClearPassable() => passable = null;

        [SerializeField] int[] capability;
        public int[] Capability => capability;
        public void SetCapability(int[] value) => capability = value;

        public Vector2Int CellCount;
        public Vector2 CellSize;
        public Vector2 LocalOffset;

        public int MPU;

        public bool GetPassableValue(Vector2Int index) {
            var x = index.x;
            var y = index.y;
            var i = x + y * CellCount.x;
            if (i >= passable.Length || i < 0) {
                Debug.LogError($"Index out of range");
            }
            return passable[x + y * CellCount.x];
        }

        public int GetCapabilityValue(Vector2Int index) {
            var x = index.x;
            var y = index.y;
            var i = x + y * CellCount.x;
            if (i >= passable.Length || i < 0) {
                Debug.LogError($"Index out of range");
            }
            return capability[x + y * CellCount.x];
        }

        public void SetPassableValue(Vector2Int index, bool value) {
            var x = index.x;
            var y = index.y;
            var i = x + y * CellCount.x;
            if (i >= passable.Length || i < 0) {
                Debug.LogError($"Index out of range: x = {x}, y = {y}, i = {i}, length = {passable.Length}");
            }
            passable[i] = value;
        }

        public void SetCapabilityValue(Vector2Int index, int value) {
            var x = index.x;
            var y = index.y;
            var i = x + y * CellCount.x;
            if (i >= passable.Length || i < 0) {
                Debug.LogError($"Index out of range: x = {x}, y = {y}, i = {i}, length = {passable.Length}");
            }
            capability[i] = value;
        }

        public void Clear() {
            passable = null;
            capability = null;
            CellCount = Vector2Int.zero;
        }

    }

}