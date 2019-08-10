using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameInput;
using Terraria;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;

namespace RODHTDS
{
    public class ROTHTDSPlayer : ModPlayer
    {
        public override void ProcessTriggers(TriggersSet triggersSet)
        {
            if ((RODHTDS.safeRodKey.JustPressed || RODHTDS.unsafeRodKey.JustPressed) && player.itemTime <= 0)
            {
                Item item = null;
                for (int i = 0; i < 58; i++)
                {
                    item = player.inventory[i];
                    if (item.type == ItemID.RodofDiscord)
                    {
                        break;
                    }
                    else
                    {
                        item = null;
                    }
                }
                if (item == null || !CheckValidWarpLocation(Main.MouseWorld))
                    return;
                if (RODHTDS.safeRodKey.JustPressed)
                {
                    if (player.HasBuff(BuffID.ChaosState))
                        return;
                    player.Teleport(Main.MouseWorld, 1);
                    player.AddBuff(BuffID.ChaosState, 360);
                }
                else if (RODHTDS.unsafeRodKey.JustPressed)
                {
                    player.Teleport(Main.MouseWorld, 1);
                    player.AddBuff(BuffID.ChaosState, 360);
                    if (player.HasBuff(BuffID.ChaosState))
                        OofOuchOwie(player);
                }
            }
        }

        public bool CheckValidWarpLocation(Vector2 position)
        {
            Vector2 mousePos = default;
            mousePos.X = Main.mouseX + Main.screenPosition.X;
            if (player.gravDir == 1f)
            {
                mousePos.Y = Main.mouseY + Main.screenPosition.Y - player.height;
            }
            else
            {
                mousePos.Y = Main.screenPosition.Y + Main.screenHeight - Main.mouseY;
            }
            ref float x = ref mousePos.X;
            x -= player.width / 2;
            if (mousePos.X > 50f && mousePos.X < Main.maxTilesX * 16 - 50 && mousePos.Y > 50f && mousePos.Y < Main.maxTilesY * 16 - 50)
            {
                int integerX = (int)(mousePos.X / 16f);
                int integerY = (int)(mousePos.Y / 16f);
                if (Main.tile[integerX, integerY].wall != 87 && !Collision.SolidCollision(position, player.width, player.height))
                {
                    player.itemTime = 30; // This only fires if it's teleporting, so this means it's a valid warp - teleport us!
                    return true;
                }
            }
            return false;
        }

        public void OofOuchOwie(Player player)
        {

            player.statLife -= player.statLifeMax2 / 7;
            PlayerDeathReason damageSource = PlayerDeathReason.ByOther(13);
            if (Main.rand.Next(2) == 0)
            {
                damageSource = PlayerDeathReason.ByOther(player.Male ? 14 : 15);
            }
            if (player.statLife <= 0)
            {
                player.KillMe(damageSource, 1.0, 0, false);
            }
            player.lifeRegenCount = 0;
            player.lifeRegenTime = 0;
        }
    }
}