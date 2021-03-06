﻿using System;

namespace API.DTO.Vendor
{
    public class VendorForGetDTO
    {
        public int Id { get; set; }
        public DateTime CreationDate { get; set; }
        public string Name { get; set; }
        public int BranchesCount { get; set; }
        public string Description { get; set; }
        public string Facebook { get; set; }
        public string Twitter { get; set; }
        public string Instagram { get; set; }
        public string CategoryName { get; set; }
        public string ContractName { get; set; }
        public string LogoName { get; set; }
        public string LicenseName { get; set; }
        public string PersonalIdName{ get; set; }
        public string LogoPath { get; set; }
        public string LicensePath { get; set; }
        public string PersonalIdPath { get; set; }
        public string Status { get; set; }
    }
}
