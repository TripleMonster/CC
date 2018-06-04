using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingPanel : MonoBehaviour {
    [SerializeField] private Slider _loadingSlider;

    enum LoadingStep { LOADING_START, LOADING_CONFIG, LOADING_ATLAS, LOADING_PREFAB, LOADING_AUDIO, LOADING_END }
    LoadingStep curStep;
    bool isStartLoading = true;

	void Start () 
	{
        curStep = LoadingStep.LOADING_START;
	}

	void Update () 
	{
        if (isStartLoading)
        {
            isStartLoading = false;
            StartCoroutine(Loading());
        }
	}

    IEnumerator Loading()
    {
        LoadingByStep(curStep);
        yield return new WaitForSeconds(2.0f);
        isStartLoading = true;
    }

    void LoadingByStep(LoadingStep step)
    {
        switch (step)
        {
            case LoadingStep.LOADING_START:
                {
                    curStep = LoadingStep.LOADING_CONFIG;
                }
                break;
            case LoadingStep.LOADING_CONFIG:
                {
                    curStep = LoadingStep.LOADING_ATLAS;
                    //LoadingConfigs();
                }
                break;
            case LoadingStep.LOADING_ATLAS:
                {
                    curStep = LoadingStep.LOADING_PREFAB;
                    //LoadingAtlases();
                }
                break;
            case LoadingStep.LOADING_PREFAB:
                {
                    curStep = LoadingStep.LOADING_AUDIO;
                    _loadingSlider.value += 0.2f;
                }
                break;
            case LoadingStep.LOADING_AUDIO:
                {
                    curStep = LoadingStep.LOADING_END;
                    _loadingSlider.value += 0.2f;
                }
                break;
            case LoadingStep.LOADING_END:
                {
                    isStartLoading = false;
                    AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("SampleScene");
                }
                break;
        }      
    }
}
