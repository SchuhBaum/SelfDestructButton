using UnityEngine;

namespace SelfDestructButton
{
    internal static class LizardMod
    {
        internal static void OnEnable()
        {
            On.Lizard.Bite += Lizard_Bite;
        }

        // ----------------- //
        // private functions //
        // ----------------- //

        private static void Lizard_Bite(On.Lizard.orig_Bite orig, Lizard lizard, BodyChunk chunk)
        {
            if (chunk.owner is Player)
            {
                float biteDamageChance = lizard.lizardParams.biteDamageChance;
                if (MainMod.Option_LowHealthInstaKill && lizard.LizardState?.health < 0.5f)
                {
                    Debug.Log("SelfDestructButton: lizard " + lizard);
                    Debug.Log("SelfDestructButton: Insta-kill player because health is " + lizard.LizardState.health + ".");
                    lizard.lizardParams.biteDamageChance = 1f;
                }
                else if (MainMod.Option_RedLizards && lizard.Template.type == CreatureTemplate.Type.RedLizard)
                {
                    lizard.lizardParams.biteDamageChance = 1f;
                }
                else if (MainMod.Option_LizardBite)
                {
                    lizard.lizardParams.biteDamageChance = 0.0f;
                }

                orig(lizard, chunk);
                lizard.lizardParams.biteDamageChance = biteDamageChance;
            }
            else
            {
                orig(lizard, chunk);
            }
        }
    }
}