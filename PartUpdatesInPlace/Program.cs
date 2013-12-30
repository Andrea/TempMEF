using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Linq;
using Shared;

namespace PartUpdatesInPlace
{
    class Program
    {
        private static string ExtensionsPath = AppDomain.CurrentDomain.BaseDirectory + @"Extensions\";
        private static string BinDir = AppDomain.CurrentDomain.BaseDirectory;

        static void Cleanup()
        {
            foreach (var file in Directory.GetFiles(ExtensionsPath, "*.*"))
                File.Delete(file);

            File.Copy(BinDir+@"..\..\..\PartUpdatesInPlaceExtensions\bin\debug\PartUpdatesInPlaceExtensions.dll", ExtensionsPath + "Extensions1.dll");
        }

        static void Main(string[] args)
        {
            Cleanup();
            SetShadowCopy();

            var catalog = new AggregateCatalog();
            var directoryCatalog = new DirectoryCatalog(@".\Extensions");
            catalog.Catalogs.Add(directoryCatalog);
            var container = new CompositionContainer(catalog);
            
            //create barWatcher and add it to the container
            BarWatcher barWatcher = new BarWatcher();
            var batch = new CompositionBatch();
            batch.AddPart(barWatcher);
            container.Compose(batch);

            
            Console.WriteLine("Press enter to add a new bar");
            Console.ReadLine();
            //copy in a bar and refresh
            File.Copy(BinDir + @"..\..\..\PartUpdatesInPlaceExtensions2\bin\debug\PartUpdatesInPlaceExtensions2.dll", ExtensionsPath + "Extensions2.dll");
            directoryCatalog.Refresh();
            
            Console.WriteLine("Press enter to remove the first bar");
            Console.ReadLine();
            //delete the original bar and refresh
            File.Delete(ExtensionsPath + "Extensions1.dll");
            directoryCatalog.Refresh();

			Console.WriteLine("Press enter to add the original bar again");
			Console.ReadLine();
			//copy in a bar and refresh
			File.Copy(BinDir + @"..\..\..\PartUpdatesInPlaceExtensions\bin\debug\PartUpdatesInPlaceExtensions.dll", ExtensionsPath + "Extensions1.dll");
			directoryCatalog.Refresh();
			
			container.Compose(batch);
            Console.ReadLine();
        }

            private static void SetShadowCopy()
            {
	            
//				var setup = new AppDomainSetup
//				{
//					ApplicationBase = path,
//					ShadowCopyFiles = "true",
//					ShadowCopyDirectories = path
//				};
	            AppDomain.CurrentDomain.SetShadowCopyFiles();
                AppDomain.CurrentDomain.SetCachePath(@"C:\MEF\PartUpdatesInPlaceCache\");
            }
    }

	[PartCreationPolicy(CreationPolicy.NonShared)]
    [Export]
    public class BarWatcher : IPartImportsSatisfiedNotification
    {
		[ImportMany(AllowRecomposition = true)]
        public IEnumerable<IBar> Bars { get; set; }

        public void OnImportsSatisfied()
        {
            if (Bars.Any())
            {
                foreach (var bar in Bars)
                    Console.WriteLine(bar+" "+bar.Foo());
            }
            else
                Console.WriteLine("No Bars present");

            Console.WriteLine("\n");
        }
    }

	public interface IBarView
	{
		 string  Name { get;  }
	}

}
