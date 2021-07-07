

using System.Collections.Generic;

namespace CQRS.Api.Utils
{
    public class AppSettings
    {
        public string Secret { get; set; }
        public int MaxPageCount { get; set; }
        public string DefaultLanguage { get; set; }
        public List<string> SupportedLanguages { get; set; }
        public int BackWIPBalanceMonthLimit { get; set; }
    }
}
