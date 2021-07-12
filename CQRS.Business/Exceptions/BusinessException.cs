using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Business.Exceptions {
    public class BusinessException : Exception {
        public BusinessException(String message) : base(message) { }
    }
}
