﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MegaDesk3_MarekSwan
{
    public class DeskQuote
    {
        public bool RushOrder { get; set; }
        public int RushValue { get; set; }
        public string CustName { get; set; }
        public DateTime QuoteDate { get; set; }
        public decimal QuotePrice { get; set; }
        public string SurfaceMaterial { get; set; }
        public Desk Desk { get; set; }


        //const values that won't change for calcs for now (allows for easy modifcation)
        const decimal DESK_BASE_PRICE = 200.00M;
        const decimal DRAWS_PRICE = 50.00M;
        const decimal SURFACE_OAK_PRICE = 200.00M;
        const decimal SURFACE_LAMINATE_PRICE = 100.00M;
        const decimal SURFACE_PINE_PRICE = 50.00M;
        const decimal SURFACE_ROSEWOOD_PRICE = 300.00M;
        const decimal SURFACE_VENEER_PRICE = 125.00M;
        const decimal DELIVERY_14_DAY_PRICE = 0.00M;
        // 3 day rush prices
        const decimal RUSH_3DAY_L1000_PRICE = 60.00M;
        const decimal RUSH_3DAY_1000_TO_2000_PRICE = 70.00M;
        const decimal RUSH_3DAY_G2000_PRICE = 80.00M;
        // 5 day rush prices 
        const decimal RUSH_5DAY_L1000_PRICE = 40.00M;
        const decimal RUSH_5DAY_1000_TO_2000_PRICE = 50.00M;
        const decimal RUSH_5DAY_G2000_PRICE = 60.00M;
        // 7 day rush prices
        const decimal RUSH_7DAY_L1000_PRICE = 30.00M;
        const decimal RUSH_7DAY_1000_TO_2000_PRICE = 35.00M;
        const decimal RUSH_7DAY_G2000_PRICE = 40.00M;

        //Default Constructor, creates the desk in the constructor so that way its already done
        public DeskQuote(float width,float depth, int draws, string SurfaceMaterial, string CustName, string RushValue)
        {
            Desk Desk = new Desk(width,depth,draws);
            this.Desk = Desk;
            this.SurfaceMaterial = SurfaceMaterial;
            this.CustName = CustName;
            this.RushValue = Int32.Parse(RushValue);
        }
        

        //trying to figure out enums

        public decimal CalcSurface(string SurfaceValue)
        {
            switch (SurfaceValue)
            {
                case "Oak":
                    return SURFACE_OAK_PRICE;
                case "Laminate":
                    return SURFACE_LAMINATE_PRICE;
                case "Pine":
                    return SURFACE_PINE_PRICE;
                case "Rosewood":
                    return SURFACE_ROSEWOOD_PRICE;
                default:
                    return SURFACE_VENEER_PRICE;

            }

        }

        public decimal CalcRush(int RushValue, float SurfaceArea)
        {
            if (SurfaceArea < 1000)
            {
                switch (RushValue)
                {
                    case 3:
                        return RUSH_3DAY_L1000_PRICE;
                    case 5:
                        return RUSH_5DAY_L1000_PRICE;
                    case 7:
                        return RUSH_7DAY_L1000_PRICE;

                }
            }
            else if (SurfaceArea >= 1000)
            {
                switch (RushValue)
                {
                    case 3:
                        return RUSH_3DAY_1000_TO_2000_PRICE;
                    case 5:
                        return RUSH_5DAY_1000_TO_2000_PRICE;
                    case 7:
                        return RUSH_7DAY_1000_TO_2000_PRICE;

                }
            }
            else
            {
                switch (RushValue)
                {
                    case 3:
                        return RUSH_3DAY_G2000_PRICE;
                    case 5:
                        return RUSH_5DAY_G2000_PRICE;
                    case 7:
                        return RUSH_7DAY_G2000_PRICE;

                }
            }
            return DELIVERY_14_DAY_PRICE;
        }


        public decimal CalcQuote()
        {
            //base price
            decimal QuotePrice = DESK_BASE_PRICE + (DRAWS_PRICE * Desk.NumOfDraws);

            //surface area calculation
            if (Desk.SurfaceArea > 1000)
            {
                decimal ExtraArea = (decimal)Desk.SurfaceArea - 1000;
                QuotePrice += ExtraArea;
            }

            //surface material Price calculation
            QuotePrice += CalcSurface(SurfaceMaterial);

            //calculate rush price
            QuotePrice += CalcRush(this.RushValue, Desk.SurfaceArea);
       
           

            return QuotePrice;
        }









    }
}
