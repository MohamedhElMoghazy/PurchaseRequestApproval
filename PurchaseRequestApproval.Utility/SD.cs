﻿using System;
using System.Collections.Generic;
using System.Text;

namespace PurchaseRequestApproval.Utility
{
   public static class SD
    {
        /* Constant for purchse type procedures */
        public const string Proc_PurchaseType_Create = "usp_CreatePurchaseType";
        public const string Proc_PurchaseType_Get = "usp_GetPurchaseType";
        public const string Proc_PurchaseType_GetAll = "usp_GetPurchaseTypes";
        public const string Proc_PurchaseType_Update = "usp_UpdatePurchaseType";
        public const string Proc_PurchaseType_Delete = "usp_DeletePurchaseType";

        /* Constant for Shipping type */
        public const string Proc_Shipping_Create = "usp_CreateShipping";
        public const string Proc_Shipping_Get = "usp_GetShipping";
        public const string Proc_Shipping_GetAll = "usp_GetShippings";
        public const string Proc_Shipping_Update = "usp_UpdateShipping";
        public const string Proc_Shipping_Delete = "usp_DeleteShipping";

        /* Constant for Employee */        
        public const string Proc_Employee_Create = "usp_CreateEmployee";
        public const string Proc_Employee_Get = "usp_GetEmployee";
        public const string Proc_Employee_GetAll = "usp_GetEmployees";
        public const string Proc_Employee_Update = "usp_UpdateEmployee";
        public const string Proc_Employee_Delete = "usp_DeleteEmployee";

        /* Constant for Vendor */       
        public const string Proc_Vendor_Create = "usp_CreateVendor";
        public const string Proc_Vendor_Get = "usp_GetVendor";
        public const string Proc_Vendor_GetAll = "usp_GetVendors";
        public const string Proc_Vendor_Update = "usp_UpdateVendor";
        public const string Proc_Vendor_Delete = "usp_DeleteVendor";


        /* Constant for Project  */
        public const string Proc_Project_Create = "usp_CreateProject";
        public const string Proc_Project_Get = "usp_GetProject";
        public const string Proc_Project_GetAll = "usp_GetProjects";
        public const string Proc_Project_Update = "usp_UpdateProject";
        public const string Proc_Project_Delete = "usp_DeleteProject";

        public const int AccessLevel1EmpView = 1; // user will only see the tables without modification
        public const int AccessLevel2EmpMod = 2; // user will Modify Tables
        public const int AccessLevel3AdminView = 3; // Admin will only see the tables without modification
        public const int AccessLevel4AdminMod = 4; // Admin will Modify Tables


        public const string Role_Employee_View = "Employee View";
        public const string Role_Employee_Modify = "Employee Modify";
        public const string Role_Admin_View = "Admin View";
        public const string Role_Admin_Modify = "Admin Modify";

        // Adding basic information for file operation 
        public const string TempExcelRoot = @"\files\tmp\";
        public const string TempExcelName = "pratmp_3";
        public const string TempExcelExt = "xlsx";
        public const string TempExcelLocation = @"\files\tmp\pratmp_3.xlsx";
        public const string PRAExcelRoot = @"\files\excel\";

        public const int StartingPRID = 2200;



        static public bool CopyBinaryFile(string srcfilename, string destfilename)

        {



            if (System.IO.File.Exists(srcfilename) == false)

            {

                Console.WriteLine("Could not find the Source file");

                return false;

            }



            System.IO.Stream s1 = System.IO.File.Open(srcfilename, System.IO.FileMode.Open);

            System.IO.Stream s2 = System.IO.File.Open(destfilename, System.IO.FileMode.Create);



            System.IO.BinaryReader f1 = new System.IO.BinaryReader(s1);

            System.IO.BinaryWriter f2 = new System.IO.BinaryWriter(s2);



            while (true)

            {

                byte[] buf = new byte[10240];

                int sz = f1.Read(buf, 0, 10240);

                if (sz <= 0)

                    break;

                f2.Write(buf, 0, sz);

                if (sz < 10240)

                    break; // eof reached

            }

            f1.Close();

            f2.Close();

            return true;

        }














    }
}
