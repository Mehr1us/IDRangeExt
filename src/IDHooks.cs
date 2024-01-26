using MonoMod.Cil;
using System;
using System.Reflection;
using Mono.Cecil.Cil;

namespace mehr1us.ids
{
    public class IDHooks
    {
        public static int StartingIdMin = IDRangeCfg.StartingIdMin.Value;
        public static int StartingIdMax = IDRangeCfg.StartingIdMax.Value;

        public static void Apply()
        {
            IL.RainWorldGame.ctor += RainWorldGame_ctor;
        }

        private static void RainWorldGame_ctor(ILContext il)
        {
            ILCursor c = new(il);
            try
            {
                if (!c.TryGotoNext(
                    x => x.Match(OpCodes.Ldc_I4),
                    x => x.Match(OpCodes.Ldc_I4),
                    x => x.Match(OpCodes.Call),
                    x => x.MatchStfld("RainWorldGame", "nextIssuedId")
                    ))
                {
                    Plugin.logger.LogError("Failed Hooking RainWorldGame.ctor");
                    return;
                }
                c.Emit(OpCodes.Call, typeof(IDHooks).GetMethod("UpdateStatics", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static));
                c.Emit(OpCodes.Ldsfld, typeof(IDHooks).GetField("StartingIdMin", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static));
                c.Emit(OpCodes.Ldsfld, typeof(IDHooks).GetField("StartingIdMax", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static));
                c.Remove();
                c.Remove();
            }
            catch (Exception e)
            {
                Plugin.logger.LogError(e.ToString());
            }
        }

        public static void UpdateStatics()
        {
            StartingIdMin = IDRangeCfg.StartingIdMin.Value;
            StartingIdMax = IDRangeCfg.StartingIdMax.Value;
        }
    }
}