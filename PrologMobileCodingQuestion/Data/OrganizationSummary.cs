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
    public class OrganizationSummary
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public int BlackListTotal { get; set; }

        public int TotalCount { get; set; }

        public List<UserSummary> Users { get; set; }

    }
}
