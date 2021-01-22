/*
Author: Kevin Singh
Date: 1/22/2021
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrologMobileCodingQuestion.Data
{
    public class Users
    {
        public string id { get; set; }

        public string organizationId { get; set; }

        public string createdAt { get; set; }

        public string name { get; set; }
        public string email { get; set; }
    }
}
