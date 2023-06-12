using Terraria.ID;
using Terraria;
using Terraria.ModLoader;

namespace HypixelSkyblockStuff.Armor
{
    [AutoloadEquip(EquipType.Head)]
    //prevents player from dying and gives temporary strength boost
    internal class BonzoMask : ModItem
    {
        public override void SetStaticDefaults() //tooltip and name
        {
            Tooltip.SetDefault("10% damage\n10% crit chance\nSaves the player from death and gives temporary damage boost");
            DisplayName.SetDefault("Bonzo Mask");
        }
        public override void SetDefaults() //item data
        {
            Item.defense = 10;
            Item.value = Item.sellPrice(0, 2, 0, 0);
            Item.rare = ItemRarityID.Orange;
            Item.crit = 10; 
            
        }
        

        public override void UpdateEquip(Player player) //upon equip
        {
            player.GetDamage(DamageClass.Generic) += 0.1f; //increase damage by 10%
            player.GetCritChance(DamageClass.Generic) += 5; //increase crit chance by 5
            player.GetModPlayer<GlobalPlayer>().BonzoMask = true; //enable bonzo mask in global player to prevent death
        }


    }
}
