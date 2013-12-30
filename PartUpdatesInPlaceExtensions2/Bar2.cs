using System.ComponentModel.Composition;
using Shared;

namespace PartUpdatesInPlaceExtensions2
{
    [Export(typeof(IBar))]
    public class Bar2 : IBar
    {
        public override string ToString()
        {
            return "Bar 2 - " + this.GetHashCode();
        }

	    public int Foo()
	    {
		    return 2;
	    }
    }
}
