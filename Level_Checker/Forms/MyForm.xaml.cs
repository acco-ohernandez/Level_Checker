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

        ExternalEvent ExternalEvent_ElementColorChange;
        ExternalEvent ExternalEvent_Reset;
        
        public MyForm(ExternalEvent _event, ExternalEvent _event_Reset, Document Doc)
        {
            
            InitializeComponent();
            
            // add the events to the form global variables
            ExternalEvent_ElementColorChange = _event;
            ExternalEvent_Reset = _event_Reset;

            // Add the all the Levels to the ComboBox and add them to the Globals.levelsList variable 
            AddLevelNamesToComboBox(Doc);
        }

        private void AddLevelNamesToComboBox(Document Doc)
        {
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
        }

        private void btn_Reset_Click(object sender, RoutedEventArgs e)
        {
            // Reset All Element colors event
            ExternalEvent_Reset.Raise();
        }

        private void btn_Apply_Click(object sender, RoutedEventArgs e)
        {
            // Update the Globals.selectedLevel variable
            if (cmb_LevelsList.SelectedItem != null) { Globals.selectedLevel = cmb_LevelsList.SelectedItem as string; }else { Globals.selectedLevel = null; }

            // Update the Globals.selectedColor variable
            if (rdl_Red.IsChecked == true)         Globals.selectedColor = new Autodesk.Revit.DB.Color(255, 0, 0);//"Red";
            else if (rdl_Green.IsChecked == true)  Globals.selectedColor = new Autodesk.Revit.DB.Color(0, 255, 0);//"Green";
            else if (rdl_Blue.IsChecked == true)   Globals.selectedColor = new Autodesk.Revit.DB.Color(0, 0, 255);//"Blue";
            else if (rdl_Yellow.IsChecked == true) Globals.selectedColor = new Autodesk.Revit.DB.Color(255, 255, 0);//"yellow";
            else  Globals.selectedColor = null;

            // Update the Globals. walls, columns, and framing variable
            if (chk_Walls.IsChecked   == true) { Globals.wallsIsChecked   = true; } else Globals.wallsIsChecked   = false;
            if (chk_Columns.IsChecked == true) { Globals.columnsIsChecked = true; } else Globals.columnsIsChecked = false;
            if (chk_Framing.IsChecked == true) { Globals.framingIsChecked = true; } else Globals.framingIsChecked = false;

            // call the event 
            ExternalEvent_ElementColorChange.Raise();
            lbl_Info.Content = Globals.lbl_InfoContent;
        }
    }

}
