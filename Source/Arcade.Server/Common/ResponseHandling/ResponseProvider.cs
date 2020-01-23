using Common.Enums;
using System.Globalization;
using Common.Core;
using Microsoft.Extensions.Options;
using Common.Configuration;

namespace Common.ResponseHandling
{
    public class ResponseProvider
    {
        private LocalizationDictionary<object, ApiResponse> LocalizationDictionary { get; set; }
        public ResponseProvider(IOptions<ResponseOptions> responseOptions)
        {
            LocalizationDictionary = responseOptions.Value.Dictionary;
        }
        
        public FaultResponse GetFaultResponse(FaultCode code, CultureInfo culture)
        {
            if (!HasCulture(culture) || !LocalizationDictionary[culture.Name].ContainsKey(code))
                return FaultResponse.Default;
            return LocalizationDictionary[culture.Name][code] as FaultResponse;
        }

        private bool HasCulture(CultureInfo culture)
        {
            return LocalizationDictionary.HasCulture(culture.Name);
        }
    }
}
