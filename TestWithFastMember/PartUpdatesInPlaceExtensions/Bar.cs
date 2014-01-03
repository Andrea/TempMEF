using System.ComponentModel.Composition;
using Shared;

namespace PartUpdatesInPlaceExtensions
{
    [Export(typeof(IBar))]
    public class Bar : IBar
    {
        public override string ToString()
        {
            return "Bar 1-" + typeof(Bar).Name +GetHashCode();
        }

	    public int Foo()
	    {
		    return typeof(Bar).GetHashCode();
	    }
    }
}
