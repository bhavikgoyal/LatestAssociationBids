﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AssociationBids.Portal.Model;
namespace AssociationBids.Portal.Service.Base
{
    public interface ISupportService : IBaseService
    {
        List<SupportModel> SearchBidRequest(long PageSize, long PageIndex, string PropertyName, string VendorName, string CompanyName, string Sort,int BidStatus, Int32 Resourcekey, string BidRequestStatus, int Modulekey, DateTime FromDate, DateTime ToDate);
    }
}