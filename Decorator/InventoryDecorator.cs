namespace Decorator
{
    public abstract class InventoryDecorator : IHero
    {
        protected readonly IHero _hero;

        protected InventoryDecorator(IHero hero) => _hero = hero;

        public virtual string Name => _hero.Name;
        public virtual int Health => _hero.Health;
        public virtual int Attack => _hero.Attack;
        public virtual int Defense => _hero.Defense;
        public virtual int MagicPower => _hero.MagicPower;

        public abstract string ItemName { get; }
        public abstract string ItemDescription { get; }

        public virtual string GetDescription() =>
            $"{_hero.GetDescription()}\n  + {ItemName}: {ItemDescription}";
    }
}