using UnityEngine;

public class Bleed<T,V,S,H,J>:Effect<T,V,S,H,J> where T: EntityMover where V: EntityVisual where S:EntityStats<H,J> where H:EntityBaseStats where J:EntityBaseMoveStats 
{
    private float intervalTick;
    private BleedData bleedData;
    public Bleed(BleedData effectData,EntityController<T,V,S,H,J> entityController) :base(effectData,entityController)
    {
        intervalTick=effectData.intervalTick;
        bleedData=effectData;
    }
    public override void EntryEffect()
    {
        Debug.Log("Entra");

    }
    public override void ProcessEffect(float deltaTime)
    {
        base.ProcessEffect(deltaTime);
        intervalTick-=deltaTime;
        if (intervalTick <= 0)
        {
            entityController.stats.TakeDamage(bleedData.damage);
            intervalTick=bleedData.intervalTick;
        }
    }
    public override void ExitEffect()
    {
        Debug.Log("Saindo");
    }
    
}
