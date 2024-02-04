using Menu.Remix.MixedUI;
using UnityEngine;

namespace mehr1us.ids
{
    public class IDRangeCfg : OptionInterface
    {
        public static Configurable<int> StartingIdMax;
        public static Configurable<int> StartingIdMin;

        public IDRangeCfg() 
        {
            StartingIdMin = this.config.Bind<int>("StartingIdMin", 1000, new ConfigAcceptableRange<int>(int.MinValue + 1,int.MaxValue));
            StartingIdMax = this.config.Bind<int>("StartingIdMax", 100000, new ConfigAcceptableRange<int>(int.MinValue + 1, int.MaxValue));
        }

        public override void Initialize()
        {
            OpTab opTab = new OpTab(this, "Options");
            Tabs = new[]
            {
                opTab
            };

#nullable enable
            UIelement[]? uIelements = new UIelement[]
            {
                new OpLabel(10f, 550f, "Starting ID Min Value", true),
                new OpLabel(10f, 470f, "Starting ID Max Value", true),
                new OpUpdown(StartingIdMin, new Vector2(10f, 510f), 100f),
                new OpUpdown(StartingIdMax, new Vector2(10f, 430f), 100f)
            };
            opTab.AddItems(uIelements);
        }
    }
}