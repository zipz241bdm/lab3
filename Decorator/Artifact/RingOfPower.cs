namespace Decorator.Artifact
{
    public class RingOfPower : InventoryDecorator
    {
        public RingOfPower(IHero hero) : base(hero) { }

        public override string ItemName => "Перстень сили";
        public override string ItemDescription => "+10 ATK, +5 DEF";
        public override int Attack => _hero.Attack + 10;
        public override int Defense => _hero.Defense + 5;
    }
}