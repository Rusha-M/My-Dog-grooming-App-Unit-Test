using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestReDogGrooming
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
        public double Price { get; set; }                       
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
    public class applyDiscount :Service
    {
        public int discount { get; set; }

        public void applyintDiscount(int discount)
        {
            this.Price = Price* (1 - (1 * discount / 100));
        }
        public double Getprice()
        {
            return Price;
        }
    }
    public class Basket
    {

        private List<CartService> items;
        public Basket()
        {
            items = new List<CartService>();
        }

        // read only property to get service list
        public List<CartService> servType
        {
            get
            {
                return new List<CartService>(items);
            }
        }


        public CartService this[String code]
        {
            get
            {
                CartService item = items.FirstOrDefault(i => i.Code.ToUpper(CultureInfo.CurrentCulture) == code.ToUpper(CultureInfo.CurrentCulture));
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

        public void AddService(Service serv)
        {
            // code is unique
            var match = items.FirstOrDefault(i => i.Code.ToUpper(CultureInfo.CurrentCulture) == serv.Code.ToUpper(CultureInfo.CurrentCulture));
            if (match == null)
            {
                items.Add(new CartService() { Code = serv.Code, Description = serv.Description, Price = serv.Price, Quantity = 1 });
            }
            else
            {
                match.Quantity++;
            }
        }


        // remove item 

        public void RemoveService(Service serv)
        {
            var remove = items.FirstOrDefault(i => i.Code.ToUpper(CultureInfo.CurrentCulture) == serv.Code.ToUpper(CultureInfo.CurrentCulture));
            if (remove != null)
            {
                if (remove.Quantity == 1)
                {
                    items.Remove(remove);
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
        //add  discount promotion
        public void AddDiscount(applyDiscount noofDogs)
        {
            // code is unique
            var match = servType.Where(i => i.NoOfDogs == noofDogs.discount);
            if (match != null)
            {
                servType.Count();
            }
            else
            {
                Console.WriteLine("No discount applied");
            }
        }


        // calculate total price of cart
        public double CalculateTotalPrice()
        {
            return items.Sum(sv => sv.Price * sv.Quantity);
        }
      
    }
}



    

