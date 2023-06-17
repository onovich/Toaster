using UnityEngine;
using UnityEditor;
using MortiseFrame.Modifier.Toaster.Helper;

namespace MortiseFrame.Modifier.Toaster.Sample {

    public class ToasterSampleEditor : MonoBehaviour {

        // Output
        [Header("输出")] public ToasterSampleSO model;

        // Input
        [Header("覆盖单位数")] public Vector2Int UnitCount = new Vector2Int(10, 10);
        [Header("每单位网格数(n x n)")] public int MPU = 1;
        Vector2 localOffset => new Vector2(-UnitCount.x * 0.5f, -UnitCount.y * 0.5f);

        // Lock
        bool isBaked = false;

        // Capability
        [Header("通行度 ------------------------------------------------------------")]
        [Header("通行度计算")] public bool calculateCapability = true;
        [Header("最大允许通行度")] public int maxCapability = 10;

        // Visible
        [Header("可视化 ------------------------------------------------------------")]
        [Header("显示网格")] public bool showGrid = true;
        [Header("显示障碍物")] public bool showObstacle = true;
        [Header("显示通行度")] public bool showCapability = true;

        [ContextMenu("Bake")]
        void Bake() {

            isBaked = true;

            var group = transform.Find("editor_obstacle_group");
            var elements = group.transform.GetComponentsInChildren<ToasterSampleElement>();

            if (elements == null || elements.Length == 0) {
                Debug.Log("没有障碍物");
                return;
            }

            var goes = new GameObject[elements.Length];
            for (int i = 0; i < elements.Length; i++) {
                goes[i] = elements[i].gameObject;
            }

            var toaster = new Toaster(MPU, UnitCount, localOffset, goes, calculateCapability, maxCapability);
            var tm = toaster.Bake();

            model.tm = tm;

            EditorUtility.SetDirty(model);
            EditorUtility.SetDirty(this);
            AssetDatabase.SaveAssets();

        }

        [ContextMenu("Clear")]
        void Clear() {

            isBaked = false;

            model.tm.Clear();

            EditorUtility.SetDirty(model);
            EditorUtility.SetDirty(this);
            AssetDatabase.SaveAssets();

        }

        void OnDrawGizmos() {

            if (!isBaked) {
                return;
            }

            GizmosHelper.DrawGrid(showGrid, model.tm.CellSize, model.tm.CellCount, model.tm.LocalOffset, model.tm);
            GizmosHelper.DrawObstacle(showObstacle, model.tm.CellSize, model.tm.CellCount, model.tm.LocalOffset, model.tm);
            GizmosHelper.DrawCapability(showCapability, model.tm.CellSize, model.tm.CellCount, model.tm.LocalOffset, model.tm);

        }

    }

}