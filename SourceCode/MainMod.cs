using UnityEngine;

namespace SelfDestructButton
{
    public class MainMod : Partiality.Modloader.PartialityMod
    {
        public string updateURL = "http://beestuff.pythonanywhere.com/audb/api/mods/8/3";
        public int version = 4;
        public string keyE = "AQAB";
        public string keyN = "0Sb8AUUh0jkFOuNDGJti4jL0iTB4Oug0pM8opATxJH8hfAt6FW3//Q4wb4VfTHZVP3+zHMX6pxcqjdvN0wt/0SWyccfoFhx2LupmT3asV4UDPBdQNmDeA/XMfwmwYb23yxp0apq3kVJNJ3v1SExvo+EPQP4/74JueNBiYshKysRK1InJfkrO1pe1WxtcE7uIrRBVwIgegSVAJDm4PRCODWEp533RxA4FZjq8Hc4UP0Pa0LxlYlSI+jJ+hUrdoA6wd+c/R+lRqN2bjY9OE/OktAxqgthEkSXTtmZwFkCjds0RCqZTnzxfJLN7IheyZ69ptzcB6Zl7kFTEofv4uDjCYNic52/C8uarj+hl4O0yU4xpzdxhG9Tq9SAeNu7h6Dt4Impbr3dAonyVwOhA/HNIz8TUjXldRs0THcZumJ/ZvCHO3qSh7xKS/D7CWuwuY5jWzYZpyy14WOK55vnEFS0GmTwjR+zZtSUy2Y7m8hklllqHZNqRYejoORxTK4UkL4GFOk/uLZKVtOfDODwERWz3ns/eOlReeUaCG1Tole7GhvoZkSMyby/81k3Fh16Z55JD+j1HzUCaoKmT10OOmLF7muV7RV2ZWG0uzvN2oUfr5HSN3TveNw7JQPd5DvZ56whr5ExLMS7Gs6fFBesmkgAwcPTkU5pFpIjgbyk07lDI81k=";

        public static MainMod? instance;

        public static bool Option_LowHealthInstaKill = true;
        public static bool Option_DropBugBite = true;
        public static bool Option_EmergencyThrow = true;

        public static bool Option_LizardBite = true;
        public static bool Option_RedLizards = true;
        public static bool Option_SpiderBite = true;

        public static OptionalUI.OptionInterface LoadOI() // requires ConfigMachine.dll
        {
            return new MainModOptions();
        }

        public MainMod()
        {
            ModID = "SelfDestructButton";
            Version = "0.33";
            author = "SchuhBaum";
            instance = this;
        }

        public override void OnEnable()
        {
            base.OnEnable();
            On.RainWorld.Start += RainWorld_Start;
        }

        // ----------------- //
        // private functions //
        // ----------------- //

        private void RainWorld_Start(On.RainWorld.orig_Start orig, RainWorld rainWorld)
        {
            Debug.Log("SelfDestructButton: Version " + Version);
            orig(rainWorld);
            MainModOptions.inGameTranslator = rainWorld.inGameTranslator;

            CreatureMod.OnEnable();
            DropBugMod.OnEnable();
            LizardMod.OnEnable();
            PlayerMod.OnEnable();
            RainWorldGameMod.OnEnable();
        }
    }
}