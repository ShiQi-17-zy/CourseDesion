using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResManager : BesetMgr<ResManager>
{
    private enum LoadState
    {
        //空闲状态
        Idle,
        //加载状态
        LoadScene,
        //
        TickLoadSceneProgress,
    }

    private LoadState mCurrentLoadState = LoadState.Idle;
    private string mCurrentSceneName = null;
    private AsyncOperation mCurrentSceneAsyncOperation;

    public delegate void OnLoadCallBack();
    private OnLoadCallBack SceneLoadedCallback;


    public void Update()
    {
        switch(mCurrentLoadState)
        {
            case LoadState.Idle:
                break;
            case LoadState.LoadScene:
                SceneManager.sceneLoaded += SceneManager_sceneLoaded;
                mCurrentSceneAsyncOperation =SceneManager.LoadSceneAsync(mCurrentSceneName, LoadSceneMode.Single);
                if(mCurrentSceneAsyncOperation == null)
                {
                    Debug.LogError("Failed to load scene, mCurrentSceneAsyncOperation is null");
                    mCurrentLoadState= LoadState.Idle;
                    return;
                }
                mCurrentLoadState = LoadState.TickLoadSceneProgress;
                break;
            case LoadState.TickLoadSceneProgress:
                Debug.Log("Loading scene" + mCurrentSceneName + "process " + mCurrentSceneAsyncOperation.progress);
                break;
        }
    }

    //异步加载场景
    public void LoadSceneAsync(string name, OnLoadCallBack callback)
    {
        //判断当前是否正在加载场景
        if(mCurrentLoadState !=LoadState.Idle)
        {
            Debug.LogError("One scene is loading, scene name " + name);
            return;
        }

        mCurrentLoadState = LoadState.LoadScene;
        mCurrentSceneName = name;
        SceneLoadedCallback = callback;
    }

    //unity回调给我们的加载完成
    public void SceneManager_sceneLoaded(Scene scene,LoadSceneMode loadSceneMode)
    {
        SceneManager.sceneLoaded -= SceneManager_sceneLoaded;
        mCurrentLoadState = LoadState.Idle;

        if(SceneLoadedCallback !=null)
        {
            SceneLoadedCallback();
        }
    }
}
