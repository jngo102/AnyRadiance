namespace AnyRadiance
{
    internal class ArenaInfo
    {
        public const float A1Left = 48;
        public const float A1Right = 74;
        public const float A1CenterX = A1Left + (A1Right - A1Left) / 2;
        public const float A1Width = A1Right - A1Left;
        public const float A1Top = 50;
        public const float A1Bottom = 20;
        public const float A1Height = A1Top - A1Bottom;

        public const float A2Left = 40;
        public const float A2Right = 80;
        public const float A2CenterX = A2Left + (A2Right - A2Left) / 2;
        public const float A2Width = A2Right - A2Left;
        public const float A2Top = 57;
        public const float A2Bottom = 37;
        public const float A2Height = A2Top - A2Bottom;

        public static float CurrentLeft;
        public static float CurrentRight;
        public static float CurrentCenterX;
        public static float CurrentWidth;
        public static float CurrentTop;
        public static float CurrentBottom;
        public static float CurrentHeight;

        public static void SetPhase(int phase)
        {
            switch (phase)
            {
                case 1:
                    CurrentLeft = A1Left;
                    CurrentRight = A1Right;
                    CurrentCenterX = A1CenterX;
                    CurrentWidth = A1Width;
                    CurrentTop = A1Top;
                    CurrentBottom = A1Bottom;
                    CurrentHeight = A1Height;
                    break;
                case 2:
                    CurrentLeft = A2Left;
                    CurrentRight = A2Right;
                    CurrentCenterX = A2CenterX;
                    CurrentWidth = A2Width;
                    CurrentTop = A2Top;
                    CurrentBottom = A2Bottom;
                    CurrentHeight = A2Height;
                    break;
            }
        }
    }
}
