
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace HypixelSkyblockStuff.Items
{
    //corresponding summon item for blue whale pet
    public class BlueWhale : ModItem
    {

        public override void SetDefaults() //item data
        {
            Item.stack = 1; //max stack is 1
            Item.shoot = ModContent.ProjectileType<Projectiles.Pets.BlueWhale>(); //summon a blue whale pet
            Item.rare = ItemRarityID.Orange; //be orange
            Item.buffType = ModContent.BuffType<Buffs.BlueWhale>(); //give blue whale pet buff
            Item.useStyle = 4; //hold up
            Item.useAnimation = 30; //how long to do animation
            Item.useTime = 30; //how long to actually use
        }

        public override void AddRecipes() //recipe
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.SharkFin, 3); //ingredients
            recipe.AddIngredient(ItemID.Shrimp,2);
            recipe.AddIngredient(ItemID.Salmon, 5);
            recipe.AddTile(TileID.MythrilAnvil); //crafting tile
            recipe.Register();
        }

        //on click
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            player.AddBuff(Item.buffType, 3600, true); //give blue whale buff
            return false;
        }

    }
}