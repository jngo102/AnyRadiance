using System;

namespace AnyRadiance
{
    [Serializable]
    public class LocalSettings
    {
        public BossStatue.Completion Completion = new BossStatue.Completion
        {
            isUnlocked = true,
            hasBeenSeen = true,
        };
        public bool UsingAltVersion = false;
        public bool InBossDoor = false;
    }
}