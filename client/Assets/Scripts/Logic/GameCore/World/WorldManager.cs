using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEditor.Build.Content;
using UnityEngine;

public class WorldManager : BaseMgr<WorldManager>

    //单例类
{
    //场景状态机
    enum LoadState
    {
        //初始化状态
        Init,
        //加载场景状态
        LoadScene,
        //更新状态
        Update,
        //等待状态
        Wait,
    }
    private LoadState mState;
    private string mloadSceneName;



    private long mObjectId;
    //初始化
    public void Init()
    {
        mObjectId = 0;
        EnterState(LoadState.Init);
    }

    //世界更新
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
                    //等等场景加载完成后，加载玩家到场景中
                    LoadMainPlayer();
                    LoadNpc();
                });
            //ResManger.Instance.LoadScene(mLoadSceneName);

        }
    }

    //世界管理中的加载场景
    public void LoadScene(string name)
    {
        mloadSceneName= name;

        EnterState(LoadState.LoadScene);
    }

    //改变当前的状态机
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

    //加载场景中的npc
    public void LoadNpc()
    {
        Vector3 npcPosition = new Vector3(56.3f, 22.23f, 43.8f);
        EntityNpc npc = (EntityNpc)EntityManager.Instance.CreateEntity(eEntityType.NPC, GeneraterObjectID(), npcPosition);
        npc.PlayAnimator("metarig|idle");
        npc.SetForward(new Vector3 (90,0,0));
        npc.SetName("神秘商人");
    }

    //生成物体的id
    private long GeneraterObjectID()
    {
        mObjectId++;
        return mObjectId;
    }
}
