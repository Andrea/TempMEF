using System.ComponentModel.Composition;
using Shared;

namespace PartUpdatesInPlaceExtensions
{
    [Export(typeof(IBar))]
	[ExportMetadata("Name", "Bar1")]
    public class Bar : IBar
    {
        public override string ToString()
        {
            return "Bar 1" + this.GetHashCode();
        }

	    public int Foo()
	    {
		    return 1;
	    }
    }
}
