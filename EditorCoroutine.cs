using System.Collections;
using System.Collections.Generic;
using UnityEditor;

[InitializeOnLoad]
public static class EditorCoroutine {
  // Constructor
  static EditorCoroutine() {
    EditorApplication.update += ExecuteCoroutine;
  }
  
  // This is the entry function
  public static IEnumerator StartCoroutine(IEnumerator newCorou) {
    CoroutineInProgress.Add(newCorou);
    return newCorou;
  }

  /// <summary>
  ///  Coroutine to execute. Manage by the EasyLocalization_script
  /// </summary>
  private static List<IEnumerator> CoroutineInProgress = new List<IEnumerator>();

  static int currentExecute = 0;
  private static void ExecuteCoroutine() {
    if (CoroutineInProgress.Count <= 0) {
      //  Debug.LogWarning("ping");
      return;
    }
    // Debug.LogWarning("execute coroutine...");

    currentExecute = (currentExecute + 1) % CoroutineInProgress.Count;
    bool finish = !CoroutineInProgress[currentExecute].MoveNext();
    if (finish)
      CoroutineInProgress.RemoveAt(currentExecute);
  }
}
