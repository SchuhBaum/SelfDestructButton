namespace SelfDestructButton
{
    internal static class DropBugMod
    {
        internal static void OnEnable()
        {
            On.DropBug.Collide += DropBug_Collide;
        }

        // ----------------- //
        // private functions //
        // ----------------- //

        private static void DropBug_Collide(On.DropBug.orig_Collide orig, DropBug dropBug, PhysicalObject otherObject, int myChunk, int otherChunk)
        {
            if (otherObject is Player)
            {
                if (MainMod.Option_LowHealthInstaKill && dropBug.State?.health < 0.5f)
                {
                    dropBug.grabOnNextAttack = 0;
                }
                else if (MainMod.Option_DropBugBite)
                {
                    dropBug.grabOnNextAttack = 1;
                }
            }
            orig(dropBug, otherObject, myChunk, otherChunk);
        }
    }
}