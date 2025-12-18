#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

public class SceneMonoBehaviourScanner : EditorWindow
{
    Vector2 _scroll;
    List<System.Type> _types;
    Dictionary<string, List<MonoBehaviour>> _map;
    int _minCountFilter = 1;
    string _searchFilter = "";
    string _exportPath = "MonoBehaviourReport.csv";

    [MenuItem("Tools/Scene MonoBehaviour Scanner")]
    public static void ShowWindow()
    {
        var w = GetWindow<SceneMonoBehaviourScanner>("MB Scanner");
        w.Refresh();
    }

    void OnGUI()
    {
        EditorGUILayout.LabelField("Scene MonoBehaviour Scanner", EditorStyles.boldLabel);
        EditorGUILayout.Space();

        if (GUILayout.Button("Refresh"))
        {
            Refresh();
        }

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Min count filter:", GUILayout.Width(110));
        _minCountFilter = EditorGUILayout.IntField(_minCountFilter, GUILayout.Width(60));
        GUILayout.FlexibleSpace();
        EditorGUILayout.LabelField("Name filter:", GUILayout.Width(70));
        _searchFilter = EditorGUILayout.TextField(_searchFilter, GUILayout.Width(200));
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Space();

        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Export CSV"))
        {
            ExportCsv();
        }
        _exportPath = EditorGUILayout.TextField(_exportPath);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Space();

        if (_map == null || _map.Count == 0)
        {
            EditorGUILayout.HelpBox("No MonoBehaviours found in loaded scenes. Open a scene and press Refresh.", MessageType.Info);
            return;
        }

        EditorGUILayout.LabelField($"Total distinct MonoBehaviour types: {_map.Count}", EditorStyles.label);
        int totalInstances = _map.Values.Sum(list => list.Count);
        EditorGUILayout.LabelField($"Total MonoBehaviour instances (scene): {totalInstances}", EditorStyles.label);
        EditorGUILayout.Space();

        _scroll = EditorGUILayout.BeginScrollView(_scroll);
        // Order by descending instance count
        foreach (var kv in _map.OrderByDescending(k => k.Value.Count))
        {
            if (kv.Value.Count < _minCountFilter) continue;
            if (!string.IsNullOrEmpty(_searchFilter) && !kv.Key.ToLower().Contains(_searchFilter.ToLower())) continue;

            EditorGUILayout.BeginVertical("box");
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(kv.Key, EditorStyles.boldLabel, GUILayout.Width(300));
            EditorGUILayout.LabelField($"Instances: {kv.Value.Count}", GUILayout.Width(100));
            if (GUILayout.Button("Select All", GUILayout.Width(80)))
            {
                SelectAllOfType(kv.Value);
            }
            if (GUILayout.Button("Show Examples", GUILayout.Width(100)))
            {
                ShowExamples(kv.Value, 10);
            }
            EditorGUILayout.EndHorizontal();

            // Foldout of up to N entries (small preview)
            int showN = Mathf.Min(kv.Value.Count, 6);
            for (int i = 0; i < showN; i++)
            {
                var mb = kv.Value[i];
                if (mb == null) continue;
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField($"  {i + 1}. {mb.gameObject.name}", GUILayout.Width(300));
                EditorGUILayout.LabelField($"Path: {GetGameObjectPath(mb.gameObject)}", GUILayout.Width(600));
                if (GUILayout.Button("Ping", GUILayout.Width(60)))
                {
                    EditorGUIUtility.PingObject(mb.gameObject);
                }
                if (GUILayout.Button("Select", GUILayout.Width(60)))
                {
                    Selection.activeGameObject = mb.gameObject;
                }
                EditorGUILayout.EndHorizontal();
            }

            EditorGUILayout.EndVertical();
        }

        EditorGUILayout.EndScrollView();
    }

    void Refresh()
    {
        // Find MonoBehaviours in loaded scenes only
        var all = Resources.FindObjectsOfTypeAll<MonoBehaviour>()
                   .Where(m => m != null && m.gameObject != null && m.gameObject.scene.isLoaded)
                   .ToArray();

        _map = new Dictionary<string, List<MonoBehaviour>>();
        foreach (var mb in all)
        {
            string typeName = mb.GetType().Name;
            if (!_map.TryGetValue(typeName, out var list))
            {
                list = new List<MonoBehaviour>();
                _map[typeName] = list;
            }
            list.Add(mb);
        }
        _types = _map.Keys.Select(k => System.Type.GetType(k) ?? typeof(MonoBehaviour)).ToList();
        Repaint();
    }

    void SelectAllOfType(List<MonoBehaviour> list)
    {
        var objs = list.Where(m => m != null).Select(m => m.gameObject).Distinct().ToArray();
        Selection.objects = objs;
        EditorGUIUtility.PingObject(objs.FirstOrDefault());
    }

    void ShowExamples(List<MonoBehaviour> list, int maxCount)
    {
        // open inspector on the first few (select them)
        var objs = list.Take(maxCount).Where(m => m != null).Select(m => m.gameObject).ToArray();
        Selection.objects = objs;
        if (objs.Length > 0) EditorGUIUtility.PingObject(objs[0]);
    }

    string GetGameObjectPath(GameObject go)
    {
        string path = go.name;
        var t = go.transform;
        while (t.parent != null)
        {
            t = t.parent;
            path = t.name + "/" + path;
        }
        return path;
    }

    void ExportCsv()
    {
        if (_map == null) { EditorUtility.DisplayDialog("Export", "No data to export. Press Refresh first.", "OK"); return; }

        var sb = new StringBuilder();
        sb.AppendLine("Type,Instances,ExamplePaths(semi-colon separated)");

        foreach (var kv in _map.OrderByDescending(k => k.Value.Count))
        {
            if (kv.Value.Count < _minCountFilter) continue;
            if (!string.IsNullOrEmpty(_searchFilter) && !kv.Key.ToLower().Contains(_searchFilter.ToLower())) continue;

            var paths = kv.Value.Take(6).Where(m => m != null).Select(m => GetGameObjectPath(m.gameObject));
            string joined = string.Join(" | ", paths);
            sb.AppendLine($"{kv.Key},{kv.Value.Count},\"{joined}\"");
        }

        string full = Path.Combine(Application.dataPath, _exportPath);
        try
        {
            File.WriteAllText(full, sb.ToString());
            EditorUtility.DisplayDialog("Exported", $"CSV exported to:\n{full}", "OK");
            Debug.Log($"MonoBehaviour report exported to: {full}");
        }
        catch (System.Exception ex)
        {
            EditorUtility.DisplayDialog("Error", "Failed to write CSV: " + ex.Message, "OK");
        }
    }
}
#endif
