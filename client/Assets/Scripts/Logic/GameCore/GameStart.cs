using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStart : MonoBehaviour
{
    //在游戏运行期间始终保留的GameObject
    private GameObject mGo;

    //游戏启动运行的位置
    void Start()
    {
        mGo = gameObject;

        //加载其他场景时不销毁该物体
        DontDestroyOnLoad(mGo);

        try
        {

        }
        catch (Exception e)
        {
            Debug.LogException(e);
        }

        ResManager.Instance
    }

    //游戏循环
    void Update()
    {
        try
        {
            ResManager.Instance.Update();
        }
        catch(Exception e)
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

        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
    }
}
