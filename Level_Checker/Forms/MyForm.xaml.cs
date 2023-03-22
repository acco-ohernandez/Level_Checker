using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace Level_Checker
{
    /// <summary>
    /// Interaction logic for Window.xaml
    /// </summary>
    public partial class MyForm : Window
    {

        ExternalEvent myEvent;
        ExternalEvent myEvent_Reset;
        public MyForm(ExternalEvent _event, ExternalEvent _event_Reset, Document Doc)
        {
            InitializeComponent();

            //Create a List of Levels
            List<Level> levels = new List<Level>();

            //Get all Level Elements from the Document
            FilteredElementCollector levelCollector = new FilteredElementCollector(Doc).OfClass(typeof(Level));

            //Iterate through the Level Elements and add them to the List
            foreach (Level level in levelCollector)
            {
                levels.Add(level);
            }

            //Globals.levelsList.Clear();
            Globals.levelsList = levels;  // add the list of levels to the global list

            //Add the List of Levels to the ComboBox
            foreach (var level in levels)
            {
                cmb_LevelsList.Items.Add(level.Name);
            }


            myEvent = _event;
            myEvent_Reset = _event_Reset;
        }

        private void btn_Reset_Click(object sender, RoutedEventArgs e)
        {
            myEvent_Reset.Raise();
        }

        private void btn_Apply_Click(object sender, RoutedEventArgs e)
        {
            myEvent.Raise();
        }
    }

    
}
