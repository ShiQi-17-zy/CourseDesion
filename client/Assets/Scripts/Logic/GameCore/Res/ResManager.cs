using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//��Դ�����࣬ʵ�ֵ���ģʽ
public class ResManager : BesetMgr<ResManager>
{
    private enum LoadState
    {
        //����״̬
        Idle,
        //����״̬
        LoadScene,
        //������״̬
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
                //ͨ���ص���ʽ�������ǣ������������
                SceneManager.sceneLoaded += SceneManager_sceneLoaded;

                //ͨ���첽��ʽ���س���
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
                Debug.Log("Loading scene " + mCurrentSceneName + " process " + mCurrentSceneAsyncOperation.progress);
                break;
        }
    }

    //�첽���س���
    public void LoadSceneAsync(string name, OnLoadCallBack callback)
    {
        //�жϵ�ǰ�Ƿ����ڼ��س���
        if(mCurrentLoadState != LoadState.Idle)
        {
            Debug.LogError("One scene is loading, scene name " + name);
            return;
        }

        mCurrentLoadState = LoadState.LoadScene;
        mCurrentSceneName = name;
        SceneLoadedCallback = callback;
    }

    ////
    //public void LoadScene(string name)
    //{
    //    //ͬ�����س���
    //    SceneManager.LoadScene(name);
    //    LoadPlayer();
    //}


    private void LoadPlayer()
    {

    }
    //unity�ص������ǵļ������
    public void SceneManager_sceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        SceneManager.sceneLoaded -= SceneManager_sceneLoaded;
        mCurrentLoadState = LoadState.Idle;

        if(SceneLoadedCallback != null)
        {
            SceneLoadedCallback();
        }
    }

    //������Դ
    public Object LoadResource(string resPath)
    {
#if UNITY_EDITOR
        //ֻ����unity�� editor �µ���Դ���ط�ʽ,ֻ�ǴӴ��̼��ص��ڴ浱��ȥ
        Object obj = UnityEditor.AssetDatabase.LoadAssetAtPath<Object>(resPath);
        return obj;
#else
//�����ļ��ط�ʽ
#endif
    }

    //ʵ������ʾһ����Դ
    public GameObject InstantiateGameObject(string resPath)
    {
        GameObject obj = LoadResource(resPath) as GameObject;
        if (obj != null)
        {
            //ʵ������Դ
            GameObject go = GameObject.Instantiate(obj);
            if (go == null)
            {
                Debug.LogError("game instantiate failed" + resPath);
                return null;
            }

            //��ʾ��Դ
            go.SetActive(true);
            return go;
        }
        else
        {
            return null;
        }
    }
}
