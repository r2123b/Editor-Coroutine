using System.Collections;
using System.Collections.Generic;
using UnityEditor;

[InitializeOnLoad]
public static class EditorCoroutine {
  /// <summary>
  ///  Coroutines to be executed.
  /// </summary>
  private static List<IEnumerator> CoroutineInProgress = new List<IEnumerator>();
  static int currentExecute = 0;

  static EditorCoroutine() {
    EditorApplication.update += ExecuteCoroutine;
  }

  // This is my callable function
  public static IEnumerator StartCoroutine(IEnumerator newCorou) {
    CoroutineInProgress.Add(newCorou);
    return newCorou;
  }

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
