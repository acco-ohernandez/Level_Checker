#region Namespaces
//using Autodesk.Revit.Creation;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;

#endregion

namespace Level_Checker
{
    public class EventAction : IExternalEventHandler
    {
        public void Execute(UIApplication uiapp)
        {
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Document Doc = uidoc.Document;


            


            //List<ElementId> selectedElems =  uidoc.Selection.GetElementIds().ToList();
            //TaskDialog.Show("Test",$"You selected {selectedElems.Count} elements");


            //FilteredElementCollector collector = new FilteredElementCollector(Doc);
            //collector.OfCategory(BuiltInCategory.OST_TitleBlocks);

            //using (Transaction t = new Transaction(Doc))
            //{
            //    t.Start("Create new sheet");

            //    ViewSheet newSheet;


            //    newSheet = ViewSheet.Create(Doc, collector.FirstElementId());


            //    t.Commit();
            //}
        }

        public string GetName()
        {
            return "EventAction";
        }
    }

    public class EventAction_ResetAllElements : IExternalEventHandler
    {
        public void Execute(UIApplication uiapp)
        {
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Document Doc = uidoc.Document;

            // get all elements in view
            FilteredElementCollector collector = new FilteredElementCollector(Doc, Doc.ActiveView.Id);
            List<Element> viewElements = collector.Cast<Element>().ToList();

            OverrideGraphicSettings newSettings = new OverrideGraphicSettings();

            using (Transaction t = new Transaction(Doc))
            {
                t.Start("Rest elements");

                foreach (Element curElem in viewElements)
                {
                    Doc.ActiveView.SetElementOverrides(curElem.Id, newSettings);
                }

                t.Commit();
            }


        }

        public string GetName()
        {
            return "EventAction2";
        }
    }
}
