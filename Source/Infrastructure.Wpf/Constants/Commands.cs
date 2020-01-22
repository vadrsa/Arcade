using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Constants
{
    public static class Commands
    {
        public const string SwitchToDarkMode = "SwitchToDarkMode";
        public const string Exit = "Exit";
        public const string OpenFile = "OpenFileCommand";
        public const string SaveData = "SaveData";
        public const string FinishImport = "FinishImport";

        public const string AddObject = "AddObject";
        public const string AddObjectCopy = "AddObjectCopy";
        public const string EditObject = "EditObject";
        public const string SaveObject = "SaveObject";
        public const string RemoveObject = "RemoveObject";
        public const string CancelEditingObject = "CancelEditingObject";

        public const string ImportFromExcel = "ImportFromExcel";
        public const string Accept = "Accept";

        public const string RefreshList = "RefreshList";
        public const string Search = "Search";
        public const string ExpandAll = "ExpandAll";
        public const string CollapseAll = "CollapseAll";

        public const string NewTab = "NewTabCommand";
        public const string Reconnect = "Reconect";
    }
}
