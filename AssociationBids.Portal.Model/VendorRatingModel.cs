using System;

namespace AssociationBids.Portal.Model
{
    public class VendorRatingModel : BaseModel
    {
        public int VendorRatingKey { get; set; }
        public int VendorKey { get; set; }
        public int ResourceKey { get; set; }
        public int RatingOne { get; set; }
        public int RatingTwo { get; set; }
        public int RatingThree { get; set; }
        public int RatingFour { get; set; }
        public int RatingFive { get; set; }
        public DateTime LastModificationTime { get; set; }
    }
}
