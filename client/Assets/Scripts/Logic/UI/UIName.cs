using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIName : MonoBehaviour
{
    public Text Name;

    private string mName;

    void Update()
    {
        //����UI�ĳ���Ϊ������ĳ���
        transform.forward= Camera.main.transform.forward;


        Name.text = mName; 
    }

    //��������
    public void SetName(string name)
    {
        mName = name;
    }
}
