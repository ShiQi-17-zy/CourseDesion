using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameStart : MonoBehaviour 
{
    //在游戏运行期间始终保留的GameObject
    private GameObject mGo;

    //游戏启动运行的位置
    void Start()
    {
        Debug.Log("Game Start");
        mGo = gameObject;

        //加载其他场景时不销毁该物体
        DontDestroyOnLoad(mGo);

        try
        {
            //场景世界初始化
            WorldManager.Instance.Init();
            EntityManager.Instance.Init();
        }
        catch (Exception e)
        {
            Debug.LogException(e);
        }

        //WorldManager.Instance.LoadScene("rpgpp_lt_scene_1.0");
    }



    //游戏循环,以固定频率更新
    void Update()
    {
        try
        {
            ResManager.Instance.Update();
            WorldManager.Instance.Update();
            EntityManager.Instance.Update();
        }
        catch (Exception e)
        {
            Debug.LogException(e);
        }
    }

    //在Update后更新
    private void LateUpdate()
    {
        try
        {
            EntityManager.Instance.LateUpdate();
        }
        catch (Exception e)
        {
            Debug.LogException(e);
        }
    }

    //以固定频率更新
    private void FixedUpdate()
    {
        try
        {

        }
        catch (Exception e)
        {
            Debug.LogException(e);
        }
    }

    //游戏退出
    //作用是，退出游戏时销毁资源
    private void OnApplicationQuit()
    {
        Debug.Log("Game Quit");

        try
        {
            EntityManager.Instance.Exit();
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
    }
}
