using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
namespace ToDoList
{
    class Global
    {
        public static readonly string VERSION = "1.0";
        public static string PROGRAM_DIRECTORY = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/ToDoList " + VERSION;
        public static string FILE_CONFIG = PROGRAM_DIRECTORY + "/configuration.ini";
        public static string FILE_DATA = PROGRAM_DIRECTORY + "/data.ini";
        public static Main MAIN_FORM = null;
        public static System.Drawing.Point DEFAULT_POSITION = new System.Drawing.Point();
        public static System.Drawing.Point POSITION = new System.Drawing.Point(0, 0);
        public static int X
        {
            get { return POSITION.X; }
            set
            {
                POSITION.X = value;
                MAIN_FORM.Location = POSITION;
            }
        }
        public static int Y
        {
            get { return POSITION.Y; }
            set
            {
                POSITION.Y = value;
                MAIN_FORM.Location = POSITION;
            }
        }
        public static char DATA_LINE_SEPERATOR = '|';
        public static int COUNT_OF_LINES = 15;
        public static List<DataItem> DATA_ITEMS = new List<DataItem>();
        public const int FORM_ABOUT = 0, FORM_SETTING = 1, FORM_ADDITEM = 2, FORM_EDITITEMS = 3;
        public static int REQUESTED_FORM = -1;
        public static Form FORM
        {
            get
            {
                switch (REQUESTED_FORM)
                {
                    case FORM_ABOUT: return new About();
                    case FORM_SETTING: return new Setting();
                    case FORM_ADDITEM: return new AddItem();
                    case FORM_EDITITEMS: return new EditItems();
                }
                return null;
            }
        }
    }
}