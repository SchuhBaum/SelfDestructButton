using UnityEngine;

namespace SelfDestructButton
{
    internal static class CreatureMod
    {
        internal static void OnEnable()
        {
            On.Creature.Violence += Creature_Violence;
        }

        // ----------------- //
        // private functions //
        // ----------------- //

        private static void Creature_Violence(On.Creature.orig_Violence orig, Creature creature, BodyChunk source, Vector2? directionAndMomentum, BodyChunk hitChunk, PhysicalObject.Appendage.Pos hitAppendage, Creature.DamageType type, float damage, float stunBonus)
        {
            if (source != null && creature is Player player)
            {
                if (MainMod.Option_LowHealthInstaKill && source.owner is Creature sourceOwner && sourceOwner.State is HealthState healthState && healthState.health < 0.5f)
                {
                    Debug.Log("SelfDestructButton: sourceOwner " + sourceOwner);
                    Debug.Log("SelfDestructButton: Insta-kill player because bleeding out.");
                    damage = 2f;
                }
                else if (MainMod.Option_SpiderBite && source.owner is BigSpider bigSpider && !bigSpider.spitter)
                {
                    if (bigSpider.grasps?[0] is Creature.Grasp grasp && grasp.grabbedChunk.owner != player)
                    {
                        bigSpider.Grab(player, 0, hitChunk.index, Creature.Grasp.Shareability.CanNotShare, 0.5f, false, true);
                    }
                    damage = 0.0f;
                }
                else if (MainMod.Option_DropBugBite && source.owner is DropBug dropBug && dropBug.State?.health >= 0.0f) // they pretend to be dead when health is below zero // always let them deal damage in that case
                {
                    damage = 0.0f;
                }
            }
            orig(creature, source, directionAndMomentum, hitChunk, hitAppendage, type, damage, stunBonus);
        }
    }
}