using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCartTest
{
 
    public class Service
    {
        private String code;
        public String Code
        {
            get
            {
                return code;
            }
            set
            {
                this.code = value;
            }
        }

        public String Description { get; set; }
        public double Price { get; set; }                       // euro
        public int NoOfDogs { get; set; }
        public override String ToString()
        {
            return "Code:" + Code + "  description: " + Description + "  price: " + Price + "NoOfDoge" + NoOfDogs;
        }

    }
  
    public class CartService : Service
    {
        public int Quantity { get; set; }

        public override String ToString()
        {
            return base.ToString() + " quantity: " + Quantity;
        }
    }

    public class ServiceCart
    {

        private List<CartService> ServiceType;                                              
        public ServiceCart()
        {
            ServiceType = new List<CartService>();
        }

        // read only property to get service list
        public List<CartService> servType
        {
            get
            {
                return new List<CartService>(ServiceType);
            }
        }

      
        public CartService this[String code]
        {
            get
            {
                CartService item = ServiceType.FirstOrDefault(i => i.Code.ToUpper(CultureInfo.CurrentCulture) == code.ToUpper(CultureInfo.CurrentCulture));
                if (item != null)
                {
                    return item;
                }
                else
                {
                    throw new ArgumentException("Service Not Found!");
                }
            }
        }

        // adding services to the cart
   
        public void AddSerive(Service serv)
        {
            // code is unique
            var match = ServiceType.FirstOrDefault(i => i.Code.ToUpper(CultureInfo.CurrentCulture) == serv.Code.ToUpper(CultureInfo.CurrentCulture));
            if (match == null)
            {
                ServiceType.Add(new CartService() { Code = serv.Code, Description = serv.Description, Price = serv.Price, Quantity = 1 });
            }
            else
            {
                match.Quantity++;                  
            }
        }


        // remove item 
     
        public void RemoveService(Service serv)
        {
            var remove = ServiceType.FirstOrDefault(i => i.Code.ToUpper(CultureInfo.CurrentCulture) == serv.Code.ToUpper(CultureInfo.CurrentCulture));
            if (remove != null)
            {
                if (remove.Quantity == 1)
                {
                    ServiceType.Remove(remove);           
                }
                else
                {
                    remove.Quantity--;              
                }
            }
            else
            {
                throw new ArgumentException("Service removed");
            }

        }


        // calculate total price of cart
        public double CalculateTotalPrice()
        {
            return ServiceType.Sum(sv => sv.Price * sv.Quantity);
        }

    }
}

