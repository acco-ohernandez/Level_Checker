#region Namespaces
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
//using Autodesk.Revit.Creation;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Windows.Controls;

#endregion

namespace Level_Checker
{
    [Transaction(TransactionMode.Manual)]
    public class Command : IExternalCommand
    {
        public Result Execute(
          ExternalCommandData commandData,
          ref string message,
          ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Application app = uiapp.Application;
            Document doc = uidoc.Document;

            // put any code needed for the form here
            EventAction myAction = new EventAction();
            ExternalEvent myEvent = ExternalEvent.Create(myAction);

            EventAction_ResetAllElements myAction_Reset = new EventAction_ResetAllElements();
            ExternalEvent myEvent_Reset = ExternalEvent.Create(myAction_Reset);


            // open form
            MyForm currentForm = new MyForm(myEvent, myEvent_Reset, doc)
            {
                Width = 500,
                Height = 550,
                WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen,
                Topmost = true,
            };



            //currentForm.ShowDialog();
            currentForm.Show();

            // get form data and do something

            return Result.Succeeded;
        }

        public static String GetMethod()
        {
            var method = MethodBase.GetCurrentMethod().DeclaringType?.FullName;
            return method;
        }
    }
}
