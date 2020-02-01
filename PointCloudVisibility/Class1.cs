using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace PointCloudVisibility
{
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]

    public class PCVis:IExternalCommand
    {
        
        
        
            static AddInId appId = new AddInId(new Guid("C6A695BF-6E88-40C4-9F76-C232DAC7F668"));
            public Autodesk.Revit.UI.Result Execute(Autodesk.Revit.UI.ExternalCommandData commandData,
                                                   ref string message,
                                                   ElementSet elements)
            {

                Document doc = commandData.Application.ActiveUIDocument.Document;
            

                FilteredElementCollector collector = new FilteredElementCollector(doc).OfClass(typeof(PointCloudInstance));
                ICollection<ElementId> coll = collector.ToElementIds();



                Element el = collector.FirstElement();


                Category category = el.Category;
                BuiltInCategory enumCategory = (BuiltInCategory)category.Id.IntegerValue;

                bool b = doc.ActiveView.GetCategoryHidden(category.Id);

            if (!el.IsHidden(doc.ActiveView))
            {
                using (Transaction t = new Transaction(doc, "Irnes, thank you (PC OFF)"))
                {
                    t.Start();

                    doc.ActiveView.HideElementsTemporary(coll);
                    doc.ActiveView.ConvertTemporaryHideIsolateToPermanent();


                    t.Commit();
                }
            }
            else

            {

                using (Transaction t = new Transaction(doc, "Irnes, thank you (PC ON)"))
                {
                    t.Start();
                    doc.ActiveView.UnhideElements(coll);
                    t.Commit();
                }

            }

            return Result.Succeeded;
            }


     }





}

