using System.ComponentModel.Composition;
using FastMember;
using Shared;

namespace PartUpdatesInPlaceExtensions2
{
    [Export(typeof(IBar))]
    public class Bar2 : IBar
    {
	    public string A { get; set; }
        public override string ToString()
        {
	        var aaa = "a a ab";
	        var obj = new Bar2 { A = aaa};

			var access = TypeAccessor.Create(typeof(Bar2));

			if(aaa== (string) access[obj, "A"])
				return "Bar 2 after accesor " + this.GetHashCode();
	        return "Bar 2 ";
        }

	    public int Foo()
	    {
		    return 2;
	    }
    }
}
