using UnityEngine;
using UnityEditor;
using System.Collections;

public class TestBundleFileWindow : EditorWindow {
  string bunldeName;
  public static float progress = -1;

  [MenuItem("Test/Bundle File")]
  static void Init() {
    TestBundleFileWindow window = (TestBundleFileWindow)GetWindow(typeof(TestBundleFileWindow));
    window.Show();
  }

  void OnGUI() {
    bunldeName = EditorGUILayout.TextField("Bundle Name:", bunldeName);

    EditorGUILayout.Space();
    if (GUILayout.Button("Build Bundle")) {
      // init progress bar's values
      progress = 0;

      // do heavy IO process without blocking the display of progressbar 
      EditorCoroutine.StartCoroutine(myHeavyJob());
    }
    DisplayProgressBar();
  }

  private void DisplayProgressBar() {
    if (progress >= 0 && progress < 1)
      EditorUtility.DisplayProgressBar("Loading Files...", string.Format("{0:00} %", progress * 100), progress);
    else {
      EditorUtility.ClearProgressBar();
      progress = -1;
    }
  }

  private IEnumerator myHeavyJob() {
    // do your Heavy Job here
    // e.g. load many textrues or process many files
    yield return myHeavyTasks();
  }

  // to refresh the progress bar window
  void OnInspectorUpdate() {
    Repaint();
  }
}
