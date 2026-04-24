using System.Collections;

namespace LightHTML.Iterator
{
    public interface ILightIterator : IEnumerator<LightNode>
    {
        object IEnumerator.Current => Current;
    }
}
