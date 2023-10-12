using Terraria.ID;
using Terraria;
using Terraria.ModLoader;

namespace HypixelSkyblockStuff.Armor
{
    [AutoloadEquip(EquipType.Body)]
    //provides a barrier at full health that makes the player take no damage for 1 hit
    internal class SharkChestplate : ModItem
    {
        public override void SetDefaults() //item data
        {
            Item.defense = 10;
            Item.value = Item.sellPrice(0, 10, 0, 0);
            Item.rare = 4;
        }
        public override void UpdateEquip(Player player) //upon equip
        {
            player.GetDamage(DamageClass.Generic) += 0.05f; //increase damage by 5%
            player.GetCritChance(DamageClass.Generic) += 10; //increase crit chance by 10 
            player.GetModPlayer<GlobalPlayer>().SharkChestplate = true; //activate shark chestplate in global player to do the effect
            
        }

        public override void AddRecipes() //recipes
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.SharkFin, 5); //ingredients
            recipe.AddIngredient(ItemID.PalladiumBar, 15);
            recipe.AddIngredient(ItemID.PalmWood, 15);
            recipe.AddTile(ItemID.MythrilAnvil); //crafting tile
            recipe.Register();
            Recipe recipe2 = CreateRecipe();
            recipe2.AddIngredient(ItemID.SharkFin, 5);
            recipe2.AddIngredient(ItemID.CobaltBar, 15);
            recipe2.AddIngredient(ItemID.PalmWood, 15);
            recipe2.AddTile(ItemID.MythrilAnvil);
            recipe2.Register();
        }
    }
}
