namespace LightHTML.Visitor
{
    public interface ILightNodeVisitor
    {
        void Visit(LightElementNode node);
        void Visit(LightTextNode node);
        void Visit(LightImageNode node);
    }
}