namespace Decorator.Weapon
{
    public class HolyMace : InventoryDecorator
    {
        public HolyMace(IHero hero) : base(hero) { }

        public override string ItemName => "Священна булава";
        public override string ItemDescription => "+20 ATK, +10 MGK";
        public override int Attack => _hero.Attack + 20;
        public override int MagicPower => _hero.MagicPower + 10;
    }
}