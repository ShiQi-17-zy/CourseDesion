using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class WorldManager : BesetMgr<WorldManager>

    //������
{
    //����״̬��
    enum LoadState
    {
        //��ʼ��״̬
        Init,
        //���س���״̬
        LoadScene,
        //����״̬
        Update,
        //�ȴ�״̬
        Wait,
    }
    private LoadState mState;
    private string mloadSceneName;

    //��ʼ��
    public void Init()
    {
        EnterState(LoadState.Init);
    }

    //�������
    public void Update()
    {
        if (mState == LoadState.Init)
        {

        }
        if (mState == LoadState.LoadScene)
        {
            
            EnterState(LoadState.Wait);
            ResManager.Instance.LoadSceneAsync(mloadSceneName, () =>
                {
                    //�ȵȳ���������ɺ󣬼�����ҵ�������
                    LoadMainPlayer();
                });
            //ResManger.Instance.LoadScene(mLoadSceneName);

        }
    }

    //��������еļ��س���
    public void LoadScene(string name)
    {
        mloadSceneName= name;

        EnterState(LoadState.LoadScene);
    }

    //�ı䵱ǰ��״̬��
    private void EnterState(LoadState state)
    {
        mState = state;
    }

    private void LoadMainPlayer()
    {
        GameObject mainPlayer = ResManager.Instance.InstantiateGameObject("Assets/Res/Role/Peasant Nolant Blue(Free Version).prefab");
        if(mainPlayer == null)
        {
            Debug.LogError("Load Main Player Error");
        }
        mainPlayer.transform.position = new Vector3(63.0f, 22.0f, 43.0f);
    }
}
