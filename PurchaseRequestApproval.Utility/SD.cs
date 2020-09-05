using System;
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
        


    }
}
