using System.Collections.Generic;
using UnityEngine;

public class SampleScriptManager : MonoBehaviour { 
    private List<SampleScript> scripts = new List<SampleScript>();

    private void Awake()
    {
        scripts.AddRange(FindObjectsByType<SampleScript>(FindObjectsSortMode.None));
    }

    [ContextMenu("Начать выполнение")]
    public void ExecuteAll()
    {
        foreach (var script in scripts)
        {
            script.Use();
        }
    }
}
