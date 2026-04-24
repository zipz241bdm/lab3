namespace LightHTML.Command
{
    public interface ILightCommand
    {
        string Name { get; }

        void Execute();

        void Undo();
    }
}
