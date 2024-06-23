using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameStart : MonoBehaviour 
{
    //����Ϸ�����ڼ�ʼ�ձ�����GameObject
    private GameObject mGo;

    //��Ϸ�������е�λ��
    void Start()
    {
        Debug.Log("Game Start");
        mGo = gameObject;

        //������������ʱ�����ٸ�����
        DontDestroyOnLoad(mGo);

        try
        {
            //���������ʼ��
            WorldManager.Instance.Init();
            EntityManager.Instance.Init();
        }
        catch (Exception e)
        {
            Debug.LogException(e);
        }

        //WorldManager.Instance.LoadScene("rpgpp_lt_scene_1.0");
    }



    //��Ϸѭ��,�Թ̶�Ƶ�ʸ���
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

    //��Update�����
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

    //�Թ̶�Ƶ�ʸ���
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

    //��Ϸ�˳�
    //�����ǣ��˳���Ϸʱ������Դ
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
