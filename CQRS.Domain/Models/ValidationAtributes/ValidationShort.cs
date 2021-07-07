using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Domain.Models.ValidationAtributes {
    public class ValidationShort : ValidationAttribute {
        public ValidationShort() {
        }

        public override bool IsValid(object value) {
            return value.ToString().Length > 5;
        }
    }
}
