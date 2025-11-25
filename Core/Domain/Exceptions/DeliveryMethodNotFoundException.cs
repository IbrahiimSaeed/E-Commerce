using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class DeliveryMethodNotFoundException : NotFoundException 
    {
        public DeliveryMethodNotFoundException(int id) : base($"Delivery Method with id : {id} Not Found")
        {
            
        }
    }
}
