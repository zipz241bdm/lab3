namespace Decorator.Weapon
{
    public class MagicStaff : InventoryDecorator
    {
        public MagicStaff(IHero hero) : base(hero) { }

        public override string ItemName => "Магічний посох";
        public override string ItemDescription => "+30 MGK, +10 ATK";
        public override int MagicPower => _hero.MagicPower + 30;
        public override int Attack => _hero.Attack + 10;
    }
}