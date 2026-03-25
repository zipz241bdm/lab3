namespace Decorator.Armor 
{
    public class MageRobe : InventoryDecorator
    {
        public MageRobe(IHero hero) : base(hero) { }

        public override string ItemName => "Мантія мага";
        public override string ItemDescription => "+20 HP, +15 MGK";
        public override int Health => _hero.Health + 20;
        public override int MagicPower => _hero.MagicPower + 15;
    }
}