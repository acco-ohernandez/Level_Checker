using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Level_Checker
{
    public static class Globals
    {
        public static List<Level> levelsList;
        public static string selectedLevel;
        public static Color selectedColor;
        public static bool wallsIsChecked;
        public static bool columnsIsChecked;
        public static bool elemChkboxSelected;
        public static bool framingIsChecked;
        public static string lbl_InfoContent;
    }
}
