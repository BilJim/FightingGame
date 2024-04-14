using UnityGameFramework.Runtime;

public static class EntityExtension
{
    public static void HideEntity(this EntityComponent entityComponent, Entity entity)
    {
        entityComponent.HideEntity(entity.Entity);
    }
}