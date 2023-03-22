#region Namespaces
//using Autodesk.Revit.Creation;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;

#endregion

namespace Level_Checker
{
    public class EventAction : IExternalEventHandler
    {
        public void Execute(UIApplication uiapp)
        {
            Document Doc = uiapp.ActiveUIDocument.Document;

            FilteredElementCollector collector = new FilteredElementCollector(Doc);
            collector.OfCategory(BuiltInCategory.OST_TitleBlocks);

            using (Transaction t = new Transaction(Doc))
            {
                t.Start("Create new sheet");

                ViewSheet newSheet;


                newSheet = ViewSheet.Create(Doc, collector.FirstElementId());


                t.Commit();
            }
        }

        public string GetName()
        {
            return "EventAction";
        }
    }
}
