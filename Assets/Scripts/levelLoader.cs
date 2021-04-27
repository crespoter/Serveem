using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levelLoader : MonoBehaviour {

    private AsyncOperation async = null;
    public string LevelToLoad;
    void Start()
    {
        LevelToLoad = PlayerPrefs.GetString("level to load");
        StartCoroutine(LoadLevel(LevelToLoad));
    }

    private IEnumerator LoadLevel(string level)
    {
        async = Application.LoadLevelAsync(level);
        yield return async;
    }
}
