using Terraria.ModLoader;

namespace RODHTDS
{
	public class RODHTDS : Mod
    {
        public static ModHotKey safeRodKey;
        public static ModHotKey unsafeRodKey;
        public RODHTDS()
        {
            Properties = new ModProperties()
            {
                Autoload = true,
                AutoloadGores = true,
                AutoloadSounds = true,
                AutoloadBackgrounds = true
            };
        }

        public override void Load()
        {
            safeRodKey = RegisterHotKey("Safe Teleport", "Y");
            unsafeRodKey = RegisterHotKey("Unsafe Teleport", "G");
        }
    }
}