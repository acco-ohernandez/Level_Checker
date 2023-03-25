#region Namespaces
//using Autodesk.Revit.Creation;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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

            try
            {
                // Get the selectLevel name from the Globals variable
                string selectedLevel = Globals.selectedLevel;
                if (selectedLevel != null)
                {
                    List<Level> levelsList = Globals.levelsList;
                    // Find the element with the selectedLevel name
                    Level level = levelsList.FirstOrDefault(x => x.Name == selectedLevel);

                    // Get the element id for the selected Level
                    ElementLevelFilter filter = new ElementLevelFilter(level.Id);

                    List<ElementId> elementIDs = new List<ElementId>();
                    GetSelectedLevelElementsIDs(Doc, filter, elementIDs);

                    // Color the selected Elements
                    Color newColor = Globals.selectedColor;

                    if (newColor != null)
                    {
                        if (Globals.elemChkboxSelected)
                        {
                            OverrideGraphicSettings newSettings = new OverrideGraphicSettings();
                            newSettings.SetCutForegroundPatternColor(newColor);
                            newSettings.SetSurfaceForegroundPatternColor(newColor);

                            FillPatternElement curPatt = GetFillPatternByName(Doc, "<Solid fill>");
                            newSettings.SetCutForegroundPatternId(curPatt.Id);
                            newSettings.SetSurfaceForegroundPatternId(curPatt.Id);

                            using (Transaction t = new Transaction(Doc))
                            {
                                t.Start("Change element colors");

                                foreach (ElementId curId in elementIDs)
                                {
                                    Doc.ActiveView.SetElementOverrides(curId, newSettings);
                                }

                                t.Commit();
                            }
                            // Notify the user how many elements were updated
                            //TaskDialog.Show("Info", $"Updated {elementIDs.Count.ToString()} elements.");
                            Globals.lbl_InfoContent = $"Updated {elementIDs.Count.ToString()} elements.";
                        }
                        else
                        { Globals.lbl_InfoContent = "Select a Category CheckBox!"; }
                    }
                    else
                    { Globals.lbl_InfoContent = "Please Select a Color!"; }
                }
                else
                { Globals.lbl_InfoContent = "Please Select a Level!"; }
            }
            catch (Exception e)
            {

                Globals.lbl_InfoContent = "Error: Check DebugLog";
                Debug.Print(e.Message);
            }
        }

        private static void GetSelectedLevelElementsIDs(Document Doc, ElementLevelFilter filter, List<ElementId> elementIDs)
        {
            // if no checkbox is selected this global variable will remain false
            Globals.elemChkboxSelected = false;
            
            if (Globals.wallsIsChecked == true)
            {
                // Define a Filtered Element Collector
                FilteredElementCollector collector = new FilteredElementCollector(Doc);

                // Collect only the Walls on selected Level defined by the filter
                collector.WherePasses(filter).OfCategory(BuiltInCategory.OST_Walls);
                foreach (var elemId in collector)
                {
                    elementIDs.Add(elemId.Id);
                }
                Globals.elemChkboxSelected = true;
            }

            if (Globals.columnsIsChecked == true)
            {
                // Define a Filtered Element Collector
                FilteredElementCollector collector = new FilteredElementCollector(Doc);

                // Collect only the Columns on selected Level defined by the filter
                collector.WherePasses(filter).OfCategory(BuiltInCategory.OST_Columns);
                foreach (var elemId in collector)
                {
                    elementIDs.Add(elemId.Id);
                }
                Globals.elemChkboxSelected = true;
            }

            if (Globals.framingIsChecked == true)
            {
                // Define a Filtered Element Collector
                FilteredElementCollector collector = new FilteredElementCollector(Doc);

                // Collect only the StructuralFraming on selected Level defined by the filter
                collector.WherePasses(filter).OfCategory(BuiltInCategory.OST_StructuralFraming);
                foreach (var elemId in collector)
                {
                    elementIDs.Add(elemId.Id);
                }
                Globals.elemChkboxSelected = true;
            }

        }

        private FillPatternElement GetFillPatternByName(Document doc, string name)
        {
            FillPatternElement curFPE = null;

            curFPE = FillPatternElement.GetFillPatternElementByName(doc, FillPatternTarget.Drafting, name);

            return curFPE;
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
