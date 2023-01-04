namespace SelfDestructButton
{
    internal static class PlayerMod
    {
        internal static void OnEnable()
        {
            On.Player.ThrowToGetFree += Player_ThrowToGetFree;
        }

        // ----------------- //
        // private functions //
        // ----------------- //

        private static void Player_ThrowToGetFree(On.Player.orig_ThrowToGetFree orig, Player player, bool eu)
        {
            // don't release the player after being grabbed, i.e. remove the emergency throw mechanic
            if (!MainMod.Option_EmergencyThrow || player.dangerGraspTime == 0)
            {
                orig(player, eu);
            }
        }
    }
}