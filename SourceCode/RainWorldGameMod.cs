using UnityEngine;

namespace SelfDestructButton
{
    internal static class RainWorldGameMod
    {
        public static int[] deathCounter = new int[0];

        internal static void OnEnable()
        {
            On.RainWorldGame.ctor += RainWorldGame_ctor;
            On.RainWorldGame.Update += RainWorldGame_Update;
        }

        // ----------------- //
        // private functions //
        // ----------------- //

        private static void RainWorldGame_ctor(On.RainWorldGame.orig_ctor orig, RainWorldGame game, ProcessManager manager)
        {
            orig(game, manager);
            int playerCount = Mathf.Max(game.Players?.Count ?? 0, manager.arenaSitting?.players?.Count ?? 0);
            deathCounter = new int[playerCount];
        }

        private static void RainWorldGame_Update(On.RainWorldGame.orig_Update orig, RainWorldGame game)
        {
            orig(game);
            foreach (AbstractCreature abstractPlayer in game.Players)
            {
                if (abstractPlayer.state.alive && abstractPlayer.realizedCreature is Player player)
                {
                    if (player.dangerGraspTime > 0)
                    {
                        int playerNumber = player.playerState.playerNumber;
                        if (RWInput.PlayerInput(playerNumber, game.rainWorld.options, game.setupValues).pckp)
                        {
                            ++deathCounter[playerNumber];
                            if (deathCounter[playerNumber] > 40)
                            {
                                player.Die();
                            }
                        }
                        else if (deathCounter[playerNumber] > 0)
                        {
                            deathCounter[playerNumber] = 0;
                        }
                    }
                }
            }
        }
    }
}