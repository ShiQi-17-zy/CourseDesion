using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStart : MonoBehaviour
{
    //����Ϸ�����ڼ�ʼ�ձ�����GameObject
    private GameObject mGo;

    //��Ϸ�������е�λ��
    void Start()
    {
        mGo = gameObject;

        //������������ʱ�����ٸ�����
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

    //��Ϸѭ��
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

    //��Ϸ�˳�
    //�����ǣ��˳���Ϸʱ������Դ
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
