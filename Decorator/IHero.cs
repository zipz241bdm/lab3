namespace Decorator
{
    public interface IHero
    {
        string Name { get; }
        int Health { get; }
        int Attack { get; }
        int Defense { get; }
        int MagicPower { get; }
        string GetDescription();
    }
}