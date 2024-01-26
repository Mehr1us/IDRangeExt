using System;
using System.Security.Permissions;
using System.Security;
using BepInEx;
using BepInEx.Logging;


#pragma warning disable CS0618 // Type or member is obsolete
[assembly: SecurityPermission(SecurityAction.RequestMinimum, SkipVerification = true)]
[module: UnverifiableCode]
#pragma warning restore CS0618 // Type or member is obsolete

namespace mehr1us.ids
{
    [BepInPlugin("mehr1us.ids", "IdRangeExtender", "1.0")]
    public class Plugin : BaseUnityPlugin
    {
        public static IDRangeCfg cfg;
        public static bool initialized = false;
        public static ManualLogSource logger;

        public void OnEnable()
        {
            logger = base.Logger;
            try
            {
                Plugin.cfg = new IDRangeCfg();
                IDHooks.Apply();
            } catch (Exception e)
            {
                logger.LogError($"[IDRangeExt]  Exception at RainWorldGame.ctor:\n{e}");
            }
            On.RainWorld.OnModsInit += RainWorld_OnModsInit;
        }

        private void RainWorld_OnModsInit(On.RainWorld.orig_OnModsInit orig, RainWorld self)
        {
            orig(self);
            if (!initialized)
            {
                try
                {
                    initialized = true;
                    global::MachineConnector.SetRegisteredOI("mehr1us.ids", Plugin.cfg);
                    logger.LogDebug("Applied Range Extender");
                }
                catch (Exception e)
                {
                    logger.LogError($"[IDRangeExt]  Exception at RainWorld.OnModsInit:\n{e}");
                }
            }
        }
    }
}