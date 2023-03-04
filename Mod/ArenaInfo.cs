using System;
using System.Linq;
using System.Reflection;

namespace AnyRadiance
{
    internal static class ArenaInfo
    {
        private const float A1Left = 48;
        private const float A1Right = 74;
        private const float A1CenterX = A1Left + (A1Right - A1Left) / 2;
        private const float A1Width = A1Right - A1Left;
        private const float A1Top = 50;
        private const float A1Bottom = 20;
        private const float A1Height = A1Top - A1Bottom;
        
        private const float A2Left = 40;
        private const float A2Right = 80;
        private const float A2CenterX = A2Left + (A2Right - A2Left) / 2;
        private const float A2Width = A2Right - A2Left;
        private const float A2Top = 57;
        private const float A2Bottom = 37;
        private const float A2Height = A2Top - A2Bottom;

        public static float CurrentLeft;
        public static float CurrentRight;
        public static float CurrentCenterX;
        public static float CurrentWidth;
        public static float CurrentTop;
        public static float CurrentBottom;
        public static float CurrentHeight;

        public static void SetPhase(byte phase)
        {
            foreach (var fi in typeof(ArenaInfo).GetFields(BindingFlags.Public | BindingFlags.Static).Where(fi => fi.Name.StartsWith("Current")))
            {
                try
                {
                    fi.SetValue(null,
                        typeof(ArenaInfo).GetField($"A{phase}{fi.Name.Substring(7)}",
                            BindingFlags.NonPublic | BindingFlags.Static)?.GetValue(null));
                }
                catch (MissingFieldException e)
                {
                    AnyRadiance.Instance.LogError($"{e}: Phase {phase} does not exist!");
                    throw;
                }
            }
        }
    }
}
