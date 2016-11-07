using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace senses.Helpers {
    public static class ExceptionsHelpers {
        public static String GetMessageFromException(Exception exception) {
            var message = "There was an error";
            return message;
        }

        private static String NotInRange() {
            return "The device is not in range";
        }
    }
}
