using CQRS.Business.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Business.Handlers.Definition {
    public class GetPageLimiteBase {
        public const int LIMIT_ROWS = 100;

        public int Page { get; }
        public int Limit { get; }
        public int Indexof { get; }

        public GetPageLimiteBase(int page, int limit) {
            Page = page;
            Limit = limit;
            IsValid(Page , Limit);
            Indexof = (Page - 1) * Limit;
        }

        public static void IsValid(int page , int limit) {
            if (page <= 0) throw new BusinessException("Page number should be greather than 0.");
            if (limit <= 0) throw new BusinessException("Limit of rows number should be greather than 0.");
            if (limit > LIMIT_ROWS) throw new BusinessException("Limit of rows number should be equal or less than 100.");
        }
    }
}
