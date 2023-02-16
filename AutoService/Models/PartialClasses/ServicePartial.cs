using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AutoService.Models
{
    public partial class Service
    {
        public string DiscountText
        {
            get
            {
                if (Discount == 0 || Discount == null)
                    return "";
                else
                    return $"* скидка {Discount * 100} %";
            }
        }
        public double CostWithDiscount
        {
            get
            {
                if (Discount == 0 || Discount == null)
                {
                    return (double)Cost;
                }
                else
                {
                    var costWithDiscount = (double)Cost * (1.00 - Discount);
                    return costWithDiscount.Value;
                }
            }
        }
        public string TotalCost
        {
            get
            {
                string[] result = DurationInSeconds.Trim().Split();
                int minut;
                if (result[1] == "мин.")
                {
                    minut = Convert.ToInt32(result[0]);
                }
                else
                {
                    if (result[1] == "час.")
                    {
                        minut = Convert.ToInt32(result[0]) * 60;
                    }
                    else
                    {
                        minut = Convert.ToInt32(result[0]) * 1;
                    }

                }
                if (Discount == 0 || Discount == null)
                {
                   
                    return $"{Cost:N2} рублей за {minut / 60} минут";
                }
                else
                {
                    return $"{CostWithDiscount:N2} рублей за {minut / 60} минут";
                }
            }

        }
        public Visibility DiscountVisibility
        {
            get
            {
                if (Discount == 0 || Discount == null)
                    return Visibility.Collapsed;
                else
                    return Visibility.Visible;
            }
        }
        public string BackColor
        {
            get
            {
                if (Discount == 0 || Discount == null)
                    return "#FFFFE1";
                else
                    return "#D1FFD1";
            }
        }
        public string AdminControlsVisibility
        {
            get
            {
                //1-admin 2- user
                if (App.CurrentUser.RoleId ==1)
                {
                    return "Visible";
                }
                else
                {
                    return "Collapsed";
                }
            }
        }
    }
}
