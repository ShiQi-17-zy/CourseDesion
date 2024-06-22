using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//ʵ�ֵ���ģ����
public class BesetMgr<T> where T : class,new()
{
    private static T mInstance = null;
    public static T Instance
    {
        get
        {
            if(mInstance == null)
            {
                mInstance = new T();
            }

            return mInstance;
        }
    }
}
