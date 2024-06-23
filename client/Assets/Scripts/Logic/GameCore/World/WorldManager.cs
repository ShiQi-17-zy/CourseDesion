using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEditor.Build.Content;
using UnityEngine;

public class WorldManager : BaseMgr<WorldManager>

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



    private long mObjectId;
    //��ʼ��
    public void Init()
    {
        mObjectId = 0;
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
                    LoadNpc();
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
        this.mState = state;
    }

    private void LoadMainPlayer()
    {
        Vector3 mainPlayerPos = new Vector3(63, 22.5f, 43);
        EntityMainPlayer mainPlayer = (EntityMainPlayer)EntityManager.Instance.CreateEntity(eEntityType.PLAYER_MAIN, GeneraterObjectID(), mainPlayerPos);

        mainPlayer.PlayerAnimation("WK_heavy_infantry_05_combat_idle");
        //mainPlayer.PlayAnimator("metarig|Walk");
        //GameObject mainPlayer = ResManager.Instance.InstantiateGameObject("Assets/Res/Role/Peasant Nolant Blue(Free Version).prefab");
        //if(mainPlayer == null)
        //{
        //    Debug.LogError("Load Main Player Error");
        //}
        //mainPlayer.transform.position = new Vector3(63.0f, 22.0f, 43.0f);
    }

    //���س����е�npc
    public void LoadNpc()
    {
        Vector3 npcPosition = new Vector3(56.3f, 22.23f, 43.8f);
        EntityNpc npc = (EntityNpc)EntityManager.Instance.CreateEntity(eEntityType.NPC, GeneraterObjectID(), npcPosition);
        npc.PlayAnimator("metarig|idle");
        npc.SetForward(new Vector3 (90,0,0));
        npc.SetName("��������");
    }

    //���������id
    private long GeneraterObjectID()
    {
        mObjectId++;
        return mObjectId;
    }
}
