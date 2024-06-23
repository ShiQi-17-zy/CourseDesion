using System.Collections;
using System.Collections.Generic;
using UnityEngine;



//ʵ�崴�������࣬���ڴ�����ͬ���͵�ʵ��
public class EntityFactory : BaseMgr<EntityFactory>
{
    public Entity CreateEntity(eEntityType entityType, long entityID)
    {
        Entity entity = null;
        switch (entityType)
        {
            case eEntityType.PLAYER_MAIN:
                entity = new EntityMainPlayer();
                break;
            case eEntityType.PLAYER:
                break;
            case eEntityType.NPC:
                entity = new EntityNpc();
                break;
            default:
                Debug.LogError("failed to create entity type " + entityType.ToString());
                break;
        }
        entity.SetID(entityID);

        return entity;
    }
}
