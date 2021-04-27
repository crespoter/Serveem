using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class loader : MonoBehaviour {

    private AsyncOperation async = null;
    public string LevelToLoad;
	void Start () {
        StartCoroutine(LoadLevel(LevelToLoad));
	}
	
    private IEnumerator LoadLevel(string level)
    {
        async = Application.LoadLevelAsync(level);
        yield return async;
    }

}
