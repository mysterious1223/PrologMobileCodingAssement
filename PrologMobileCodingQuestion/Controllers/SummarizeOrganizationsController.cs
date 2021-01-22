/*
Author: Kevin Singh
Date: 1/22/2021
Usage: 
Visual Studio : Click build and run
Dot Net Command Line :  dotnet run
    API endpoint: http://localhost:5000/SummarizeOrganizations (if using dotnet run)
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PrologMobileCodingQuestion.Data;
using PrologMobileCodingQuestion.Utility;

namespace PrologMobileCodingQuestion.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SummarizeOrganizationsController : Controller
    {
     
        [HttpGet]
        public async Task<IActionResult> Get()
        {

            List<Phones> phones = new List<Phones>();
            List<Users> users = new List<Users>();
            List<Organizations> organizations = new List<Organizations>();

            phones = await WebRetriever<Phones>.GetWebDataAsync(SD.PhonesUrl);
            users = await WebRetriever<Users>.GetWebDataAsync(SD.UsersUrl);
            organizations = await WebRetriever<Organizations>.GetWebDataAsync(SD.OrganizationsUrl);

            if (phones.Count < 1 || users.Count < 1 || organizations.Count < 1)
            {
                // handle error
                return Json("Count of phone or users or organization is zero!");
            }


            // store organization summary info

            var orgCounts = (from user in users
                                join phone in phones
                                on user.id equals phone.userId
                                into uGroup from phone2 in uGroup
                                select new { 
                                    organizationid = user.organizationId,
                                    TotalCount = uGroup.Count (u=>u.userId == phone2.userId),
                                    BlackListCount = uGroup.Count(u => u.Blacklist == true && u.userId == phone2.userId)
                            }).Distinct();


   
            
            var orgSumm = from organization in organizations
                          join user in users on organization.id equals user.organizationId
                          into userOrgGroup join orgcounts in orgCounts on organization.id equals orgcounts.organizationid
                          into lJoin from lj in lJoin.DefaultIfEmpty()
                          select new {
                            Id = organization.id,
                            Name = organization.name,                         
                            BlackListCount = lj is null ? 0 : lj.BlackListCount,
                            TotalCount = lj is null ? 0 : lj.TotalCount,
                            Users = from user in users
                                    join phone in phones
                                    on user.id equals phone.userId
                                    into userGroup
                                    where user.organizationId == organization.id
                                    select new
                                    {
                                        User = user.id,
                                        Email = user.email,
                                        PhoneCount = userGroup.Count()
                                    }
                          };

           

          
            return Json(orgSumm);
        }
    }
}
