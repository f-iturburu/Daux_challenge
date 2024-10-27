using Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Helpers
{
    public class Encode
    {
        public static string EncodeString(Request request)
        {
            string concatenatedValues = $"{request.nombre}{request.apellido}";
            byte[] byteArray = Encoding.UTF8.GetBytes(concatenatedValues);
            string base64Encoded = Convert.ToBase64String(byteArray);
            return base64Encoded;
        }
    }
}