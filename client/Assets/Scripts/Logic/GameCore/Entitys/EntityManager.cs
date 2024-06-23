using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityManager : BaseMgr<EntityManager>
{
    //�洢�����Ѿ�����������ʵ��
    private Dictionary<long, Entity> mEntitys = new Dictionary<long, Entity>();

    //��ʼ��
    public void Init()
    {

    }

    //�߼�����
    public void Update()
    {
        float deltaTime = Time.deltaTime;
        foreach(var entity in mEntitys)
        {
            entity.Value.OnUpdate(deltaTime);
        }
    }

    //�Ӻ����
    public void LateUpdate()
    {

    }

    public void Exit()
    {

    }

    //����ʵ��
    public Entity CreateEntity(eEntityType entityType, long entityID, Vector3 pos)
    {
        //�ж�ID�Ƿ���ڣ��������ʲôҲ����
        if(ExistEntity(entityID))
        {
            Debug.LogError("create entity error, entity id " + entityID + "exist");
            return null;
        }
        Entity entity = EntityFactory.Instance.CreateEntity(entityType, entityID);
        AddEntity(entity);


        entity.SetPosition(pos);
        return entity;
    }

    private void AddEntity (Entity entity)
    {
        entity.OnCreate();
        mEntitys.Add(entity.GetID(), entity);
    }

    //�ж�һ��id��ʵ���Ƿ����
    private bool ExistEntity(long entityID)
    {
        if(mEntitys.ContainsKey(entityID))
        {
            return true;
        }
        return false;
    }
}
