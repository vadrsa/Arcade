using Arcade.ViewModels;
using Infrastructure.ObjectManagement;
using Newtonsoft.Json.Linq;
using SharedEntities;
using System.Collections;
using System.Text;

namespace StaffModule.Workitems.StaffAddEdit.Views
{
    public class StaffDetailsViewModel : ObjectManagerDetailsViewModel<EmployeeUploadViewModel>
    {

        protected override string GetFaultMessage(FaultResponse response)
        {

            if (response.Descriptor is IEnumerable)
            {
                var stringBuilder = new StringBuilder();
                foreach (var line in (IEnumerable)response.Descriptor)
                {
                    stringBuilder.Append(line);
                    stringBuilder.Append('\n');
                }
                return stringBuilder.ToString();
            }
            return response.Message;
        }
    }
}
